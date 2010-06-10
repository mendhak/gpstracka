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
    /// <summary>Foldeer browse dialog</summary>
    public partial class FolderBrowseDialog : Form
    {
        private Dictionary<string, int> imageIndexes = new Dictionary<string, int>();
        /// <summary>CTor</summary>
        public FolderBrowseDialog()
        {
            InitializeComponent();
            imlImages.ImageSize = OpenNETCF.Windows.Forms.SystemInformation2.SmallIconSize;

            tvwTree.Nodes[0].Expand();
        }

        private void tvwTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].ImageIndex < 0)
            {
                e.Node.Nodes.Clear();
                string[] directories;
                try
                {
                    directories = Directory.GetDirectories(e.Node.FullPath.Replace("\\\\", "\\"));
                }
                catch { e.Cancel = true; return; }
                foreach (string folder in directories)
                {
                    var node = e.Node.Nodes.Add(Path.GetFileName(folder));
                    int iindex=0;
                    if (imageIndexes.ContainsKey(folder))
                    {
                        iindex = imageIndexes[folder];
                    }
                    else
                    {
                        Icon icon = null;
                        try
                        {
                            icon = Tools.IOt.FileSystemTools.GetIcon(folder, false, true);
                            if (icon != null)
                            {
                                imlImages.Images.Add(icon);
                                iindex = imlImages.Images.Count - 1;
                                imageIndexes.Add(folder, iindex);
                            }
                        }
                        catch { }
                    }
                    node.ImageIndex = node.SelectedImageIndex = iindex;
                    var subnode = node.Nodes.Add(Properties.Resources.PleaseWait);
                    subnode.ImageIndex = -1;
                    subnode.SelectedImageIndex = -2;
                    subnode.ForeColor = SystemColors.GrayText;
                }
            }
        }

        //private void tvwTree_AfterExpand(object sender, TreeViewEventArgs e)
        //{
        //    e.Node.ImageIndex = 1;
        //    e.Node.SelectedImageIndex = 1;
        //}

        //private void tvwTree_AfterCollapse(object sender, TreeViewEventArgs e)
        //{
        //    e.Node.ImageIndex = e.Node.SelectedImageIndex = imageIndexes.ContainsKey(e.Node.FullPath.Replace("\\\\", "\\")) ? imageIndexes[e.Node.FullPath.Replace("\\\\", "\\")] : 0;
        //}
        /// <summary>Gets or sest path of currently selecte folder</summary>
        /// <value>When path baing set dos not exists (or it is not folder); the neares existing parent is selected.</value>
        public string SelectedPath
        {
            get { return tvwTree.SelectedNode == null ? null : tvwTree.SelectedNode.FullPath.Replace("\\\\", "\\"); }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (!Path.IsPathRooted(value)) throw new ArgumentException(Properties.Resources.err_PathMustBeRooted);
                string newPath = value.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
                string[] segments = newPath.Split(Path.DirectorySeparatorChar);
                TreeNode node = tvwTree.Nodes[0];
                int i = 0;
                foreach (string segment in segments)
                {
                    if (i == 0 && segment == "") continue;
                    node.Expand();
                    bool found = false;
                    foreach (TreeNode subnode in node.Nodes)
                    {
                        if (subnode.Text.ToLower() == segment.ToLower())
                        {
                            node = subnode;
                            found = true;
                            break;//inner for
                        }
                    }
                    if (!found) break;
                    i += 1;
                }
                tvwTree.SelectedNode = node;
                node.EnsureVisible();
            }
        }

        private void mniOK_Click(object sender, EventArgs e)
        {
            if (tvwTree.SelectedNode == null)
            {
                MessageBox.Show(Properties.Resources.err_SelectFolderPlease);
                return;
            }
            allowClose = true;
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        private void mniCancel_Click(object sender, EventArgs e)
        {
            allowClose = true;
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }
        /// <summary>Indicates that form closing is caused by menu click and <see cref="Form.Closing"/> event should not be handled.</summary>
        private bool allowClose = false;
        private void FolderBrowseDialog_Closing(object sender, CancelEventArgs e)
        {
            if (!allowClose && tvwTree.SelectedNode == null)
            {
                MessageBox.Show(Properties.Resources.err_SelectFolderPlease);
                e.Cancel = true;
            }
            else if (!allowClose)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}