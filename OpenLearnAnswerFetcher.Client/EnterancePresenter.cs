using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Client
{
    public class EnterancePresenter
    {
        public EnterancePresenter(IEnteranceView view, EnteranceViewModel model)
        {
            Model = model;
            View = view;
        }

        public EnteranceViewModel Model { get; }
        public IEnteranceView View { get; }

        public void OpenWindow(string windowName)
        {
            View.OpenWindow(windowName);
        }
    }
}
