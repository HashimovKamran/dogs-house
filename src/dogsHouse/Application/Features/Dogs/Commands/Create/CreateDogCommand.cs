using Application.Features.Dogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;

namespace Application.Features.Dogs.Commands.Create;

public class CreateDogCommand : IRequest<CreatedDogResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required string Name { get; set; }
    public required string Color { get; set; }
    public required ushort TailLength { get; set; }
    public required ushort Weight { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDogs"];

    public class CreateDogCommandHandler : IRequestHandler<CreateDogCommand, CreatedDogResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDogRepository _dogRepository;
        private readonly DogBusinessRules _dogBusinessRules;

        public CreateDogCommandHandler(IMapper mapper, IDogRepository dogRepository,
                                         DogBusinessRules dogBusinessRules)
        {
            _mapper = mapper;
            _dogRepository = dogRepository;
            _dogBusinessRules = dogBusinessRules;
        }

        public async Task<CreatedDogResponse> Handle(CreateDogCommand request, CancellationToken cancellationToken)
        {
            Dog dog = _mapper.Map<Dog>(request);
            await _dogBusinessRules.DogNameIsUniqueAsync(dog.Name, cancellationToken);

            await _dogRepository.AddAsync(dog);

            CreatedDogResponse response = _mapper.Map<CreatedDogResponse>(dog);
            return response;
        }
    }
}