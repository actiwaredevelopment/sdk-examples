# Module Service
## Usage
- Extract the content of the ModuleService.zip into a directory.
- Place your module zip file into the folder "module". Allowed extensions are .zip and .mod
- Edit ModuleService.exe.config:
  -  Replace the port number of 8080 in the line 
     ```
     <add key="baseAddress" value="http://*:8080" />
     ```
     with a port of your choice (keep the property "service_address" of the info.json in mind)
- Start ModuleService.exe
- If you wish to run the service as a windows service take a look at https://nssm.cc/
- You can rename "ModuleService.exe" as you like. Notice that you also need to rename the "ModuleService.exe.config" file.