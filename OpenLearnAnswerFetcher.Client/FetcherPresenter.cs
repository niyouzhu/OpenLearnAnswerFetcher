using OpenLearnAnswerFetcher.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Client
{
    public class FetcherPresenter
    {

        public FetcherPresenter(IFetcherView view, FetcherViewModel model)
        {
            View = view;
            Model = model;
        }

        public IFetcherView View { get; }

        public FetcherViewModel Model { get; }


        public IEnumerable<FetcherViewModel> Analyse()
        {
            var jsonString = View.GetJsonString();
            return FetcherViewModel.GetResults(jsonString);
        }

        public string GetSavedFilePath()
        {
            return View.GetSavedFilePath;
        }


        public bool Save()
        {
            var results = Analyse();
            using (var stream = ExcelHelper.Export(FetcherExcelViewModel.ConvertFrom(results)))
            {
                stream.SaveAsFile(GetSavedFilePath());
                return true;
            }
        }
    }
}
