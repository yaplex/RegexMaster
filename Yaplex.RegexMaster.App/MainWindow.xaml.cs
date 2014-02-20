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
            
            _vm = new PresentationViewModel();
            DataContext = _vm;
            var doc = new FlowDocument(new Paragraph(new Run(_vm.SourceText)));
            RichTextToParce.Document = doc;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _vm.Parse();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.Parse();
            RichTextToParce.Document = _vm.ParsedDocument;
        }
    }
}