using System;

using System.Collections.Generic;
using System.Text;

namespace GPSTracka
{
    /// <summary>Represents Shall Link (a LNK file)</summary>
    public sealed class ShellLink
    {
        private string file;
        /// <summary>Gets or sets path of file this link links to</summary>
        /// <exception cref="ArgumentException">Value being set contains double quote (")</exception>
        public string File
        {
            get
            {
                return file;
            }
            set
            {
                if (file.IndexOf("\"") >= 0) throw new ArgumentException(Properties.Resources.err_QuotesInPath, "value");
                file = value;
            }
        }
        /// <summary>Gets or sets optional command line arguments for link</summary>
        public string Arguments { get; set; }
        /// <summary>CTor - creates a new instance of the <see cref="ShellLink"/> class repersenting an empty link</summary>
        public ShellLink(){}
        /// <summary>CTot - creates a new instance of the <see cref="ShellLink"/> class form exisiting *.lnk file</summary>
        /// <param name="path">Path to existing *.lnk file</param>
        /// <exception cref="System.IO.IOException">The link file is invalid (unrecognized format) -or- An I/O error occurs.</exception>
        /// <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <exception cref="System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="path"/> is null.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified <paramref name="path"/> is invalid, (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file specified in <paramref name="path"/> was not found.</exception>
        /// <exception cref="System.NotSupportedException"><paramref name="path"/> is in an invalid format.</exception>
        /// <exception cref="System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        public ShellLink(string path)
        {
            using (var lnk = System.IO.File.OpenText(path))
            {
                string data = lnk.ReadToEnd();
                int pathStart = data.IndexOf('#' )+1;
                if (pathStart == 0) throw new System.IO.IOException(Properties.Resources.err_InvalidLinkFile_Hash);
                if (data.Length <= pathStart) File = "";
                else if (data[pathStart] == '"')
                {
                    int pathEnd = data.IndexOf('"', pathStart +1)-1;
                    if (pathEnd < 0) throw new System.IO.IOException(Properties.Resources.err_InvalidLinkFile_Quote);
                    File = data.Substring(pathStart + 1, pathEnd - pathStart);
                    if (data.Length > pathEnd + 1 && data[pathEnd + 1] == ' ') Arguments = data.Substring(pathEnd + 2);
                }
                else if (data.IndexOf(' ', pathStart) >= 0)
                {
                    File = data.Substring(pathStart, data.IndexOf(' ', pathStart) - pathStart);
                    Arguments = data.Substring(data.IndexOf(' ', pathStart) + 1);
                }
                else File = data.Substring(pathStart);
            }
        }
        /// <summary>Saves a link to given *.lnk file</summary>
        /// <param name="path">Path of file to save link to</param>
        /// <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <exception cref="System.ArgumentException"><paramref name="path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="path"/> is null.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified <paramref name="path"/> is invalid, (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file specified in <paramref name="path"/> was not found.</exception>
        /// <exception cref="System.NotSupportedException"><paramref name="path"/> is in an invalid format.</exception>
        /// <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        public void Save(string path)
        {
            using (var lnk = System.IO.File.Open(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                using (var w = new System.IO.StreamWriter(lnk, System.Text.Encoding.Default))
                {
                    var file = File ?? "";
                    var arguments = Arguments ?? "";
                    w.Write((file.Length + arguments.Length + (file.IndexOf(' ') >= 0 ? 2 : 0) + ((file.Length > 0 && arguments.Length > 0) ? 1 : 0)).ToString(System.Globalization.CultureInfo.InvariantCulture));
                    w.Write("#");
                    if (file.IndexOf(' ') >= 0) w.Write("\"");
                    w.Write(file);
                    if (file.IndexOf(' ') >= 0) w.Write("\"");
                    if (file.Length > 0 && arguments.Length > 0) w.Write(" ");
                    w.Write(arguments);
                }
            }
        }

    }
}
