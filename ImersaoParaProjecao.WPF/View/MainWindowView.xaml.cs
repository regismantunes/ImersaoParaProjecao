using ImmersionToProjection.ViewModel;
using System.Windows;

namespace ImmersionToProjection.View;

public partial class MainWindowView : Window
{
    public MainWindowView()
    {
        InitializeComponent();
    }

    private void Border_DragEnter(object sender, System.Windows.DragEventArgs e)
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

    private void Border_Drop(object sender, DragEventArgs e)
    {
        if (e.Data == null)
            return;

        if (e.Data.GetData(DataFormats.FileDrop, false) is not string[] dragData ||
            dragData.Length != 1 ||
            (dragData.Length == 1 && !dragData[0].EndsWith(".pdf")))
            return;

        if (DataContext is MainWindowViewModel viewModel)
        {
            e.Effects = DragDropEffects.Scroll;
            viewModel.OnFileDroppedAsync(dragData[0])
                .ConfigureAwait(false);
        }
    }
}