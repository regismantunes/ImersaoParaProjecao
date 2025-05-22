using ImmersionToProjection.Model;
using ImmersionToProjection.Service.Language;
using System.Windows;

namespace ImmersionToProjection.ViewModel;

public class ConfigurationPatternsItemViewModel(ILanguageKeys languageKeys) : BaseViewModel(languageKeys)
{
    public bool EnableFields => !string.IsNullOrEmpty(Language); 

    public PatternsItem? PatternsItem { get; set; }

    public string Language
    {
        get => PatternsItem?.Language ?? string.Empty;
        set
        {
            if (value is null)
                return;

            if (PatternsItem is null)
            {
                PatternsItem = new PatternsItem()
                {
                    Language = value,
                    MessageTitleFormat = string.Empty,
                    ImmersionPoint = string.Empty,
                    EndOfDaillyPoint = string.Empty,
                    MessageHeader = string.Empty,
                    Number = string.Empty,
                    BibleReading = string.Empty
                };
                OnPropertyChanged(nameof(Language));
                OnPropertyChanged(nameof(EnableFields));
            }
            else
            {
                PatternsItem.Language = value;
                OnPropertyChanged(nameof(Language));
            }
        }
    }

    public string? MessageTitleFormat
    {
        get => PatternsItem?.MessageTitleFormat;
        set
        {
            if (PatternsItem is null)
                return;
            PatternsItem.MessageTitleFormat = value ?? string.Empty;
            OnPropertyChanged(nameof(MessageTitleFormat));
        }
    }

    public string? ImmersionPoint
    {
        get => PatternsItem?.ImmersionPoint;
        set
        {
            if (PatternsItem is null)
                return;
            PatternsItem.ImmersionPoint = value ?? string.Empty;
            OnPropertyChanged(nameof(ImmersionPoint));
        }
    }

    public string? EndOfDaillyPoint
    {
        get => PatternsItem?.EndOfDaillyPoint;
        set
        {
            if (PatternsItem is null)
                return;
            PatternsItem.EndOfDaillyPoint = value ?? string.Empty;
            OnPropertyChanged(nameof(EndOfDaillyPoint));
        }
    }

    public string? MessageHeader
    {
        get => PatternsItem?.MessageHeader;
        set
        {
            if (PatternsItem is null)
                return;
            PatternsItem.MessageHeader = value ?? string.Empty;
            OnPropertyChanged(nameof(MessageHeader));
        }
    }

    public string? Number
    {
        get => PatternsItem?.Number;
        set
        {
            if (PatternsItem is null)
                return;
            PatternsItem.Number = value ?? string.Empty;
            OnPropertyChanged(nameof(Number));
        }
    }

    public string? BibleReading
    {
        get => PatternsItem?.BibleReading;
        set
        {
            if (PatternsItem is null)
                return;
            PatternsItem.BibleReading = value ?? string.Empty;
            OnPropertyChanged(nameof(BibleReading));
        }
    }
}