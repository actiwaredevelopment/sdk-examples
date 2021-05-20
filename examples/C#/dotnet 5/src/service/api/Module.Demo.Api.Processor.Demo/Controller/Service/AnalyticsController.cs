using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Module.Demo.Api.Processor.Demo.Controller.Service
{
    /// <summary>
    /// Data-Query Add-In: Jira Query
    /// </summary>
    [ApiController]
    [Route("/api/v1/processor/demo/analytics")]
    public class AnalyticsController : ControllerBase
    {
        #region Constructor

        public AnalyticsController(ILogger<AnalyticsController> logger, Development.SDK.Logging.Controller.Logger sdkLogger, Controller.Analytics.Processor analytics)
        {
            sdkLogger.Initialize(logger);

            _logger = sdkLogger;
            _analytics = analytics;
        }

        #endregion

        #region Fields

        private readonly Development.SDK.Logging.Controller.Logger? _logger;
        private readonly Controller.Analytics.Processor _analytics;

        #endregion

        #region API Methods

        /// <summary>
        /// Collects all configurations originating from modules and/or project settings used.
        /// </summary>
        /// <param name="config">The saved item configuration for this action.</param>
        /// <returns>All references to modules and/or project settings that this action has used; otherwise <c>null</c> if no references to modules and/or project settings exists.</returns>
        [HttpPost]
        [Route("collect")]
        public ActionResult<Development.SDK.Module.Data.Export.Dependency> CollectReferences(Development.SDK.Module.Service.Data.Requests.ConfigInformation config)
        {
            try
            {
                // This processor needs no clean up method
                //return Ok(_analytics.CollectReferences(config.Config));
                return NoContent();
            }
            catch (Exception ex)
            {
                // EX: DEMO-0000
                _logger?.Critcial("DEMO-0000", "The following error occurred while executing the processor. Error: {0}", ex.Message);
                return this.StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Inspects the settings to see if this has any misconfigurations or missing referenced elements.
        /// </summary>
        /// <param name="config">The saved item configuration for this action.</param>
        /// <returns>A list of error information that occurred during inspection; otherwise <c>null</c> if no errors occured.</returns>
        [HttpPost]
        [Route("inspect")]
        public ActionResult<List<Development.SDK.Module.Data.Common.Error>> Inspect(Development.SDK.Module.Service.Data.Requests.ConfigInformation config)
        {
            try
            {
                // This processor needs no clean up method
                return NoContent();
            }
            catch (Exception ex)
            {
                // EX: DEMO-0000
                _logger?.Critcial("DEMO-0000", "The following error occurred while executing the processor. Error: {0}", ex.Message);
                return this.StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Generates the documentation for this configuration.
        /// </summary>
        /// <param name="config">The saved item configuration for this action.</param>
        /// <param name="showPasswords">Whether passwords must be shown as plain text in the documentation.</param>
        /// <returns>The documentation for the given configuration; otherwise <c>null</c> if no documentation is to be created.</returns>
        [HttpPost]
        [Route("documentation")]
        public ActionResult<Development.SDK.Module.Data.Document.Documentation> GenerateDocumentation(Development.SDK.Module.Service.Data.Requests.ProcessorGenerateDocumentation documentation)
        {
            try
            {
                // This processor needs no clean up method
                return Ok(_analytics.GenerateDocumentation(documentation.Config, documentation.ShowPasswords, new string[] { }));
            }
            catch (Exception ex)
            {
                // EX: DEMO-0000
                _logger?.Critcial("DEMO-0000", "The following error occurred while executing the processor. Error: {0}", ex.Message);
                return this.StatusCode(500, ex.Message);
            }
        }

        #endregion
    }
}