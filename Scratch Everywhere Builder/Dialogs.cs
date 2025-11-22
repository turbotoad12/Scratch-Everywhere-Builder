namespace Scratch_Everywhere_Builder.Dialogs
{
    internal class FileDialog
    {
        internal enum Filters
        {
            PNG,
            SB3
        }
        OpenFileDialog dialog = new OpenFileDialog();
        FileInfo fileinfo = new FileInfo(".");
        internal FileInfo ShowDialog(Filters filter)
        {
            if (filter == Filters.PNG)
            {
                dialog.Filter = "PNG files (*.png)|*.png";
                dialog.Title = "Select a PNG file";
            }
            else if (filter == Filters.SB3)
            {
                dialog.Filter = "Scratch Project files (*.sb3)|*.sb3";
                dialog.Title = "Select a Scratch Project file";
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileinfo = new FileInfo(dialog.FileName);
            }
            else
            {
                throw new OperationCanceledException("User canceled file dialog.");
            }
            return fileinfo;
        }

    }
    internal class FolderDialog
    {
        FolderBrowserDialog dialog = new FolderBrowserDialog();
        DirectoryInfo directoryinfo = new DirectoryInfo(".");
        internal DirectoryInfo ShowDialog()
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                directoryinfo = new DirectoryInfo(dialog.SelectedPath);
            }
            else
            {
                throw new OperationCanceledException("User canceled folder dialog.");
            }
            return directoryinfo;
        }
    }
}
