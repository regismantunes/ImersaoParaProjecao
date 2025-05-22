using ImmersionToProjection.Model;
using System.ComponentModel;

namespace ImmersionToProjection.Service.Configuration;

public interface IAppConfiguration : INotifyPropertyChanged
{
    string Language { get; set; }
    IEnumerable<PatternsItem> Patterns { get; set; }
    string Theme { get; set; }

    void Save();
}