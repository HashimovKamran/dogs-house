using Application.Features.Dogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Dogs.Commands.Update;

public class UpdateDogCommand : IRequest<UpdatedDogResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Color { get; set; }
    public required ushort TailLength { get; set; }
    public required ushort Weight { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDogs"];

    public class UpdateDogCommandHandler : IRequestHandler<UpdateDogCommand, UpdatedDogResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDogRepository _dogRepository;
        private readonly DogBusinessRules _dogBusinessRules;

        public UpdateDogCommandHandler(IMapper mapper, IDogRepository dogRepository,
                                         DogBusinessRules dogBusinessRules)
        {
            _mapper = mapper;
            _dogRepository = dogRepository;
            _dogBusinessRules = dogBusinessRules;
        }

        public async Task<UpdatedDogResponse> Handle(UpdateDogCommand request, CancellationToken cancellationToken)
        {
            Dog? dog = await _dogRepository.GetAsync(predicate: d => d.Id == request.Id, cancellationToken: cancellationToken);
            await _dogBusinessRules.DogShouldExistWhenSelected(dog);
            await _dogBusinessRules.DogNameIsUniqueAsync(dog.Name, request.Name);
            dog = _mapper.Map(request, dog);

            await _dogRepository.UpdateAsync(dog!);

            UpdatedDogResponse response = _mapper.Map<UpdatedDogResponse>(dog);
            return response;
        }
    }
}