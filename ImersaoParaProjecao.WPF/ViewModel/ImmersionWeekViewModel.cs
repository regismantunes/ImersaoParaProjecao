using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ImersaoParaProjecao.Utility;

namespace ImersaoParaProjecao.ViewModel
{
    public class ImmersionWeekViewModel
    {
        public ICommand DragEnter { get; }
        public ICommand DragDrop { get; }

        private void DragEnterCommand(object sender, DragEventArgs e)
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

        private void DragDropCommand(object sender, DragEventArgs e)
        {
            if (e.Data == null)
                return;

            if (e.Data.GetData(DataFormats.FileDrop, false) is not string[] dragData ||
                dragData.Length != 1 ||
                (dragData.Length == 1 && !dragData[0].EndsWith(".pdf")))
                return;

            //LoadImersionFile(dragData[0]);
        }
    }
}
