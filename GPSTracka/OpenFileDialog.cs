using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GPSTracka
{
    /// <summary>A common dialog usef for opening files</summary>
    public partial class OpenFileDialog : Form
    {

#region Initialization
        /// <summary>True until <see cref="OnLoad"/> finished. Prevents some event handlers.</summary>
        private bool loading = true;
        /// <summary>CTor - creates a new instance of the <see cref="OpenFileDialog"/> class</summary>
        public OpenFileDialog()
        {
            InitializeComponent();
            imlLargeIcons.ImageSize = OpenNETCF.Windows.Forms.SystemInformation2.IconSize;
            imlSmallIcons.ImageSize = OpenNETCF.Windows.Forms.SystemInformation2.SmallIconSize;
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            AddExtension = true;
            CheckFileExists = true;
            FollowLinks = true;
        }
        private string currentDirectory;
        /// <summary>Raises the <see cref="System.Windows.Forms.Form.Load"/> event.</summary>
        /// <param name="e">An <see cref="System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            bool loaded = false;
            //Load initial directory
            try
            {
                if (Directory.Exists(InitialDirectory))
                {
                    LoadFolder(InitialDirectory, SelectedFilter);
                    loaded = true;
                }
            }
            catch { }
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            if (!loaded && Directory.Exists(documents)) try
                {
                    LoadFolder(documents, SelectedFilter);
                    loaded = true;
                 }
                catch { }
            if (!loaded)
            {
                try
                {
                    LoadFolder("\\", SelectedFilter);
                }
                catch
                {
                    MessageBox.Show(string.Format(Properties.Resources.err_LoadDirectory, "\\"), Properties.Resources.err_ErrorTitleMsg, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                currentDirectory = "\\";
            }

            foreach (var item in filters)
            {
                cmbFilter.Items.Add(item[0]);
            }
            if (FilterIndex >= 0 && FilterIndex < cmbFilter.Items.Count) cmbFilter.SelectedIndex = FilterIndex;
            loading = false;

        }
#endregion
#region Folder browsing
        /// <summary>Loads content of the folder and presents results to the user</summary>
        /// <param name="path">Folder to load</param>
        /// <param name="mask">File filter. Multiple filters can be separated by ;</param>
        private void LoadFolder(string path, string mask)
        {
            lvwFiles.Items.Clear();
            while (imlLargeIcons.Images.Count > 2) imlLargeIcons.Images.RemoveAt(2);
            while (imlSmallIcons.Images.Count > 2) imlSmallIcons.Images.RemoveAt(2);
            var folder = true;
            var masks = string.IsNullOrEmpty(mask)? new string[]{} : mask.Split(';');
            var oc = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try {
                foreach (var array in new string[][] { Directory.GetDirectories(path), Directory.GetFiles(path) }) {
                    foreach (var itemPath in array) {
                        if (!folder && masks.Length > 0) {
                            bool fits = false;
                            foreach (string msk in masks) {
                                if (Microsoft.VisualBasic.CompilerServices.StringType.StrLikeText(Path.GetFileName(itemPath), msk)) {
                                    fits = true;
                                    break;
                                }
                            }
                            if (!fits) continue;
                        }
                        var item = new ListViewItem();
                        item.Text = Path.GetFileName(itemPath);
                        item.ImageIndex = folder ? 0 : 1;
                        try {
                            var largeIcon = Tools.IOt.FileSystemTools.GetIcon(itemPath, true, true);
                            var smallIcon = Tools.IOt.FileSystemTools.GetIcon(itemPath, false, true);
                            if (largeIcon != null && smallIcon != null) {
                                imlSmallIcons.Images.Add(smallIcon);
                                imlLargeIcons.Images.Add(largeIcon);
                                item.ImageIndex = imlLargeIcons.Images.Count - 1;
                            }
                        } catch { }
                        if (folder) {
                            var fsi = new DirectoryInfo(itemPath);
                            item.Tag = fsi;
                            item.SubItems.Add(fsi.LastWriteTime.ToString("F"));
                            item.SubItems.Add("");
                            item.SubItems.Add(attrributesToString(fsi.Attributes));
                        } else {
                            var fsi = new FileInfo(itemPath);
                            item.Tag = fsi;
                            item.SubItems.Add(fsi.LastWriteTime.ToString("F"));
                            item.SubItems.Add(fsi.Length.ToString());
                            item.SubItems.Add(attrributesToString(fsi.Attributes));
                        }
                        lvwFiles.Items.Add(item);
                    }
                    folder = false;
                }
                if (sortIndex >= 0) Sort();
                currentDirectory = path;
                lblPath.Text = currentDirectory;
            } finally {
                Cursor.Current = oc;
            }
        }
        /// <summary>Converts file or folder attributes to string</summary>
        /// <param name="attributes">Attrutes to get string representation of</param>
        /// <returns>String representation of <paramref name="attributes"/> with unique letter assigned to each flag set</returns>
        private static string attrributesToString(FileAttributes attributes)
        {
            StringBuilder b = new StringBuilder();
            if ((attributes & FileAttributes.Archive) != 0) b.Append(Properties.Resources.fileAttr_Archive); //A
            if ((attributes & FileAttributes.Compressed ) != 0) b.Append(Properties.Resources.fileAttr_Compressed);//C
            if ((attributes & FileAttributes.Device ) != 0) b.Append(Properties.Resources.fileAttr_Device); //D
            if ((attributes & FileAttributes.Directory ) != 0) b.Append(Properties.Resources.fileAttr_Directory);//F
            if ((attributes & FileAttributes.Encrypted ) != 0) b.Append(Properties.Resources.fileAttr_Encrypted); //E
            if ((attributes & FileAttributes.Hidden ) != 0) b.Append(Properties.Resources.fileAttr_Hidden); //H
            if ((attributes & FileAttributes.NotContentIndexed ) != 0) b.Append(Properties.Resources.fileAttr_NotContentIndexed);//N
            if ((attributes & FileAttributes.Offline ) != 0) b.Append(Properties.Resources.fileAttr_Offline); //O
            if ((attributes & FileAttributes.ReadOnly ) != 0) b.Append(Properties.Resources.fileAttr_ReadOnly);//R
            if ((attributes & FileAttributes.SparseFile ) != 0) b.Append(Properties.Resources.fileAttr_SparseFile);//P
            if ((attributes & FileAttributes.System ) != 0) b.Append(Properties.Resources.fileAttr_System); //S
            if ((attributes & FileAttributes.Temporary ) != 0) b.Append(Properties.Resources.fileAttr_Temporary);//T
            return b.ToString();
        }
#endregion

#region Properties
        /// <summary>Gets or sets path of currently selected file</summary>
        public string FileName { get; set; }
        /// <summary>Contains the filter options 0 - description, 1 - pattern</summary>
        private string[][] filters=new string[][] { };
        /// <summary>Gets or sets the current file name filter string, which determines the choices that appear in the "FileType" drop-down box.</summary>
        /// <remarks>
        /// For each filtering option, the filter string contains a description of the filter, followed by the vertical bar (|) and the filter pattern. The strings for different filtering options are separated by the vertical bar.
        /// You can add several filter patterns to a filter by separating the file types with semicolons.
        /// </remarks>
        public string Filter {
            get {
                StringBuilder b = new StringBuilder();
                foreach (var item in filters)
                {
                    if (b.Length > 0) b.Append("|");
                    b.Append(item[0]);
                    b.Append("|");
                    b.Append(item[1]);
                }
                return b.ToString();
            }
            set {
                if (string.IsNullOrEmpty(value))
                {
                    this.filters = new string[][] { };
                    return;
                }
                var parts = value.Split('|');
                List<string[]> filters = new List<string[]>(parts.Length / 2);
                for (int i = 1; i < parts.Length; i += 2)
                {
                    filters.Add(new string[] { parts[i - 1], parts[i] });                    
                }
                this.filters = filters.ToArray();
            }
        }
        /// <summary>Gets or sets index of currently selected filter</summary>
        public int FilterIndex { get; set; }
        /// <summary>Gets or sets the directory dialog shows at first</summary>
        /// <value>Default value is user home directory (documents)</value>
        public string InitialDirectory { get; set; }
        /// <summary>Gets or sets title of the dialog</summary>
        public string Title { get { return Text; } set { Text = value; } }
        /// <summary>Gets or sets value indicating if dialog automaticallly ads default extension if user does not specify it</summary>
        public bool AddExtension { get; set; }
        /// <summary>Default extension appended to file name entered by user when <paramref name="AddExtension"/> is true</summary>
        public string DefaultExt { get; set; }
        /// <summary>True when dialog only closes when user specified an existing file</summary>
        public bool CheckFileExists { get; set; }
        /// <summary>Gets currently applicable filter</summary>
        public string SelectedFilter
        {
            get
            {
                if(FilterIndex<0 || FilterIndex >= filters.Length) return null;
                return filters[FilterIndex][1];
            }
        }

        /// <summary>gets or sets current mode haow files and folders are displayed</summary>
      public   View View { get { return lvwFiles.View; } set { lvwFiles.View = value; } }

        /// <summary>Gets or sets value if links are followed or returned</summary>
      public bool FollowLinks { get; set; }
#endregion

        /// <summary>True when window can be closed without verifying file name (cancel situation)</summary>
        private bool allowClose = false;
        private void mniCancel_Click(object sender, EventArgs e)
        {
            allowClose = true;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void mniOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>Raises the <see cref="System.Windows.Forms.Form.Closing"/> event.</summary>
        /// <param name="e">A <see cref="System.ComponentModel.CancelEventArgs"/> that contains the event data.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (!allowClose) 
                if (!(e.Cancel = !FollowPath(txtFileName.Text, FollowLinks))) DialogResult = DialogResult.OK;
            if (!e.Cancel) base.OnClosing(e);
        }
        
        /// <summary>Follows path selected by user</summary>
        /// <param name="fileName">Path to be followed</param>
        /// <param name="followLinks">True to follow links in *.lnk files</param>
        /// <returns>True when <paramref name="fileName"/> represents a file that can be returned from dialog; false otherwise (invalid path, directory navigation, filter change)</returns>
        private bool FollowPath(string fileName, bool followLinks)
        {
            //Empty path - do nothing
            if (string.IsNullOrEmpty(fileName)) return false;
            //Current directory
            else if (fileName == ".") return false;
            //Up one level
            else if (fileName == "..")
            {
                if (currentDirectory == "\\") return false;
                var parts = currentDirectory.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar).TrimEnd(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
                try
                {
                    string newPath = string.Join(Path.DirectorySeparatorChar.ToString(), parts, 0, parts.Length - 1);
                    LoadFolder(string.IsNullOrEmpty(newPath) ? "\\" : newPath, SelectedFilter);
                }
                catch
                {
                    MessageBox.Show(Properties.Resources.err_NavigatingToParentFolder, Properties.Resources.FileDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                return false;
            }
            //Path contains relative sgments - normalize it and combien with current
            else if (fileName.StartsWith("." + Path.DirectorySeparatorChar) ||
                    fileName.EndsWith(Path.DirectorySeparatorChar + ".") ||
                    fileName.StartsWith(".." + Path.DirectorySeparatorChar) ||
                    fileName.EndsWith(Path.DirectorySeparatorChar + "..") ||
                    fileName.IndexOf(Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar) >= 0 ||
                    fileName.IndexOf(Path.DirectorySeparatorChar + "." + Path.DirectorySeparatorChar) >= 0 ||

                    fileName.StartsWith("." + Path.AltDirectorySeparatorChar) ||
                    fileName.EndsWith(Path.AltDirectorySeparatorChar + ".") ||
                    fileName.StartsWith(".." + Path.AltDirectorySeparatorChar) ||
                    fileName.EndsWith(Path.AltDirectorySeparatorChar + "..") ||
                    fileName.IndexOf(Path.AltDirectorySeparatorChar + ".." + Path.AltDirectorySeparatorChar) >= 0 ||
                    fileName.IndexOf(Path.AltDirectorySeparatorChar + "." + Path.AltDirectorySeparatorChar) >= 0 ||

                    fileName.IndexOf(Path.DirectorySeparatorChar + ".." + Path.AltDirectorySeparatorChar) >= 0 ||
                    fileName.IndexOf(Path.DirectorySeparatorChar + "." + Path.AltDirectorySeparatorChar) >= 0 ||
                    fileName.IndexOf(Path.AltDirectorySeparatorChar + ".." + Path.DirectorySeparatorChar) >= 0 ||
                    fileName.IndexOf(Path.AltDirectorySeparatorChar + "." + Path.DirectorySeparatorChar) >= 0)
            {
                var newPath = fileName.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar).TrimEnd(Path.DirectorySeparatorChar);
                List<string> comb = new List<string>(currentDirectory.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar).TrimEnd(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar));
                foreach (var seg in newPath.Split(Path.DirectorySeparatorChar))
                {
                    if (seg == ".") continue;
                    else if (seg == ".." && comb.Count > 0) comb.RemoveAt(comb.Count - 1);
                    else comb.Add(seg);
                }
                return FollowPath(string.Join(Path.DirectorySeparatorChar.ToString(), comb.ToArray()), followLinks);
            }
            //Wildcards + directories (not supported)
            else if ((fileName.IndexOf('*') >= 0 || fileName.IndexOf('?') >= 0) && (fileName.IndexOf(Path.DirectorySeparatorChar) >= 0 || fileName.IndexOf(Path.AltDirectorySeparatorChar) >= 0))
            {
                MessageBox.Show(Properties.Resources.err_CombineWildcardsAndFolders, Properties.Resources.FileDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }
            //Wildcards - set filter
            else if (fileName.IndexOf('*') >= 0 || fileName.IndexOf('?') >= 0)
            {
                try
                {
                    LoadFolder(currentDirectory, fileName);
                }
                catch
                {
                    MessageBox.Show(string.Format(Properties.Resources.err_ApplyingFiler, fileName), Properties.Resources.FileDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                return false;
            }
            //Relative or absolute path
            else if (fileName.IndexOf(Path.DirectorySeparatorChar) >= 0 || fileName.IndexOf(Path.AltDirectorySeparatorChar) >= 0)
            {
                try
                {
                    if (!Path.IsPathRooted(fileName)) fileName = Path.Combine(currentDirectory, fileName);
                    if (File.Exists(fileName))
                    {
                        return AcceptFilename(fileName, followLinks);
                    }
                    else if (AddExtension && !string.IsNullOrEmpty(DefaultExt) &&
                        Path.GetExtension(fileName) == "" && File.Exists(fileName + (DefaultExt.StartsWith(".") ? "" : ".") + DefaultExt))
                    {
                        fileName = fileName + (DefaultExt.StartsWith(".") ? "" : ".") + DefaultExt;
                        return AcceptFilename(fileName, followLinks);
                    }
                    LoadFolder(fileName, SelectedFilter);
                }
                catch
                {
                    MessageBox.Show(string.Format(Properties.Resources.err_LoadDirectory, fileName), Properties.Resources.FileDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                return false;
            }
            //File or directory name
            else
            {
                try
                {
                    if (File.Exists(Path.Combine(currentDirectory, fileName)))
                    {
                        fileName = Path.Combine(currentDirectory, fileName);
                        return AcceptFilename(fileName, followLinks);
                    }
                    else if (Directory.Exists(Path.Combine(currentDirectory, fileName)))
                    {
                        fileName = Path.Combine(currentDirectory, fileName);
                        LoadFolder(fileName, SelectedFilter);
                    }
                    else if (AddExtension && !string.IsNullOrEmpty(DefaultExt) &&
                        File.Exists(fileName = Path.Combine(currentDirectory, fileName + (DefaultExt.StartsWith(".") ? "" : ".") + DefaultExt)))
                    {
                        return AcceptFilename(fileName, followLinks);
                    }
                    else
                    {
                        MessageBox.Show(string.Format(Properties.Resources.err_FileNotFound, fileName), Properties.Resources.FileDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                }
                catch
                {
                    MessageBox.Show(string.Format(Properties.Resources.err_InvalidFileName, fileName), Properties.Resources.FileDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                return false;
            }
        }

        /// <summary>Performs last checks before accepting file name from user and closing the dilaog. Also can follow *.lnk links.</summary>
        /// <param name="fileName">Full path of file to be accepted</param>
        /// <param name="followLinks">True to follow *.lnk links</param>
        /// <returns>True when file was accepted, false when it represented link to folder or invalid link (when <paramref name="followLinks"/> is true) of nonexisten file (when <see cref="CheckFileExists"/> is true) or recursive link (when <paramref name="followLinks"/> is false, <see cref="FollowLinks"/> is true and <paramref name="fileName"/> is *.lnk file).</returns>
        private bool AcceptFilename(string fileName, bool followLinks)
        {
            if (followLinks && Path.GetExtension(fileName).ToLower(System.Globalization.CultureInfo.InvariantCulture) == ".lnk")
            {
                try
                {
                    ShellLink link = new ShellLink(fileName);
                    return FollowPath(link.File, false);
                }
                catch { return false; }
            }
            else if (FollowLinks && !followLinks && Path.GetExtension(fileName).ToLower(System.Globalization.CultureInfo.InvariantCulture) == ".lnk")
            {
                return false;
            }
            else
            {
                if (!CheckFileExists || File.Exists(fileName))
                {
                    FileName = fileName;
                    return true;
                }
                return false;
            }
        }

        private void txtFileName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) Close();
        }

        /// <summary>Raises the <see cref="System.Windows.Forms.Control.KeyDown"/> event.</summary>
        /// <param name="e">A <see  cref="System.Windows.Forms.KeyEventArgs"/> that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Escape || e.KeyCode == (Keys)166 /*Keys.BrowserBack*/) {
                allowClose = true;
                DialogResult = DialogResult.Cancel;
                Close();
                e.Handled = true;
            } else if (e.KeyCode == Keys.Enter) {
                Close();
                e.Handled = true;
            }
        }

        private void lvwFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwFiles.SelectedIndices.Count > 0)
                txtFileName.Text = lvwFiles.Items[lvwFiles.SelectedIndices[0]].Text;
        }

        private void lvwFiles_ItemActivate(object sender, EventArgs e)
        {
            var oldAppend = AddExtension;
            AddExtension = false;
            try
            {
                Close();
            }
            finally
            {
                AddExtension = oldAppend;
            }
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loading) {
                FilterIndex = cmbFilter.SelectedIndex;
                LoadFolder(currentDirectory, SelectedFilter);
            }
        }

        private void cmnView_Popup(object sender, EventArgs e)
        {
            mniLargeIcons.Checked = View == View.LargeIcon;
            mniSmallIcons.Checked = View == View.SmallIcon;
            mniDetails.Checked = View == View.Details;
            mniList.Checked = View == View.List;
        }

        private void mniView_Click(object sender, EventArgs e)
        {
            if (sender == mniLargeIcons) View = View.LargeIcon;
            else if (sender == mniSmallIcons) View = View.SmallIcon;
            else if (sender == mniDetails) View = View.Details;
            else if (sender == mniList) View = View.List;
        }

        /// <summary>Index of column files are currently sored by. -1 for no sort.</summary>
        private int sortIndex = -1;
        /// <summary>True when current sort order is descending, true for ascending</summary>
        private bool descending = false;
        private void lvwFiles_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (sortIndex == e.Column) descending = !descending;
            else { sortIndex = e.Column; descending = false; }
            Sort();
        }

        /// <summary>Sorts items in list view</summary>
        private void Sort()
        {
            Comparison<ListViewItem> sorter;
            if(sortIndex == lvwFiles.Columns.IndexOf(cohName))
                sorter = delegate(ListViewItem a,ListViewItem b){ 
                    if(a.Tag is DirectoryInfo && b.Tag is FileInfo ) return -1;
                    if(a.Tag is FileInfo  && b.Tag is DirectoryInfo  ) return 1;
                    return (descending?-1:1)*StringComparer.CurrentCultureIgnoreCase.Compare(a.Text,b.Text);
                };
            else   if(sortIndex == lvwFiles.Columns.IndexOf(cohDate ))
                  sorter = delegate(ListViewItem a,ListViewItem b){ 
                    if(a.Tag is DirectoryInfo && b.Tag is FileInfo ) return -1;
                    if(a.Tag is FileInfo  && b.Tag is DirectoryInfo  ) return 1;
                    return (descending ? -1 : 1) * DateTime.Compare(((FileSystemInfo)a.Tag).LastWriteTime, ((FileSystemInfo)b.Tag).LastWriteTime);
                };
            else if(sortIndex == lvwFiles.Columns.IndexOf(cohSize  ))
                  sorter = delegate(ListViewItem a,ListViewItem b){ 
                    if(a.Tag is DirectoryInfo && b.Tag is FileInfo ) return -1;
                    if(a.Tag is FileInfo  && b.Tag is DirectoryInfo  ) return 1;
                    if(a.Tag is DirectoryInfo )return 0;
                    if (((FileInfo)a.Tag).Length < ((FileInfo)a.Tag).Length) return descending?1:-1;
                    if (((FileInfo)a.Tag).Length > ((FileInfo)a.Tag).Length) return descending ? -1 : 1;
                      return 0;
                };
            else   if(sortIndex == lvwFiles.Columns.IndexOf(cohSize  ))
                      sorter = delegate(ListViewItem a, ListViewItem b)
                      {
                          if (a.Tag is DirectoryInfo && b.Tag is FileInfo) return -1;
                          if (a.Tag is FileInfo && b.Tag is DirectoryInfo) return 1;
                          return (descending?-1:1)* StringComparer.CurrentCultureIgnoreCase.Compare(a.SubItems[lvwFiles.Columns.IndexOf(cohSize)].Text, b.SubItems[lvwFiles.Columns.IndexOf(cohSize)].Text);
                      };
            else
                sorter = delegate(ListViewItem a, ListViewItem b)
                {
                    if (a.Tag is DirectoryInfo && b.Tag is FileInfo) return -1;
                    if (a.Tag is FileInfo && b.Tag is DirectoryInfo) return 1;
                    return 0;
                };
            List<ListViewItem> items = new List<ListViewItem>(lvwFiles.Items.Count);
            foreach (ListViewItem item in lvwFiles.Items) items.Add(item);
            items.Sort(sorter);
            lvwFiles.Items.Clear();
            foreach (var item in items) lvwFiles.Items.Add(item);
        }

        private void cmdViewMode_Click(object sender, EventArgs e)
        {
            cmnView.Show(cmdViewMode, new Point(0,cmdViewMode.Height));
        }

        private void cmdUp_Click(object sender, EventArgs e)
        {
            FollowPath("..", false);
        }

        private void lvwFiles_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Back) {
                FollowPath("..", false);
                e.Handled = true;
            } else if (e.KeyCode == Keys.Delete ) {
                if(lvwFiles.SelectedIndices.Count > 0 && MessageBox.Show(Properties.Resources.ConfirmDelete, Properties.Resources.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes) {
                    foreach (int index in lvwFiles.SelectedIndices) {
                        var fsi = (FileSystemInfo)lvwFiles.Items[index].Tag;
                        try {
                            fsi.Delete();
                        } catch (Exception ex) {
                            MessageBox.Show(string.Format(Properties.Resources.err_Delete, fsi.FullName, ex.Message));
                        }
                    }
                    LoadFolder(currentDirectory, SelectedFilter);
                }
                e.Handled = true;
            }
        }

    }
}
