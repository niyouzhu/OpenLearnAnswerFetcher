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
    /// Interaction logic for EnteranceWindow.xaml
    /// </summary>
    public partial class EnteranceWindow : Window, IEnteranceView
    {
        public EnteranceWindow()
        {
            InitializeComponent();
        }

        private EnterancePresenter _presenter;

        public EnterancePresenter Presenter
        {
            get
            {
                if (_presenter == null)
                    _presenter = new EnterancePresenter(this, new EnteranceViewModel());
                return _presenter;

            }
        }

        public void OpenWindow(string windowName)
        {
            var objectHandle = (System.Activator.CreateInstance(System.Reflection.Assembly.GetExecutingAssembly().FullName, windowName));
            var window = (Window)objectHandle.Unwrap();
            window.Show();
        }

        private void ButtonFetcher_Click(object sender, RoutedEventArgs e)
        {
            Presenter.OpenWindow("OpenLearnAnswerFetcher.Client.FetcherWindow");
        }

        private void ButtonSimulator_Click(object sender, RoutedEventArgs e)
        {
            Presenter.OpenWindow("OpenLearnAnswerFetcher.Client.SimulatorWindow");

        }
    }
}
