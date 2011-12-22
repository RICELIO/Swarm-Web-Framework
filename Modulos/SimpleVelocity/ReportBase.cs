using System;
using System.Text;
using SimpleVelocity.Core;

namespace SimpleVelocity
{
    public abstract class ReportBase : SimpleVelocityBase
    {
        public ReportBase(string path)
            : base(Enumerators.ContentType.Report, path)
        {
        }

        protected override abstract void SetDataContext();
    }
}