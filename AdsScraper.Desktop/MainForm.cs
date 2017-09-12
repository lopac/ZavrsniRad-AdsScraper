using System;
using System.Windows.Forms;
using AdsScraper.Desktop.Interfaces;

namespace AdsScraper.Desktop
{
    public partial class MainForm : Form, IEventLogForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public TextBox EventLog { get; private set; }

        public void ClearLog()
        {
            EventLog.Text = string.Empty;
        }

        public void WriteToLog(string value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(WriteToLog), value);
                return;
            }

            EventLog.Text += value + Environment.NewLine;
        }

        private void FetchAdsButton_Click(object sender, EventArgs e)
        {
            var url = urlTextBox.Text;
            Logic.AdsScraper.Instance.SaveCars(url);
        }
    }
}