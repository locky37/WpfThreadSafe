using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WpfThreadSafe
{
    public delegate void GetString(string text);

    public class WorkerContinous
    {
        public void WorkerJob(GetString gettingStringMethod) 
        {
            int i = 0;
            while (true) 
            {
                string someText = $"String: {i++}";
                gettingStringMethod(someText);
                Thread.Sleep(100);
            }
        }
    }
}
