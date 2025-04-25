using Microsoft.Extensions.Configuration;
using System.ComponentModel;

namespace ImmersionToProjection.Service.Configuration
{ 
    public interface IConfigurationManager : IConfiguration, INotifyPropertyChanged
    {
        string GetSetting(string key);
        void UpdateSetting(string key, string value);
    }
}