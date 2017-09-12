using System.Linq;
using System.Windows.Forms;
using AdsScraper.Desktop.Interfaces;

namespace AdsScraper.Desktop.Logic
{
    public static class EventLog
    {
        public static void WriteLine(string text)
        {
            foreach (var form in Application.OpenForms.OfType<IEventLogForm>())
            {
                form.WriteToLog(text);
            }
        }

        public static void Clear()
        {
            foreach (var form in Application.OpenForms.OfType<IEventLogForm>())
            {
                form.ClearLog();
            }
        }
    }
}