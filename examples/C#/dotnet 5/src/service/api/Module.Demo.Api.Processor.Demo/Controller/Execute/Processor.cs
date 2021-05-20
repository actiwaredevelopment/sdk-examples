using Development.Framework.LanguageManager.Portable.Controller;
using Development.SDK.Module.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Demo.Api.Processor.Demo.Controller.Execute
{
    public class Processor
    {
        #region Public Methods 

        /// <summary>
        /// Initialize the configuration controller.
        /// </summary>
        /// <param name="config">The loaded processor configuration.</param>
        /// <param name="configInfo"></param>
        public Development.SDK.Module.Data.Container.Step Execute(Development.SDK.Module.Data.Container.Container container, Development.SDK.Module.Data.Common.ItemConfig config, string inputPort, bool inTestMode, Development.SDK.Logging.Controller.Logger? _logger)
        {
            // Define step container
            Development.SDK.Module.Data.Container.Step step = null;

            // Create new step container
            step = new Development.SDK.Module.Data.Container.Step();

            // Define module configuration
            Data.Processor.Config processorConfig = null;

            if (config == null ||
                config.Parameters == null ||
                config.Parameters.Count == 0)
            {
                throw new ArgumentException("No processor config given");
            }

            Development.SDK.Module.Enums.CommunicationChannel channel = Development.SDK.Module.Enums.CommunicationChannel.HttpRest;

            // Create new collection for node-, data-, runtime fields and variabls
            step.Fields = new Dictionary<string, List<string>>();
            step.RuntimeFields = new Dictionary<string, List<string>>();
            step.DataFields = new Dictionary<string, List<string>>();
            step.Variables = new Dictionary<string, List<string>>();

            if (inTestMode == true &&
                container.ExecutionMode != Development.SDK.Module.Enums.ExecutionMode.Service)
            {
                channel = Development.SDK.Module.Enums.CommunicationChannel.MessageQueue;
            }

            using (Development.SDK.Module.Controller.Helper helper = new Development.SDK.Module.Controller.Helper(channel, container))
            {
                try
                {
                    // Set default state
                    step.State = Development.SDK.Module.Enums.ProcessState.Done;
                    // Link helper messages
                    step.Messages = helper.Messages;

                    helper.WriteMessage(Development.SDK.Module.Enums.ReportLevel.Message, "PROCESSOR_LOAD_CONFIG", "Load processor configuration");

                    // Get configuration
                    processorConfig = new Data.Processor.Config();
                    processorConfig.Config1 = config.Parameters["config_1"];
                    processorConfig.Config2 = config.Parameters["config_2"];
                    processorConfig.Config3 = config.Parameters["config_3"];

                    if (processorConfig == null)
                    {
                        helper.WriteMessage(Development.SDK.Module.Enums.ReportLevel.Error, "ERROR_PROCESSOR_LOAD_CONFIG_FAILED", "No processor configuration couldn't be loaded");
                        throw new Exception("No processor configuration couldn't be loaded");
                    }

                    // First format common configuration
                    this.FormatConfiguration(ref processorConfig, helper);

                    // Do some stuff here
                }
                catch (Exception ex)
                {
                    step.State = Development.SDK.Module.Enums.ProcessState.Error;

                    Console.WriteLine(ex.ToString());

                    throw ex;
                }
                finally
                {
                    if (helper != null &&
                        helper.Messages != null)
                    {
                        foreach (Development.SDK.Module.Data.Common.Message message in helper.Messages)
                        {
                            if (message != null)
                            {
                                switch (message.ReportLevel)
                                {
                                    case Development.SDK.Module.Enums.ReportLevel.Debug:
                                        _logger?.Debug(message.LanguageCode, message.DefaultText, message.Arguments);
                                        break;

                                    case Development.SDK.Module.Enums.ReportLevel.Message:
                                        _logger?.Message(message.LanguageCode, message.DefaultText, message.Arguments);
                                        break;

                                    case Development.SDK.Module.Enums.ReportLevel.Warning:
                                        _logger?.Warning(message.LanguageCode, message.DefaultText, message.Arguments);
                                        break;

                                    case Development.SDK.Module.Enums.ReportLevel.Error:
                                        _logger?.Error(message.LanguageCode, message.DefaultText, message.Arguments);
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            // Set exit port
            step.ExitPort = "output";

            return step;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Formats the process configuration values.
        /// </summary>
        /// <param name="processorConfig">The processor configuration object.</param>
        /// <param name="helper">The current helper.</param>
        public void FormatConfiguration(ref Data.Processor.Config processorConfig, Development.SDK.Module.Controller.Helper helper)
        {
            // Create collection for values that must be formatted
            List<string> syntaxValues = null;

            // Start to format syntax values
            syntaxValues = new List<string>();
            syntaxValues.Add(processorConfig.Config1);
            syntaxValues.Add(processorConfig.Config2);
            syntaxValues.Add(processorConfig.Config3);

            // Format syntax values
            syntaxValues = helper.FormatSyntax(syntaxValues, null);

            int syntaxIndex = 0;

            // Set formatted values
            processorConfig.Config1 = syntaxValues[syntaxIndex];
            syntaxIndex++;

            processorConfig.Config2 = syntaxValues[syntaxIndex];
            syntaxIndex++;

            processorConfig.Config3 = syntaxValues[syntaxIndex];
            syntaxIndex++;
        }

        #endregion
    }
}
