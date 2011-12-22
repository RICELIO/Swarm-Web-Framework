using System;
using System.Collections.Generic;
using System.Text;
using NVelocity;

namespace SimpleVelocity.Core
{
    public class ContextFacade
    {
        public VelocityContext Context { get; private set; }

        public ContextFacade()
        {
            this.Context = new VelocityContext();
        }

        #region Methods

        /// <summary>
        /// Add data item in NVelocity context.
        /// </summary>
        public void Add(string key, object value)
        {
            this.Context.Put(key, value);
        }

        #endregion
    }
}
