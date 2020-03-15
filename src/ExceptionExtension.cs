using System;
using System.Windows.Forms;

public static class ExceptionExtension
{
    public static void ShowAsMessageBox(this Exception exception, IWin32Window owner, string caption,
                                        MessageBoxIcon icon = MessageBoxIcon.Error)
    {
        MessageBox.Show(owner, exception.Message, caption, MessageBoxButtons.OK, icon);
    }
}