using System;
using System.Configuration;
using System.Text;
using Commons.Collections;

namespace SimpleVelocity.Core
{
    internal abstract class PropertiesFacade
    {
        private string TemplatesTrunkPath { get; set; }
        private ExtendedProperties Properties { get; set; }

        protected PropertiesFacade(string fullPathKEY)
        {
            this.TemplatesTrunkPath = ConfigurationManager.AppSettings[fullPathKEY];
            this.Properties = new ExtendedProperties();
        }

        public static PropertiesFacade Create(Enumerators.ContentType contentType)
        {
            switch (contentType)
            {
                default:
                case Enumerators.ContentType.Undefined:
                case Enumerators.ContentType.Report:
                    return new ReportProperties();
                case Enumerators.ContentType.View:
                    return new ViewProperties();
            }
        }

        #region Methods

        /// <summary>
        /// Replace the SimpleVelocity trunk path.
        /// </summary>
        public void OverwriteTrunkPath(string newTrunkPath)
        {
            if (!string.IsNullOrEmpty(newTrunkPath))
                this.TemplatesTrunkPath = newTrunkPath;
        }

        internal ExtendedProperties Load()
        {
            this.Properties.SetProperty("file.resource.loader.path", this.TemplatesTrunkPath);
            return this.Properties;
        }

        #endregion
    }
}
