using OpenLearnAnswerFetcher.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Client
{
    public class Presenter
    {

        public Presenter(IView view, ViewModel model)
        {
            View = view;
            Model = model;
        }

        public IView View { get; }

        public ViewModel Model { get; }


        public IEnumerable<ViewModel> Analyse()
        {
            var jsonString = View.GetJsonString();
            return ViewModel.GetResults(jsonString);
        }

        public string GetSavedFilePath()
        {
            return View.GetSavedFilePath;
        }


        public bool Save()
        {
            var results = Analyse();
            using (var stream = ExcelHelper.Export(ExcelViewModel.ConvertFrom(results)))
            {
                stream.SaveAsFile(GetSavedFilePath());
                return true;
            }
        }
    }
}
