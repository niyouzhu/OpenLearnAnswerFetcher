using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Client
{
    public interface IEnteranceView
    {
        EnterancePresenter Presenter { get; }

        void OpenWindow(string windowName);
    }
}
