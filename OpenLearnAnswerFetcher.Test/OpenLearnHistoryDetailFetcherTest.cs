using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using OpenLearnAnswerFetcher.Business;

namespace OpenLearnAnswerFetcher.Test
{
    [TestClass]
    public class OpenLearnHistoryDetailFetcherTest
    {
        [TestMethod]
        public void TestFetch()
        {

            string jsonString;
            using (var stream = File.OpenText("./Json.txt"))
            {
                jsonString = stream.ReadToEnd();
            }
            var fetcher = new OpenLearnHistoryDetailFetcher(jsonString);
            var obj = fetcher.Fetch();
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void TestAnalysis()
        {

            string jsonString;
            using (var stream = File.OpenText("./Json.txt"))
            {
                jsonString = stream.ReadToEnd();
            }
            var analyser = new AnswerAnalyser(jsonString);
            var obj = analyser.Analyse();
            Assert.IsNotNull(obj);
        }
    }
}
