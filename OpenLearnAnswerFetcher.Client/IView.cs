using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Client
{
    public interface IView
    {

        string GetJsonString();

        Presenter Presenter { get; }

        string GetSavedFilePath { get; }

    }
}
