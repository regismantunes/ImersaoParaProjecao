using ImmersionToProjection.View;

namespace ImmersionToProjection.Service.ViewFactory;

public interface IImmersionWeekViewFactory
{
    ImmersionWeekView? CreateImmersionWeekView(string filePath);
}