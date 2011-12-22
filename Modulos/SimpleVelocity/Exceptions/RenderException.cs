using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleVelocity.Exceptions
{
    public class RenderException : Exception
    {
        public RenderException() : base() { }
        public RenderException(string msg) : base(msg) { }
        public RenderException(string msg, Exception error) : base(msg, error) { }
    }
}
