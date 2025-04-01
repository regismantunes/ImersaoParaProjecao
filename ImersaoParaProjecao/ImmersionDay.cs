namespace ImersaoParaProjecao
{
    public partial class ImmersionDay : UserControl
    {
        private readonly string _day;
        private readonly string _messageTitle;
        private readonly string[] _immersionItems;

        public ImmersionDay(string messageTitle, string day, string[] immersionItems)
        {
            InitializeComponent();

            _day = day;
            _messageTitle = messageTitle;
            _immersionItems = immersionItems;

            lblWeekday.Text = _day;
            lblImersionItems.Text = string.Join("\r\n", _immersionItems);
        }

        public string[] ImmersionItems => lblImersionItems.Text.Split("\r\n");

        public string Day => _day;
        public string MessageTitle => _messageTitle;

        private void btnCopyTitleToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"IMERSÃO DIÁRIA NA PALAVRA PROFÉTICA | {Day} | {_messageTitle}");
        }

        private void btnCopyContentToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(string.Join("\r\n\r\n", _immersionItems));
        }
    }
}
