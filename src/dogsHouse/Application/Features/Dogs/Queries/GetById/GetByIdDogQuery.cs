using Application.Features.Dogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Dogs.Queries.GetById;

public class GetByIdDogQuery : IRequest<GetByIdDogResponse>
{
    public Guid Id { get; set; }

    public class GetByIdDogQueryHandler : IRequestHandler<GetByIdDogQuery, GetByIdDogResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDogRepository _dogRepository;
        private readonly DogBusinessRules _dogBusinessRules;

        public GetByIdDogQueryHandler(IMapper mapper, IDogRepository dogRepository, DogBusinessRules dogBusinessRules)
        {
            _mapper = mapper;
            _dogRepository = dogRepository;
            _dogBusinessRules = dogBusinessRules;
        }

        public async Task<GetByIdDogResponse> Handle(GetByIdDogQuery request, CancellationToken cancellationToken)
        {
            Dog? dog = await _dogRepository.GetAsync(predicate: d => d.Id == request.Id, cancellationToken: cancellationToken);
            await _dogBusinessRules.DogShouldExistWhenSelected(dog);

            GetByIdDogResponse response = _mapper.Map<GetByIdDogResponse>(dog);
            return response;
        }
    }
}