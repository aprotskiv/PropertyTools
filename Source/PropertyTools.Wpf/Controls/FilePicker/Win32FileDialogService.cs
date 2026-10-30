using Microsoft.Win32;
using System.Linq;

namespace PropertyTools.Wpf.Controls.FilePicker
{
	public class Win32FileDialogService : IFileDialogService
	{
		public virtual string ShowOpenFileDialog(string filename, string filter, string defaultExtension,
			string title = null,
			bool checkFileExists = false,
			System.Windows.Window owner = null)
		{
			var d = new OpenFileDialog
			{
				FileName = filename,
				Filter = filter,
				DefaultExt = defaultExtension,
				Multiselect = false,
				Title = title ?? string.Empty,
			};
			if (true == d.ShowDialog(owner))
			{
				return d.FileName;
			}

			return null;
		}

		public virtual string[] ShowOpenFilesDialog(string filter, string defaultExtension,
			string title = null,
			System.Windows.Window owner = null)
		{
			var d = new OpenFileDialog
			{
				Filter = filter,
				DefaultExt = defaultExtension,
				Multiselect = true,
				Title = title ?? string.Empty,
			};

			if (true == d.ShowDialog(owner))
			{
				return d.FileNames;
			}
			return null;
		}

		public virtual string ShowSaveFileDialog(string filename, string filter, string defaultExtension,
			string title = null,
			System.Windows.Window owner = null)
		{
			var d = new SaveFileDialog
			{
				FileName = filename,
				Filter = filter,
				DefaultExt = defaultExtension,
				Title = title ?? string.Empty,
			};

			if (true == d.ShowDialog(owner))
			{
				return d.FileName;
			}

			return null;
		}
	}
}
