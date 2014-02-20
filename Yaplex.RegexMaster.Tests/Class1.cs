using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using NUnit.Framework;
using Yaplex.RegexMaster.App;
using Yaplex.RegexMaster.Business;

namespace Yaplex.RegexMaster.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Should_Produce_Document_Simple_word()
        {
            var source = "Hello world from my \"hello world\" application";
            var pattern = "hello";
            var vm = new PresentationViewModel();
            vm.Parse();
            Assert.AreEqual(source, vm.SourceText);
            Assert.AreEqual(pattern, vm.RegexPattern);
            Assert.AreEqual(source, new TextRange(vm.ParsedDocument.ContentStart, vm.ParsedDocument.ContentEnd).Text.Trim());
        }
    }
}
