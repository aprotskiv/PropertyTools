// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileDialogService.cs" company="PropertyTools">
//   Copyright (c) 2014 PropertyTools contributors
// </copyright>
// <summary>
//   The file dialog interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PropertyTools.Wpf
{
    /// <summary>
    /// The file dialog interface.
    /// </summary>
    public interface IFileDialogService
    {
		/// <summary>
		/// Shows the open file dialog.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <param name="filter">The filter.</param>
		/// <param name="defaultExtension">The default extension.</param>
		/// <returns>
		/// Non-NULL if the user pressed ok.
		/// </returns>
		string ShowOpenFileDialog(string filename, string filter, string defaultExtension, 
			string title = null,
			bool checkFileExists = false, 
			System.Windows.Window owner = null);

		/// <summary>
		/// Shows the open files dialog.
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <param name="defaultExtension">The default extension.</param>
		/// <returns>
		/// Non-NULL if the user pressed ok.
		/// </returns>
		string[] ShowOpenFilesDialog(string filter, string defaultExtension,
			string title = null,
			System.Windows.Window owner = null);

		/// <summary>
		/// Shows the save file dialog.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <param name="filter">The filter.</param>
		/// <param name="defaultExtension">The default extension.</param>
		/// <returns>
		/// Non-NULL if the user pressed ok.
		/// </returns>
		string ShowSaveFileDialog(string filename, string filter, string defaultExtension,
			string title = null,
			System.Windows.Window owner = null);
	}
}