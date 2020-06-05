using OpenLearnAnswerFetcher.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Client
{
    public class SimulatorPresetner
    {
        public SimulatorPresetner(ISimulatorView view, SimulatorViewModel model)
        {
            View = view;
            Model = model;
        }

        public SimulatorViewModel Model { get; }
        public ISimulatorView View { get; }

        public Task<bool> Work()
        {

            var tuple = new Tuple<string, string>(View.GetHeader, GetSavedFilePath());
            return Task.Factory.StartNew(state =>
            {
                var header = ((Tuple<string, string>)state).Item1;
                var worker = new Worker();
                var questionDetails = worker.Work(header);
                var viewData = SimulatorViewModel.ConvertFrom(questionDetails);
                using (var stream = ExcelHelper.ExportWrongQuestions(viewData))
                {
                    stream.SaveAsFile(((Tuple<string, string>)state).Item2);
                    return true;
                }
            }, tuple);
        }

        public string GetSavedFilePath()
        {
            return View.GetSavedFilePath;
        }

    }
}
