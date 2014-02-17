using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Yaplex.RegexMaster.Business;

namespace Yaplex.RegexMaster.App
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PresentationViewModel _vm;
        public MainWindow()
        {
            InitializeComponent();
            var doc = new FlowDocument(new Paragraph(new Run("This is richbox!")));
            _vm = new PresentationViewModel(){RegexPattern = "load from last use"};
            DataContext = _vm;
            RichTextToParce.Document = doc;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RunRegex();
        }

        private void RunRegex()
        {
            if (!ApplicationReady()) return;

            string regexPattern = _vm.RegexPattern;
            FlowDocument originalDoc = RichTextToParce.Document;
            string content = new TextRange(originalDoc.ContentStart, originalDoc.ContentEnd).Text;
            var selectionDoc = new FlowDocument();

            Match m = Regex.Match(content,
                regexPattern);

            while (m.Success)
            {
                BuildSelection(content, selectionDoc, m);
                m = m.NextMatch();
                RichTextToParce.Document = selectionDoc;
            }
        }

        private bool ApplicationReady()
        {
            return null != RichTextToParce;
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