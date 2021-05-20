using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Module.Demo.Api.Processor.Demo.Controller.Service
{
    /// <summary>
    /// Data-Query Add-In: Jira Query
    /// </summary>
    [ApiController]
    [Route("/api/v1/processor/demo")]
    public class ExecuteController : ControllerBase
    {
        #region Constructor

        public ExecuteController(ILogger<ExecuteController> logger, Development.SDK.Logging.Controller.Logger sdkLogger, Controller.Execute.Processor execute)
        {
            sdkLogger.Initialize(logger);

            _logger = sdkLogger;
            _execute = execute;
        }

        #endregion

        #region Fields

        private readonly Development.SDK.Logging.Controller.Logger? _logger;
        private readonly Controller.Execute.Processor _execute;

        #endregion

        #region API Methods

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="container">The base container that was created by the process engine.</param>
        /// <param name="config">The item configuration that was created by the save method.</param>
        /// <param name="item">The information about the query item that must be executed.</param>
        /// <returns>The result of the execution as a table</returns>
        [HttpPost]
        [Route("execute")]
        public ActionResult<Development.SDK.Module.Data.Container.Step> Execute(Development.SDK.Module.Service.Data.Requests.ExecutionInfo executionInfo)
        {
            Development.SDK.Module.Data.DataQuery.ResultTable resultTable = new();

            Guid transactionId = Guid.Empty;

            if (executionInfo != null &&
                executionInfo.Container != null &&
                executionInfo.Container.SystemFields != null &&
                executionInfo.Container.SystemFields.TransactionId != null)
            {
                transactionId = executionInfo.Container.SystemFields.TransactionId;
            }

            using (Serilog.Context.LogContext.PushProperty("TransactionId", transactionId.ToString()))
            {
                try
                {
                    var step = _execute.Execute(executionInfo.Container,
                                                executionInfo.Config,
                                                executionInfo.InputPort,
                                                false,
                                                _logger);

                    return Ok(step);
                }
                catch (Exception ex)
                {
                    // EX: DEMO-0000
                    _logger?.Critcial("DEMO-0000", "The following error occurred while executing the processor Execute Script (C#). Error: {0}", ex.Message);
                    return this.StatusCode(500, ex.Message);
                }
            }
        }

        /// <summary>
        /// Executes the specified query in test mode.
        /// </summary>
        /// <param name="container">The base container that was created by the process engine.</param>
        /// <param name="config">The item configuration that was created by the save method.</param>
        /// <param name="item">The information about the query item that must be executed.</param>
        /// <returns>The result of the execution as a table</returns>
        [HttpPost]
        [Route("test")]
        public ActionResult<Development.SDK.Module.Data.Container.Step> Test(Development.SDK.Module.Service.Data.Requests.ExecutionInfo executionInfo)
        {
            try
            {
                var step = _execute.Execute(executionInfo.Container,
                                            executionInfo.Config,
                                            executionInfo.InputPort,
                                            true,
                                            _logger);

                return Ok(step);
            }
            catch (Exception ex)
            {
                // EX: DEMO-0000
                _logger?.Critcial("DEMO-0000", "The following error occurred while executing the processor Execute Script (C#). Error: {0}", ex.Message);
                return this.StatusCode(500, ex.Message);
            }
        }

        #endregion
    }
}