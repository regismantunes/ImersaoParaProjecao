using ImmersionToProjection.View;
using System.Windows.Controls;

namespace ImmersionToProjection.ViewModel;

public class MainWindowViewModel(ImmersionWeekView immersionWeekView) : BaseViewModel
{
    public UserControl ContentUserControl { get; private set; } = immersionWeekView;
}