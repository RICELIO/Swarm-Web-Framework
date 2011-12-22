using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleVelocity
{
    public abstract class Enumerators
    {
        public enum ContentType
        {
            Undefined = Int16.MinValue,
            Report = 1,
            View = 2
        }
    }
}
