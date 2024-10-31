using Application.Features.Dogs.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Dogs.Rules;

public class DogBusinessRules : BaseBusinessRules
{
    private readonly IDogRepository _dogRepository;
    private readonly ILocalizationService _localizationService;

    public DogBusinessRules(IDogRepository dogRepository, ILocalizationService localizationService)
    {
        _dogRepository = dogRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, DogsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task DogShouldExistWhenSelected(Dog? dog)
    {
        if (dog == null)
            await throwBusinessException(DogsBusinessMessages.DogNotExists);
    }

    public async Task DogNameIsUniqueAsync(string name, CancellationToken cancellationToken)
    {
        bool hasDog = await _dogRepository.AnyAsync(
            predicate: d => d.Name == name,
            cancellationToken: cancellationToken
        );
        if (hasDog)
            await throwBusinessException(DogsBusinessMessages.DogNameIsUnique);
    }

    public async Task DogNameIsUniqueAsync(string name, string updatedName)
    {
        if (name == updatedName)
            await throwBusinessException(DogsBusinessMessages.DogNameIsUnique);
    }
}