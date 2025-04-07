using ImmersionToProjection.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace ImmersionToProjection.View;

/// <summary>
/// Interaction logic for ImmersionWeekView.xaml
/// </summary>
public partial class ImmersionWeekView : UserControl
{
    public ImmersionWeekView()
    {
        InitializeComponent();
    }

    private void UserControl_DragEnter(object sender, System.Windows.DragEventArgs e)
    {
        if (e.Data == null)
            return;

        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            if (e.Data.GetData(DataFormats.FileDrop, false) is string[] dragData &&
                dragData.Length == 1 &&
                dragData[0].EndsWith(".pdf"))
            {
                e.Effects = DragDropEffects.All;
                return;
            }
        }

        e.Effects = DragDropEffects.None;
    }

    private void UserControl_Drop(object sender, DragEventArgs e)
    {
        if (e.Data == null)
            return;

        if (e.Data.GetData(DataFormats.FileDrop, false) is not string[] dragData ||
            dragData.Length != 1 ||
            (dragData.Length == 1 && !dragData[0].EndsWith(".pdf")))
            return;

        if (DataContext is ImmersionWeekViewModel viewModel)
        {
            e.Effects = DragDropEffects.Scroll;
            viewModel.OnFileDroppedAsync(dragData[0])
                .ConfigureAwait(false);
        }
    }
}