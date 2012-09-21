namespace RegexMaster
{
	using System;
	using System.Drawing;
	using System.Text.RegularExpressions;
	using System.Windows;
	using System.Windows.Input;
	using Microsoft.Windows.Controls.Ribbon;
	using System.Text;

	/// <summary>
	///   Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : RibbonWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}


		private void OnDoRegexEvent(object sender, KeyEventArgs e)
		{
			SaveAllData();
			ProcessRegex();
		}

		private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
		{
			LoadControlsValuesFromPreviousSession();
			OnDoRegexEvent(sender, null);
		}

		private void LoadControlsValuesFromPreviousSession()
		{
			LoadAllData();
			ProcessRegex();
		}

		private void ProcessRegex()
		{
			string pattern = txtRegexExpression.Text;
			if (string.IsNullOrEmpty(pattern))
			{
				return;
			}
			SetDefaultView();

			txtSplit.Text = string.Empty;
			string searchText = richSourceText.Text;
			RegexOptions options = GetRegexOptions();

			string[] result;
			try
			{
				result = Regex.Split(searchText,
									 pattern,
									 options);
			}
			catch (Exception)
			{
				txtSplit.Text = "Error while parsing...";
				return;
			}

			var sb = new StringBuilder();
			foreach (string s in result)
			{
				sb.AppendLine(s);
			}
			txtSplit.Text = sb.ToString();

			Match m = Regex.Match(searchText,
								  pattern,
								  options);

			while (m.Success)
			{
				SelectTextInRichEditor(m);
				m = m.NextMatch();
			}
		}

		private RegexOptions GetRegexOptions()
		{
			RegexOptions options = RegexOptions.Singleline;
			if (null != isMultiline.IsChecked && isMultiline.IsChecked.Value)
			{
				options = RegexOptions.Multiline;
			}

			if (null != ignoreCaseCheckBox.IsChecked && ignoreCaseCheckBox.IsChecked.Value)
			{
				options = options | RegexOptions.IgnoreCase;
			}

			return options;
		}

		private void SelectTextInRichEditor(Capture m)
		{
			richSourceText.Select(m.Index, m.Length);
			richSourceText.SelectionColor = Color.Red;
			richSourceText.SelectionBackColor = Color.Yellow;
		}

		private void SetDefaultView()
		{
			richSourceText.SelectAll();
			richSourceText.SelectionColor = Color.Black;
			richSourceText.SelectionFont = new Font("Arial", 12, System.Drawing.FontStyle.Regular);
			richSourceText.SelectionBackColor = Color.White;
		}


		private void SaveAllData()
		{
			if (!string.IsNullOrEmpty(txtRegexExpression.Text))
			{
				UserInputSettings.Default.RegexPattern = txtRegexExpression.Text;
			}


			if (!string.IsNullOrEmpty(richSourceText.Text))
			{
				UserInputSettings.Default.SourceText = richSourceText.Text;
			}

			UserInputSettings.Default.Save();
		}

		private void LoadAllData()
		{
			txtRegexExpression.Text = UserInputSettings.Default.RegexPattern;

			if (!string.IsNullOrEmpty(UserInputSettings.Default.SourceText))
			{
				richSourceText.Text = UserInputSettings.Default.SourceText;
			}
		}

		private void Application_Exit(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void ClearTextFields_Click(object sender, RoutedEventArgs e)
		{
			txtSplit.Text = string.Empty;
			txtRegexExpression.Text = string.Empty;
			richSourceText.Text = string.Empty;
		}

		private void Navigate_to_Website(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://alexandershapovalov.com/");
		}

		private void MenuDoRegex_Click(object sender, RoutedEventArgs e)
		{
			OnDoRegexEvent(sender, null);
		}

		void ReplaceSelectionWithCommandParameter(object sender, ExecutedRoutedEventArgs e)
		{
			var textBox = (System.Windows.Controls.TextBox)sender;
			textBox.SelectedText = e.Parameter as string;
			textBox.Focus();
		}
	}
}