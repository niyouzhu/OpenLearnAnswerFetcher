using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpenLearnAnswerFetcher.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FetcherWindow : Window, IFetcherView
    {
        public FetcherWindow()
        {
            InitializeComponent();
        }


        private FetcherPresenter _presenter;

        public FetcherPresenter Presenter
        {
            get
            {
                if (_presenter == null)
                    _presenter = new FetcherPresenter(this, new FetcherViewModel());
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

        public string GetJsonString()
        {
            return TextBoxJsonString.Text;

        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (Presenter.Save())
            {
                MessageBox.Show("Done!");
            }
        }


    }
}
