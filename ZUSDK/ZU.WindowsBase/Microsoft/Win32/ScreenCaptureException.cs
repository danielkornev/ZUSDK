using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Win32
{
    class ScreenCaptureException : Exception
    {
        public ScreenCaptureException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public ScreenCaptureException(string message)
            : base(message)
        { }
    
    }
}
