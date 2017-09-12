using System.Windows.Forms;

namespace AdsScraper.Desktop.Interfaces
{
    public interface IEventLogForm
    {
        TextBox EventLog { get; }
        void WriteToLog(string value);
        void ClearLog();
    }
}
