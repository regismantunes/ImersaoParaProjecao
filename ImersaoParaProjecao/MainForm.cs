using ImersaoParaProjecao.Utility;

namespace ImersaoParaProjecao
{
    public partial class MainForm : Form
    {
        private bool _loaded;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_loaded)
                    return;

                var imersionDaysList = new Dictionary<string, string[]>();

                var imersionDays = from ImmersionDay imersionDay in flpImersionDays.Controls
                                   select imersionDay;

                foreach (ImmersionDay control in imersionDays)
                    imersionDaysList.Add(control.Day, control.ImmersionItems);

                var imersionText = ImmersionExtractor.GetImmersionToProjection(imersionDaysList);

                Clipboard.SetText(imersionText);
            }
            catch (Exception ex)
            {
                MessageError.ShowError(this, ex, "Erro ao copiar para área de transferência!");
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data == null)
                return;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (e.Data.GetData(DataFormats.FileDrop, false) is string[] dragData &&
                    dragData.Length == 1 &&
                    dragData[0].EndsWith(".pdf"))
                {
                    e.Effect = DragDropEffects.All;
                    return;
                }
            }

            e.Effect = DragDropEffects.None;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data == null)
                return;

            if (e.Data.GetData(DataFormats.FileDrop, false) is not string[] dragData ||
                dragData.Length != 1 ||
                (dragData.Length == 1 && !dragData[0].EndsWith(".pdf")))
                return;

            LoadImersionFile(dragData[0]);
        }

        private void ClearLoadedImersionFile()
        {
            _loaded = false;

            lblMessageTitle.Text = string.Empty;
            flpImersionDays.Controls.Clear();
        }

        private void LoadImersionFile(string filePath)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                ClearLoadedImersionFile();

                var imersionDays = ImmersionExtractor.GetImmersionDays(filePath, out var messageTitle);
                if (imersionDays == null)
                    return;

                lblMessageTitle.Text = messageTitle;

                foreach (var imersionDay in imersionDays)
                {
                    var day = imersionDay.Key;
                    var items = imersionDay.Value;

                    var ucImersionDay = new ImmersionDay(messageTitle, day, items)
                    {
                        Width = flpImersionDays.Width - 25,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                    };

                    flpImersionDays.Controls.Add(ucImersionDay);
                }

                _loaded = true;
            }
            catch (Exception ex)
            {
                MessageError.ShowError(this, ex, "Erro ao carregar arquivo!");
            }
            finally
            {
                if (!_loaded)
                    ClearLoadedImersionFile();

                Cursor = Cursors.Default;
            }
        }

        private void flpImersionDays_Resize(object sender, EventArgs e)
        {
            foreach (Control control in flpImersionDays.Controls)
                control.Width = flpImersionDays.Width - 25;
        }
    }
}