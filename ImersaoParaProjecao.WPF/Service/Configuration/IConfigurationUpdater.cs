namespace ImmersionToProjection.Service.Configuration
{
    public interface IConfigurationUpdater
    {
        void UpdateSetting(string key, string value);
    }
}