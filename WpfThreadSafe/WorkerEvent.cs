using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfThreadSafe
{
    internal class WorkerEvent
    {

        public delegate void GetString(string text);

        public event GetString? Notify;

        public void WorkerJob()
        {
            int i = 0;
            while (true)
            {
                string someText = $"String: {i++}";
                Notify?.Invoke(someText);
                //gettingStringMethod(someText);
                Thread.Sleep(100);
            }
        }

    }
}
