using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleVelocity.Core
{
    public abstract class SimpleVelocityBase
    {
        protected Enumerators.ContentType ContentType { get; private set; }
        protected VelocityFacade Engine { get; private set; }
        protected ContextFacade DATA { get; private set; }

        private string TrunkPathOverwritten { get; set; }
        private string Path { get; set; }

        public SimpleVelocityBase(Enumerators.ContentType contentType, string path)
        {
            this.ContentType = contentType;
            this.DATA = new ContextFacade();
            this.Path = path;
        }

        #region Methods

        /// <summary>
        /// Needed define the DATA context.
        /// </summary>
        protected abstract void SetDataContext();

        protected void Add(string key, object value)
        {
            this.DATA.Add(key, value);
        }

        public string Render()
        {
            this.SetDataContext();

            this.Engine = new VelocityFacade(this.ContentType, this.DATA, this.Path);
            this.Engine.OverwriteTrunkPath(this.TrunkPathOverwritten);

            return this.Engine.Render().ToString();
        }

        /// <summary>
        /// If you use this object type with multiple reports, are you will need that.
        /// </summary>
        /// <param name="newPath">Path + FileName (with extension)</param>
        public void OverwritePath(string newPath)
        {
            this.Path = newPath;
        }

        /// <summary>
        /// If you use this object type with multiple reports + multiple trunk folders, are you will need that.
        /// </summary>
        public void OverwriteTrunkPath(string newFullPath)
        {
            this.TrunkPathOverwritten = newFullPath;
        }

        #endregion
    }
}
