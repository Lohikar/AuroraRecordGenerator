using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;
using System.Linq;

namespace AuroraRecordGenerator
{
	/// <summary>
	/// Interaction logic for RecordEditor.xaml
	/// </summary>
	public partial class RecordEditor
	{
		public RecordEditor()
		{
			// Initialize the record object used for storage and generation
			Data = new Record();
			DataContext = Data;
			ProtoBuf.Serializer.PrepareSerializer<GenderType>();
			ProtoBuf.Serializer.PrepareSerializer<SpeciesType>();
			ProtoBuf.Serializer.PrepareSerializer<Record>();
			InitializeComponent();
			SubSpeciesCombo.ItemsSource = GetSpeciesOptions();
		}

		private Record Data { get; set; }
		private string _currentFilePath;


		private void SpeciesSelectChanged(object sender, SelectionChangedEventArgs e)
		{
			if (SpeciesCombo.SelectionBoxItem == null)
				return;

			var type = (SpeciesType) SpeciesCombo.SelectedValue;

			switch (type)
			{
				// non-gendered species
				case SpeciesType.Diona:
				case SpeciesType.IPC:
				case SpeciesType.Vaurca:
					Debug.WriteLine("Disabled GenderCombo, type is " + type);
					GenderCombo.IsEnabled = false;
					GenderCombo.Text = "N/A";
					break;
				// gendered species
				case SpeciesType.Human:
				case SpeciesType.Skrell:
				case SpeciesType.Tajara:
				case SpeciesType.Unathi:
					Debug.WriteLine("Enabled GenderCombo, type is " + type);
					GenderCombo.IsEnabled = true;
					break;
				case SpeciesType.None:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			SubSpeciesCombo.ItemsSource = GetSpeciesOptions(type);
		}

		private void WindowLoaded(object sender, RoutedEventArgs e)
		{
			SpeciesCombo.SelectedIndex = 0;
		}

		private void GenerateRecord(object sender, RoutedEventArgs e)
		{
			// THIS IS IT
			var wnd = new GeneratedResultWindow(Data);
			wnd.Show();
		}

		private async void SaveContent(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(_currentFilePath))
				SaveContentAs(null, null);
			else
			{
				// have a path, attempt to save to it
				if (!File.Exists(_currentFilePath))
				{
					switch (
						await
							this.ShowMessageAsync("File Error",
								"Current file missing, renamed or deleted. Do you want to save as another name?", 
								MessageDialogStyle.AffirmativeAndNegative))
					{
						case MessageDialogResult.Negative:
							_currentFilePath = null;
							return;
						case MessageDialogResult.Affirmative:
							SaveContentAs(null, null);
							return;
						default:
							throw new ArgumentOutOfRangeException();
					}
				}

				var fs = File.Open(_currentFilePath, FileMode.Truncate);
				ProtoBuf.Serializer.Serialize(fs, Data);
			}
		}

		private void OpenContent(object sender, RoutedEventArgs e)
		{
			var dialog = new Microsoft.Win32.OpenFileDialog
			{
				AddExtension = true,
				CheckFileExists = true,
				CheckPathExists = true,
				Filter = "Character Profiles (*.ss13prof)|*.ss13prof|All Files (*.*)|*.*"
			};

			if (!(dialog.ShowDialog() ?? false)) return;

			var fs = File.Open(dialog.FileName, FileMode.Open);
			Data = ProtoBuf.Serializer.Deserialize<Record>(fs);
			_currentFilePath = dialog.FileName;
			// So WPF updates bindings
			DataContext = Data;
		}

		private void SaveContentAs(object sender, RoutedEventArgs e)
		{
			var dialog = new Microsoft.Win32.SaveFileDialog
			{
				AddExtension = true, CheckPathExists = true, Filter = "Character Profiles (*.ss13prof)|*.ss13prof|All Files (*.*)|*.*"
			};
			if (!(dialog.ShowDialog() ?? false)) return;
			var fs = File.Open(dialog.FileName, FileMode.Create);
			ProtoBuf.Serializer.Serialize(fs, Data);
			_currentFilePath = dialog.FileName;
		}

		public static IList<string> GetSpeciesOptions()
		{
			return Enum.GetValues(typeof(SpeciesSubType)).Cast<SpeciesSubType>().Select(item => Utility.SubspeciesNiceName(item)).ToList();
		}

		public static IList<string> GetSpeciesOptions(SpeciesType limitTo)
		{
			return Enum.GetValues(typeof(SpeciesSubType)).Cast<SpeciesSubType>().Select(item => Utility.SubspeciesNiceName(item)).ToList();
		}
	}
}
