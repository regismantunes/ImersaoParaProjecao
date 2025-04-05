namespace ImmersionToProjection.Utility
{
    public static class MessageError
    {
        public static void ShowError(Form owner, Exception ex, string errorDescription)
        {
#if DEBUG
            MessageBox.Show(owner, ex.ToString(), errorDescription, MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
            MessageBox.Show(owner, errorDescription, owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
        }
    }
}
