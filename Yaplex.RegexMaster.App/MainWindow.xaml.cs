using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Yaplex.RegexMaster.App
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RunRegex();
        }

        private void RunRegex()
        {
            if (null == RichTextToParce) return;

            string regexPattern = TxtPattern.Text;
            FlowDocument originalDoc = RichTextToParce.Document;
            string content = new TextRange(originalDoc.ContentStart, originalDoc.ContentEnd).Text;
            var selectionDoc = new FlowDocument();

            Match m = Regex.Match(content,
                regexPattern);

            while (m.Success)
            {
                BuildSelection(content, selectionDoc, m);
                m = m.NextMatch();
            }

            RichTextToParce.Document = selectionDoc;
        }

        private void BuildSelection(string content, FlowDocument selectionDoc, Match m)
        {
            var p = new Paragraph();
            p.Inlines.Add(new Run(content.Substring(0, m.Index)));
            p.Inlines.Add(new Bold(new Run(content.Substring(m.Index, m.Length))));
            p.Inlines.Add(new Run(content.Substring(m.Index+m.Length, content.Length-1-m.Index-m.Length)));

            selectionDoc.Blocks.Add(p);

        }
    }
}