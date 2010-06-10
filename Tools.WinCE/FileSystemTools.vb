Imports Tools.API, System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text

Namespace IOt
    ''' <summary>Contains file system-related methods and extension methods</summary>
    ''' <version version="1.5.2">Renamed from FileTystemTools to FileSystemTools</version>
    Public Module FileSystemTools
        ''' <summary>Gets icon for given file or folder</summary>
        ''' <param name="Path">Path to get icon for</param>
        ''' <param name="Large">True to get large icon (false to get small icon)</param>
        ''' <returns>Icon that represents given file or folder in operating system</returns>
        ''' <param name="Overlays">True to add all applicable overlay icons</param>
        ''' <exception cref="IO.FileNotFoundException">File <paramref name="Path"/> does not exists.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Path"/> is null</exception>
        Public Function GetIcon(ByVal Path As String, Optional ByVal Large As Boolean = False, Optional ByVal Overlays As Boolean = True) As Drawing.Icon
            If Path Is Nothing Then Throw New ArgumentNullException("Path")
            Path = Path.TrimEnd("\")
            If Not IO.Directory.Exists(Path) AndAlso Not (IO.File.Exists(Path)) Then _
                Throw New IO.FileNotFoundException(String.Format(My.Resources.PathDoesNotExist, Path))
            Dim shInfo As New SHFILEINFO
            Dim ret = SHGetFileInfo(Path, 0, shInfo, Marshal.SizeOf(shInfo), _
                FileInformationFlags.SHGFI_ICON Or If(Large, FileInformationFlags.SHGFI_LARGEICON, FileInformationFlags.SHGFI_SMALLICON) Or If(Overlays, FileInformationFlags.SHGFI_ADDOVERLAYS, CType(0, FileInformationFlags)))
            If shInfo.hIcon = IntPtr.Zero Then Return Nothing
            Dim icon = Drawing.Icon.FromHandle(shInfo.hIcon)
            Return icon
        End Function

    End Module

End Namespace