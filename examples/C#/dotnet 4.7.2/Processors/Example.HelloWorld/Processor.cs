using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Development.SDK.Module.Data.Common;
using Development.SDK.Module.Data.Container;
using Development.SDK.Module.Enums;
using Development.SDK.Module.Extensions;

namespace Example.HelloWorld
{
    public class Processor : Development.SDK.Module.Interfaces.IProcessorExecution
    {
        public void CleanUp(ProcessState processState, Container container, Step step, ItemConfig config, string inputPort)
        {
            throw new NotImplementedException();
        }

        public Stream DownloadObject(Dictionary<string, string> attributes, Services serviceSettings)
        {
            throw new NotImplementedException();
        }

        public Step Execute(Container container, ItemConfig config, string inputPort)
        {
            // Read config
            string language = config.Parameters["General.language"].ToLower();

            // Init resulting step
            Step resultStep = new Step();
            resultStep.ExitPort = "output";
            resultStep.Fields = new Dictionary<string, List<string>>();

            // Create helper
            Development.SDK.Module.Controller.Helper helper = new Development.SDK.Module.Controller.Helper(container);

            // Format syntax in language. So you can for example use "[CFG.Language]" in the node configuration for the language
            language = helper.FormatSyntax(language, null);

            // Write out which language we use
            helper.WriteMessage(ReportLevel.Debug, "MOD_EXAMPLE_HELLO_WORLD_USING_LANGUAGE", "Using language {0}", language);

            // Fallback language is en
            string helloWorld = "Hello world!";

            // Set node field depending on language
            switch (language)
            {
                case "de":
                    helloWorld = "Hallo Welt!";
                    break;
                case "en":
                    // Do nothing, helloWorld already set in en
                    break;
                case "fr":
                    helloWorld = "Bonjour le monde !";
                    break;
                case "it":
                    helloWorld = "Ciao mondo!";
                    break;
                default:
                    helper.WriteMessage(ReportLevel.Warning, "MOD_EXAMPLE_HELLO_WORLD_UNKNOWN_LANGUAGE", "Unknown language. Falling back to 'en'");
                    break;
            }

            // Add helloWorld node field
            resultStep.Fields.TryAdd("helloWorld", helloWorld, true);

            // Return step
            return resultStep;
        }

        public Step Test(Container container, ItemConfig config, string inputPort)
        {
            // test-executation is the same as production-executation
            return Execute(container, config, inputPort);
        }
    }
}
