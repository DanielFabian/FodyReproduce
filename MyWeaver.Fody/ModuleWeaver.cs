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

            // Expected behaviour:
            // Both languages should show the warnings identically, i.e.:
            // - LogWarning should show warnings in both languages
            // - LogWarningPoint without a location (presumably) should not show any line or column info in F#
            // - LogError should be visible in F# and not only break the build.

            LogInfo("Info does not show up in either language");
            LogWarning("Warning does not show up in F#, but does show in C#"); 
            LogWarningPoint("Warning does show up in both languages, but has line/column info in F#", new SequencePoint(new Document(" ")));
            LogError("Error does not show up in F#, but it causes the build to fail. C# it shows up and breaks the build.");
        }
    }
}
