using System.Text;
using System.Threading;


using System;

namespace Hw.Extensions
{
    public class CurdAfterLog : IDisposable
    {




        public static AsyncLocal<CurdAfterLog> Current = new AsyncLocal<CurdAfterLog>();
        public StringBuilder Sb { get; } = new StringBuilder();

        public CurdAfterLog()
        {
            Current.Value = this;
        }
        public void Dispose()
        {
            Sb.Clear();
            Current.Value = null;
        }
    }
}