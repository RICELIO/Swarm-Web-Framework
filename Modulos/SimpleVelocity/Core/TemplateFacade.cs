using System;
using System.IO;
using System.Text;
using NVelocity;

namespace SimpleVelocity.Core
{
    internal class TemplateFacade
    {
        private Template Template { get; set; }
        private VelocityFacade Velocity { get; set; }

        internal TemplateFacade(VelocityFacade obj)
        {
            this.Velocity = obj;
        }

        #region Methods

        internal void SetTemplate()
        {
            string velocityFilePath = this.Velocity.Path;
            this.Template = this.Velocity.Engine.GetTemplate(velocityFilePath);
        }

        /// <summary>
        /// Merge: velocity template + data context
        /// </summary>
        internal StringBuilder Fill()
        {
            StringWriter obj = new StringWriter();
            this.Template.Merge(this.Velocity.DATA.Context, obj);
            return obj.GetStringBuilder();
        }

        #endregion
    }
}
