using Newtonsoft.Json;
using System.Collections.Generic;

namespace Module.Demo.Api.Processor.Demo.Data.Processor
{
    /// <summary>
    /// Represens information to start a process.
    /// </summary>
    public class Config
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Config"/> class.
        /// </summary>
        public Config()
        {

        }

        #endregion

        #region Properties

        [JsonProperty("config_1")]
        public string Config1 { get; set; } = "";
        
        [JsonProperty("config_2")]
        public string Config2 { get; set; } = "";
        
        [JsonProperty("config_3")]
        public string Config3 { get; set; } = "";

        #endregion
    }
}
