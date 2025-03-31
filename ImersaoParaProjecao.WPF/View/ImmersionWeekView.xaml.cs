using System.Windows;
using ImersaoParaProjecao.ViewModel;

namespace ImersaoParaProjecao.View
{
    /// <summary>
    /// Interaction logic for ImmersionWeekView.xaml
    /// </summary>
    public partial class ImmersionWeekView : Window
    {
        public ImmersionWeekView()
        {
            InitializeComponent();

            DataContext = new ImmersionWeekViewModel();
        }
    }
}
