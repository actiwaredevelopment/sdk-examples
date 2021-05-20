using Development.SDK.Module.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.Demo.Api.Processor.Demo.Controller.Analytics
{
    public class Processor
    {
        #region Public methods

        /// <summary>
        /// Generates the documentation.
        /// </summary>
        /// <param name="documentationHelper">The documentation helper.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="showPasswords">if set to <c>true</c> [show passwords].</param>
        /// <param name="item">The item.</param>
        public Development.SDK.Module.Data.Document.Documentation GenerateDocumentation(Development.SDK.Module.Data.Common.ConfigInformation config, bool showPasswords, string[] filter)
        {
            Development.SDK.Module.Data.Document.Documentation documentation = new();

            return documentation;
        }

        /// <summary>
        /// Collects all configurations originating from modules and/or project settings used.
        /// </summary>
        /// <param name="config">The saved item configuration for this action.</param>
        /// <returns>All references to modules and/or project settings that this action has used; otherwise <c>null</c> if no references to modules and/or project settings exists.</returns>
        public Development.SDK.Module.Data.Export.Dependency CollectReferences(Data.Processor.Config config)
        {
            Development.SDK.Module.Data.Export.Dependency dependency = new();

            if (config != null)
            {

            }

            return dependency;
        }

        #endregion
    }
}
