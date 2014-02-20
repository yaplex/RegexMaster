using System.Text.RegularExpressions;
using System.Windows.Documents;

namespace Yaplex.RegexMaster.Core
{
    public class Parser
    {
        private FlowDocument _parsingResult;
        public string Pattern { get; set; }
        public string Text { get; set; }

        public FlowDocument ParsingResult
        {
            get { return _parsingResult; }
        }

        public void Parse()
        {
            _parsingResult = new FlowDocument();

            Match m = Regex.Match(Text, Pattern);
            BuildSelection(Text, _parsingResult, m);
        }

        private void BuildSelection(string content, FlowDocument selectionDoc, Match m)
        {
            var p = new Paragraph();
            int lastPosition = 0;
            while (m.Success)
            {
                p.Inlines.Add(new Run(content.Substring(lastPosition, m.Index)));
                p.Inlines.Add(new Bold(new Run(content.Substring(m.Index, m.Length))));
                lastPosition = m.Index + m.Length;

                if (!m.NextMatch().Success)
                {
                    p.Inlines.Add(new Run(content.Substring(lastPosition, content.Length - lastPosition)));
                }


                selectionDoc.Blocks.Add(p);

                m = m.NextMatch();
            }
        }
    }
}