using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Module.Demo.Api.Processor.Demo.Controller.Service
{
    /// <summary>
    /// Data-Query Add-In: Jira Query
    /// </summary>
    [ApiController]
    [Route("/api/v1/processor/demo/config")]
    public class ConfigController : ControllerBase
    {
        #region Constructor

        public ConfigController(ILogger<ConfigController> logger, Development.SDK.Logging.Controller.Logger sdkLogger, Controller.Config.Processor config)
        {
            sdkLogger.Initialize(logger);

            _logger = sdkLogger;
            _config = config;
        }

        #endregion

        #region Fields

        private readonly Development.SDK.Logging.Controller.Logger? _logger;
        private readonly Controller.Config.Processor _config;

        #endregion

        #region API Methods

        /// <summary>
        /// Gets the used, referenced or created items like node fields and/or language codes.
        /// </summary>
        /// <param name="config">The configuration information that can be used to generate the control. 
        /// It also contains configurations for the control that have already been saved.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("items")]
        public ActionResult<Development.SDK.Module.Data.DataQuery.QueryItem> GetItems(Development.SDK.Module.Data.Common.ConfigInformation config)
        {
            return Ok(_config.GetUsedItems(config));
        }

        /// <summary>
        /// Validates the item configuration.
        /// </summary>
        /// <param name="config">The item configuration that was created by the save method.</param>
        /// <returns><c>true</c> if the config is valid, otherwise <c>false</c>.</returns>
        [HttpPost]
        [Route("validate")]
        public ActionResult<bool> Validate(Development.SDK.Module.Data.Common.ItemConfig config)
        {
            try
            {
                return Ok(_config.Validate(config));
            }
            catch (Exception ex)
            {
                // EX: DEMO-0000
                _logger?.Critcial("DEMO-0000", "The following error occurred while executing the processor. Error: {0}", ex.Message);
                return this.StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Called when the system wants to upgrade the current configuration.
        /// </summary>
        /// <param name="config">The configuration information that can be used to upgrade the configuration. 
        /// It also contains the module and action configurations that have already been saved.</param>
        /// <returns>
        /// Returns the item configuration that must be saved; otherwise <c>null</c> if an error occured by upgrading the item configuration.
        /// </returns>
        [HttpPost]
        [Route("upgrade")]
        public ActionResult<Development.SDK.Module.Data.Common.ItemConfig> Upgrade(Development.SDK.Module.Data.Common.ConfigInformation config)
        {
            try
            {
                return Ok(_config.Upgrade(config));
            }
            catch (Exception ex)
            {
                // EX: DEMO-0000
                _logger?.Critcial("DEMO-0000", "The following error occurred while executing the processor. Error: {0}", ex.Message);
                return this.StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Gets the used, referenced or created items like node fields and/or language codes.
        /// </summary>
        /// <param name="config">The configuration information that can be used to generate the control. 
        /// It also contains configurations for the control that have already been saved.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("config/update")]
        public ActionResult<Development.SDK.Module.Data.Common.Error> UpdateReferences(Development.SDK.Module.Data.Common.UpdateReferenceInformation config)
        {
            return NoContent();
        }

        #endregion
    }
}