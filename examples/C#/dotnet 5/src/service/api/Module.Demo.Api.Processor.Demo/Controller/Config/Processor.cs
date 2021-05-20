using Development.SDK.Module.Extensions;
using Module.Demo.Api.Processor.Demo.Extensions;

namespace Module.Demo.Api.Processor.Demo.Controller.Config
{
    public class Processor
    {
        #region Public Methods

        /// <summary>
        /// Validates the view model for object information dialogs.
        /// </summary>
        /// <param name="propertyName">The name of the property to be validated.</param>
        /// <param name="viewModel">The model which contains all values for the validation.</param>
        /// <param name="error">The error message that is set during validation.</param>
        /// <returns><c>true</c> if an error occured; otherwise <c>false</c>.</returns>
        public bool Validate(Development.SDK.Module.Data.Common.ItemConfig config)
        {
            //if (string.IsNullOrEmpty(config.Name) == true ||
            //    string.IsNullOrWhiteSpace(config.Name) == true)
            //{
            //    return false;
            //}

            return true;
        }

        /// <summary>
        /// Gets the used, referenced or created items like node fields and/or language codes.
        /// </summary>
        /// <param name="config">The configuration information that can be used to generate the control. 
        /// It also contains configurations for the control that have already been saved.</param>
        /// <returns></returns>
        public Development.SDK.Module.Data.Common.UsedItems GetUsedItems(Development.SDK.Module.Data.Common.ConfigInformation config)
        {
            // Define module configuration
            Data.Processor.Config processorConfig = null;

            if (config != null)
            {
                // Get configuration
                processorConfig = new Data.Processor.Config();
                processorConfig.Config1 = config.Parameters["config_1"];
                processorConfig.Config2 = config.Parameters["config_2"];
                processorConfig.Config3 = config.Parameters["config_3"];
            }

            if (processorConfig == null)
            {
                processorConfig = new Data.Processor.Config();
            }

            return processorConfig.GetUsedItems(config);
        }

        /// <summary>
        /// Called when the system wants to upgrade the current configuration.
        /// </summary>
        /// <param name="config">The configuration information that can be used to upgrade the configuration. 
        /// It also contains the module and action configurations that have already been saved.</param>
        /// <returns>
        /// Returns the item configuration that must be saved; otherwise <c>null</c> if an error occured by upgrading the item configuration.
        /// </returns>
        public Development.SDK.Module.Data.Common.ItemConfig Upgrade(Development.SDK.Module.Data.Common.ConfigInformation config)
        {
            // Define item and module configuration object
            Data.Processor.Config processorConfig = null;
            Development.SDK.Module.Data.Common.ItemConfig itemConfig = null;

            // Create new item configuration instance.
            itemConfig = new Development.SDK.Module.Data.Common.ItemConfig();

            if (config != null)
            {
                // Get configuration
                processorConfig = new Data.Processor.Config();
                processorConfig.Config1 = config.Parameters["config_1"];
                processorConfig.Config2 = config.Parameters["config_2"];
                processorConfig.Config3 = config.Parameters["config_3"];
            }

            if (processorConfig == null)
            {
                processorConfig = new Data.Processor.Config();
            }

            // Add configuration to item configuration object
            itemConfig.Parameters.Add("config_1", processorConfig.Config1);
            itemConfig.Parameters.Add("config_2", processorConfig.Config2);
            itemConfig.Parameters.Add("config_3", processorConfig.Config3);

            return itemConfig;
        }

        /// <summary>
        /// Gets the used, referenced or created items like node fields and/or language codes.
        /// </summary>
        /// <param name="config">The configuration information that can be used to generate the control. 
        /// It also contains configurations for the control that have already been saved.</param>
        /// <returns></returns>
        public Development.SDK.Module.Data.Common.Error UpdateReferences(Development.SDK.Module.Data.Common.UpdateReferenceInformation config)
        {
            return null;
        }

        #endregion
    }
}