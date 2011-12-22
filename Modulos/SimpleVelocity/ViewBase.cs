using System;
using System.Text;
using SimpleVelocity.Core;

namespace SimpleVelocity
{
    public abstract class ViewBase : SimpleVelocityBase
    {
        public ViewBase(string path)
            : base(Enumerators.ContentType.View, path)
        {
        }

        protected override abstract void SetDataContext();
    }
}