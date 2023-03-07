using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Vocappulary.Persistence.Data;
using Vocappulary.Persistence.Services;

namespace Vocappulary.ViewModels;

public partial class AdministrationViewModel : ObservableValidator
{
    private LearnItemRepository _learnItemRepository;

    [ObservableProperty]
    [Required(AllowEmptyStrings = false)]
    [MaxLength(1024)]
    [NotifyDataErrorInfo]
    private string? _phrase;

    [ObservableProperty]
    [Required(AllowEmptyStrings = false)]
    [MaxLength(1024)]
    [NotifyDataErrorInfo]
    private string? _translation;

    public List<LearnItem>? LearnItems
    {
        get;
        private set;
    }

    public AdministrationViewModel(LearnItemRepository learnItemRepository)
    {
        _learnItemRepository = learnItemRepository;
        LearnItems = new List<LearnItem>();
        ClearInputForm();
    }

    public async Task AddNewLearnItem()
    {
        if (Phrase == null || Translation == null)
            return;

        LearnItem newItem = new LearnItem
        {
            Phrase = Phrase,
            Translation = Translation
        };

        await _learnItemRepository.SaveAsync(newItem);

        ClearInputForm();

        await LoadLearnItems();
    }

    public async Task LoadLearnItems()
    {
        LearnItems = await _learnItemRepository.GetAllAsync();
        OnPropertyChanged();
    }

    private void ClearInputForm()
    {
        Phrase = string.Empty;
        Translation = string.Empty;
    }

    public async Task<int> DeleteAll()
    {
        var numberOfDeletesEntries = await _learnItemRepository.DeletaAllAsync();
        OnPropertyChanged();
        return numberOfDeletesEntries;
    }

}

