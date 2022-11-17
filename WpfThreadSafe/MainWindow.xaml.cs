using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace WpfThreadSafe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int count = 0;

        delegate void UpdateLabelCallback(int state);

        WorkerContinous workerContinous;
        WorkerEvent workerEvent;

        public MainWindow()
        {
            InitializeComponent();

            workerContinous = new();
            workerEvent = new ();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                workerContinous.WorkerJob(SetTextCallback);
            }).Start();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                workerEvent.Notify += SetTextCallback2;
                workerEvent.WorkerJob();
            }).Start();
        }

        private void SetTextCallback(string getString)
        {
            Dispatcher.Invoke(() => { labelText.Content = getString; });
        }

        private void SetTextCallback2(string getString)
        {
            Dispatcher.Invoke(() => { labelText1.Content = getString; });
        }

        private void buttonOnce_Click(object sender, RoutedEventArgs e)
        {
            count++;
            labelText.Content = @$"Click {count}";

        }

        private void buttonContinue_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            { 
                Thread.CurrentThread.IsBackground = true;
                LongRunning();
            }).Start();
        }

        private void LongRunning() 
        {
            try
            {
                while (true) 
                {
                    int delay = 500;

                    Dispatcher.BeginInvoke( new UpdateLabelCallback(UpdateLabelBackground), 1);
                    Thread.Sleep(delay);

                    Dispatcher.BeginInvoke(new UpdateLabelCallback(UpdateLabelBackground), 2);
                    Thread.Sleep(delay);
                }
            }
            catch
            {

            }
        }

        private void UpdateLabelBackground(int state)
        {
            switch (state)
            {
                case 1:
                    labelText.Background = Brushes.Red; break;
                case 2:
                    labelText.Background = Brushes.Green; break;
                default: break;

            }
        }

    }
}
