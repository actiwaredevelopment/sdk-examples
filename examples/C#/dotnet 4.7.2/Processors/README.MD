# Processor example module
## Description
This module provides example processors. 
## Processor: HelloWorld
This processor sets the node field "helloWorld" in different languages depending on its configuration.
## How to compile and generate a module
- Open the solution "Processors.sln" in Visual Studio and rebuild all projects. 
- The assembly will be compiled to "output\functions"
- Copy the content of the "assets" directory to the "output" folder
- Create a zip file containing the content of the output folder. (functions, images and info.json must be in the root folder of the zip)
## Prepare service
- Follow the instructions to install the Module Service
## Upload module
- Start the Designer, open the module manager and import your module. Restart the designer afterwards.
## Use the module
- Create a new process in the designer and drag the processor "Hello World" into the process.
- Double click the "Hello World" processor and enter "de" as language
- Start the test mode and test the process
  - The output window should say "Using language de"
  - Double click on the "Hello World" processor.
  - Open the "Delta (Output)" tab. There should be a node field "helloWorld" with the value "Hallo Welt!"