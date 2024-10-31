using Application.Features.Dogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Dogs.Commands.Delete;

public class DeleteDogCommand : IRequest<DeletedDogResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDogs"];

    public class DeleteDogCommandHandler : IRequestHandler<DeleteDogCommand, DeletedDogResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDogRepository _dogRepository;
        private readonly DogBusinessRules _dogBusinessRules;

        public DeleteDogCommandHandler(IMapper mapper, IDogRepository dogRepository,
                                         DogBusinessRules dogBusinessRules)
        {
            _mapper = mapper;
            _dogRepository = dogRepository;
            _dogBusinessRules = dogBusinessRules;
        }

        public async Task<DeletedDogResponse> Handle(DeleteDogCommand request, CancellationToken cancellationToken)
        {
            Dog? dog = await _dogRepository.GetAsync(predicate: d => d.Id == request.Id, cancellationToken: cancellationToken);
            await _dogBusinessRules.DogShouldExistWhenSelected(dog);

            await _dogRepository.DeleteAsync(dog!);

            DeletedDogResponse response = _mapper.Map<DeletedDogResponse>(dog);
            return response;
        }
    }
}