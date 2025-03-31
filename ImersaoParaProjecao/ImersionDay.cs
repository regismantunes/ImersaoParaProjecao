namespace ImersaoParaProjecao
{
    public partial class ImersionDay : UserControl
    {
        private readonly string _day;
        private readonly string _messageTitle;
        private readonly string[] _imersionItems;

        public ImersionDay(string messageTitle, string day, string[] imersionItems)
        {
            InitializeComponent();

            _day = day;
            _messageTitle = messageTitle;
            _imersionItems = imersionItems;

            lblWeekday.Text = _day;
            lblImersionItems.Text = string.Join("\r\n", _imersionItems);
        }

        public string[] ImersionItems => lblImersionItems.Text.Split("\r\n");

        public string Day => _day;
        public string MessageTitle => _messageTitle;

        private void btnCopyTitleToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"IMERSÃO DIÁRIA NA PALAVRA PROFÉTICA | {Day} | {_messageTitle}");
        }

        private void btnCopyContentToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(string.Join("\r\n\r\n", _imersionItems));
        }
    }
}
