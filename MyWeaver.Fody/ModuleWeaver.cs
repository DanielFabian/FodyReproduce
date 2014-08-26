using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Weavers
{
    public class ModuleWeaver
    {
        public XElement Config { get; set; }
        public Action<string> LogDebug { get; set; }
        public Action<string> LogInfo { get; set; }
        public Action<string> LogWarning { get; set; }
        public Action<string, SequencePoint> LogWarningPoint { get; set; }
        public Action<string> LogError { get; set; }
        public Action<string, SequencePoint> LogErrorPoint { get; set; }
        public IAssemblyResolver AssemblyResolver { get; set; }
        public ModuleDefinition ModuleDefinition { get; set; }
        public List<string> DefineConstants { get; set; }
        public string AssemblyFilePath { get; set; }
        public string ProjectDirectoryPath { get; set; }
        public string AddinDirectoryPath { get; set; }
        public string SolutionDirectoryPath { get; set; }

        // Init logging delegates to make testing easier
        public ModuleWeaver()
        {
            LogDebug = m => { };
            LogInfo = m => { };
            LogWarning = m => { };
            LogWarningPoint = (m, p) => { };
            LogError = m => { };
            LogErrorPoint = (m, p) => { };
        }

        public void Execute()
        {
            ModuleDefinition.Types.Add(new TypeDefinition("MyNamespace", "MyType", TypeAttributes.Public));

            LogInfo("Info does not show up in \"Error List\"");
            LogWarning("Warning does not show up in \"Error List\"");
            LogWarningPoint("Warning does show up in \"Error List\"", new SequencePoint(new Document(" ")));
            //LogError("Error does not show up in \"Error List\", but it causes the build to fail");
        }
    }
}
