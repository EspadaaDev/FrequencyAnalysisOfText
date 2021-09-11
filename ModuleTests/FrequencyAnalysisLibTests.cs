using FrequencyAnalysisLib;
using NUnit.Framework;

namespace ModuleTests
{
    [TestFixture]
    public class Tests
    {
        private TextDocument _document;

        [SetUp]
        public void Setup()
        {
            _document = new TextDocument();
        }

        [Test]
        public void SetContent_Input_ReturnTrue()
        {            
            var result = _document.SetContent("abc");
            Assert.IsTrue(result);
        }

        [Test]
        public void SetContent_Input_ReturnFalse()
        {            
            var result = _document.SetContent(null);
            Assert.IsFalse(result);
        }
        
        [Test]
        public void AnalyzeTripletFrequency_Output1()
        {
            string triplet = "qwe";
            _document.SetContent(triplet);
            var result = _document.AnalyzeTripletFrequency();
            Assert.IsTrue(result[triplet] == 1);
        } 
        
        [Test]
        public void AnalyzeTripletFrequency_Output2()
        {
            string triplet = "qwe qwe";
            _document.SetContent(triplet);
            var result = _document.AnalyzeTripletFrequency();
            Assert.IsTrue(result["qwe"] == 2);
        }

        [Test]
        public void AnalyzeTripletFrequency_Output3()
        {
            string triplet = "qwe qwe rrr rrr rrr";
            _document.SetContent(triplet);
            var result = _document.AnalyzeTripletFrequency();
            Assert.IsTrue(result["qwe"] == 2 && result["rrr"] == 3);
        }

        [Test]
        public void AnalyzeTripletFrequency_Output4()
        {
            string triplet = "";
            _document.SetContent(triplet);
            var result = _document.AnalyzeTripletFrequency();
            Assert.IsTrue(result.Count == 0);
        }

        [Test]
        public void AnalyzeTripletFrequency_Output5()
        {
            string triplet = "www2www.www,www)www-www+www#www*";
            _document.SetContent(triplet);
            var result = _document.AnalyzeTripletFrequency();
            Assert.IsTrue(result["www"] == 8);
        }
    }
}