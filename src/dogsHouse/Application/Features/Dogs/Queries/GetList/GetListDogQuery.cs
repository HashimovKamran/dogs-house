using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using NArchitecture.Core.Persistence.Dynamic;

namespace Application.Features.Dogs.Queries.GetList;

public class GetListDogQuery : IRequest<GetListResponse<GetListDogListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery? DynamicQuery { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey => GenerateCacheKey();
    public string? CacheGroupKey => "GetDogs";
    public TimeSpan? SlidingExpiration { get; }

    private string GenerateCacheKey()
    {
        var sortKey = DynamicQuery?.Sort != null
            ? string.Join(",", DynamicQuery.Sort.Select(s => $"{s.Field}:{s.Dir}"))
            : "default_sort";

        var filterKey = DynamicQuery?.Filter != null
            ? $"{DynamicQuery.Filter.Field}:{DynamicQuery.Filter.Operator}:{DynamicQuery.Filter.Value}"
            : "default_filter";

        return $"GetListDogs(PageIndex:{PageRequest.PageIndex},PageSize:{PageRequest.PageSize},Sort:{sortKey},Filter:{filterKey})";
    }

    public class GetListDogQueryHandler : IRequestHandler<GetListDogQuery, GetListResponse<GetListDogListItemDto>>
    {
        private readonly IDogRepository _dogRepository;
        private readonly IMapper _mapper;

        public GetListDogQueryHandler(IDogRepository dogRepository, IMapper mapper)
        {
            _dogRepository = dogRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListDogListItemDto>> Handle(GetListDogQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Dog> dogs = request.DynamicQuery is null
                ? await _dogRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken)
                : await _dogRepository.GetListByDynamicAsync(
                    request.DynamicQuery,
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken);

            return _mapper.Map<GetListResponse<GetListDogListItemDto>>(dogs);
        }
    }
}