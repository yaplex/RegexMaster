using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using Yaplex.RegexMaster.Business.Annotations;

namespace Yaplex.RegexMaster.Business
{
    public class PresentationViewModel : INotifyPropertyChanged
    {
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

        public FlowDocument Document { get; set; }

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