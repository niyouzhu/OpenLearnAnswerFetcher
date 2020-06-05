using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Client
{
    public interface ISimulatorView
    {
        SimulatorPresetner Presenter { get; }

        string GetHeader { get; }

        string GetSavedFilePath { get; }
    }
}
