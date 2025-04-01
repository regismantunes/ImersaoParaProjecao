using System.ComponentModel;

namespace ImersaoParaProjecao.ViewModel;

public class BaseViewModel
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
