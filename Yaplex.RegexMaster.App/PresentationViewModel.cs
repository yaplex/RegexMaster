using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using Yaplex.RegexMaster.Business.Annotations;
using Yaplex.RegexMaster.Core;

namespace Yaplex.RegexMaster.App
{
    public class PresentationViewModel : INotifyPropertyChanged
    {
        public PresentationViewModel()
        {
            // load properties form somewhere
            RegexPattern = "hello";
            SourceText = "Hello world from my \"hello world\" application";
        }
        private string _regexPattern;

        public string RegexPattern
        {
            get { return _regexPattern; }
            set
            {
                if (value == _regexPattern) return;
                _regexPattern = value;
                OnPropertyChanged();
            }
        }

        public FlowDocument ParsedDocument { get { return _parser.ParsingResult; }}
        private Parser _parser;
        private string _sourceText;

        public void Parse()
        {
            _parser = new Parser(){Text = SourceText, Pattern = RegexPattern};
            _parser.Parse();
        }

        public string SourceText
        {
            get { return _sourceText; }
            set
            {
                if(_sourceText == value) return;
                _sourceText = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}