using System;
using System.Text;
using NVelocity.App;
using SimpleVelocity.Exceptions;

namespace SimpleVelocity.Core
{
    public class VelocityFacade
    {
        private Enumerators.ContentType ContentType { get; set; }
        public VelocityEngine Engine { get; private set; }
        public ContextFacade DATA { get; private set; }
        public string Path { get; private set; }
        private string TrunkPathOverwritten { get; set; }

        public VelocityFacade(Enumerators.ContentType contentType, ContextFacade dataContext, string path_VM_Files)
        {
            this.ContentType = contentType;
            this.Engine = new VelocityEngine();
            this.DATA = dataContext;
            this.Path = path_VM_Files;
            this.TrunkPathOverwritten = string.Empty;
        }

        #region Methods

        /// <summary>
        /// If you use this object type with multiple reports, are you will need that.
        /// </summary>
        public void OverwriteTrunkPath(string newTrunkPath)
        {
            this.TrunkPathOverwritten = newTrunkPath;
        }

        /// <summary>
        /// Get HTML processed.
        /// </summary>
        public StringBuilder Render()
        {
            try
            {
                PropertiesFacade properties = PropertiesFacade.Create(this.ContentType);
                properties.OverwriteTrunkPath(this.TrunkPathOverwritten); // If necessary
                this.Engine.Init(properties.Load()); // Starting Velocity Engine

                TemplateFacade template = new TemplateFacade(this);
                template.SetTemplate(); // processing
                return template.Fill(); // HTML processed
            }
            catch (Exception error) { throw new RenderException(error.Message, error); }
        }

        #endregion
    }
}
