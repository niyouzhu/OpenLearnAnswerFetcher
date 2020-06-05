using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OpenLearnAnswerFetcher.Client
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class SimulatorWindow : Window, ISimulatorView
    {
        public SimulatorWindow()
        {
            InitializeComponent();
        }

        public string GetHeader
        {
            get
            {
                return TextBoxHeader.Text;
            }
        }

        private SimulatorPresetner _presenter;

        public SimulatorPresetner Presenter
        {
            get
            {
                if (_presenter == null)
                    _presenter = new SimulatorPresetner(this, new SimulatorViewModel());
                return _presenter;
            }
        }
        public string GetSavedFilePath
        {
            get
            {
                var dialog = new VistaSaveFileDialog();
                dialog.OverwritePrompt = true;
                dialog.DefaultExt = ".xls";
                dialog.FileName = "Default.xls";
                dialog.AddExtension = true;
                dialog.ValidateNames = true;
                dialog.RestoreDirectory = true;
                dialog.Filter = "xls files (*.xls)|*.xls|xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                var result = dialog.ShowDialog(this);
                if (result.HasValue && result.Value)
                {
                    return dialog.FileName;
                }

                throw new Exception("Please input file name.");
            }
        }
        private async void ButtonSimulator_Click(object sender, RoutedEventArgs e)
        {
            if (await Presenter.Work())
            {
                MessageBox.Show("Done");
            }

        }
    }
}
