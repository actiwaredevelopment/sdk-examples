namespace Module.Demo.Api.Processor.Demo.Extensions
{
    public static class HelperExt
    {
        /// <summary>
        /// Extracts the used items from the current configuration.
        /// </summary>
        /// <param name="config">The configuration from that the used items must be extracted.</param>
        /// <returns>The used items object.</returns>
        public static Development.SDK.Module.Data.Common.UsedItems GetUsedItems(this Data.Processor.Config config, Development.SDK.Module.Data.Common.ConfigInformation configInformation)
        {
            // Define return value
            Development.SDK.Module.Data.Common.UsedItems value = null;

            // Create new used items object
            value = new Development.SDK.Module.Data.Common.UsedItems();

            return value;
        }
    }
}