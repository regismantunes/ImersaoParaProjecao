using ImmersionToProjection.View;
using ImmersionToProjection.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace ImmersionToProjection.Service.ViewFactory;

public class ImmersionWeekViewFactory(IServiceProvider serviceProvider) : IImmersionWeekViewFactory
{
    public ImmersionWeekView? CreateImmersionWeekView(string filePath)
    {
        var view = serviceProvider.GetRequiredService<ImmersionWeekView>();

        if (view.DataContext is ImmersionWeekViewModel viewModel)
        {
            viewModel.LoadImmersionWeek(filePath);
            return view;
        }

        return null;
    }
}
