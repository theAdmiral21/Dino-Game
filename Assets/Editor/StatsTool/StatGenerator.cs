using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Editor.StatsTool
{
    public class StatGeneratorWindow : EditorWindow
    {
        private string _groupName = "Run";
        private string _primitivesNamespace = "Primitives.Stats.DataStructures";
        private string _coreInterfaceNamespace = "Movement.Core.Stats";
        private string _unityNamespace = "Movement.Unity.Stats";
        private string _abstractionsNamespace = "Movement.Unity.Stats.StatSOs.Abstractions";
        private string _menuPrefix = "Stats/";

        private bool _generateInterface = true;

        private string _primitivesOutputDir = "Assets/Scripts/Primitives/Stats/DataStructures";
        private string _interfaceOutputDir = "Assets/Scripts/Core/Movement/Stats";
        private string _soOutputDir = "Assets/Scripts/Unity/Movement/Stats/StatSOs";

        private List<string> _fieldNames = new List<string> { "Speed", "Accel" };
        private Vector2 _scroll;

        [MenuItem("Tools/Stat Generator")]
        public static void ShowWindow()
        {
            GetWindow<StatGeneratorWindow>("Stat Generator");
        }

        private void OnGUI()
        {
            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            EditorGUILayout.LabelField("Naming", EditorStyles.boldLabel);
            _groupName = EditorGUILayout.TextField(new GUIContent("Group Name", "e.g. 'Run' -> RunStats, RunStatSO, IRunStats"), _groupName);
            _menuPrefix = EditorGUILayout.TextField("CreateAssetMenu Prefix", _menuPrefix);
            // _generateInterface = EditorGUILayout.Toggle("Generate Interface", _generateInterface);

            // EditorGUILayout.Space();
            // EditorGUILayout.LabelField("Namespaces", EditorStyles.boldLabel);
            // _primitivesNamespace = EditorGUILayout.TextField("Struct Namespace", _primitivesNamespace);
            // if (_generateInterface)
            //     _coreInterfaceNamespace = EditorGUILayout.TextField("Interface Namespace", _coreInterfaceNamespace);
            // _unityNamespace = EditorGUILayout.TextField("SO Namespace", _unityNamespace);
            // _abstractionsNamespace = EditorGUILayout.TextField("StatSO Base Namespace", _abstractionsNamespace);

            // EditorGUILayout.Space();
            // EditorGUILayout.LabelField("Output Folders", EditorStyles.boldLabel);
            // _primitivesOutputDir = EditorGUILayout.TextField("Struct Output Dir", _primitivesOutputDir);
            // if (_generateInterface)
            //     _interfaceOutputDir = EditorGUILayout.TextField("Interface Output Dir", _interfaceOutputDir);
            // _soOutputDir = EditorGUILayout.TextField("SO Output Dir", _soOutputDir);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Fields", EditorStyles.boldLabel);

            for (int i = 0; i < _fieldNames.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                _fieldNames[i] = EditorGUILayout.TextField($"Field {i + 1}", _fieldNames[i]);
                if (GUILayout.Button("-", GUILayout.Width(24)))
                {
                    _fieldNames.RemoveAt(i);
                    EditorGUILayout.EndHorizontal();
                    break;
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("+ Add Field"))
                _fieldNames.Add("");

            EditorGUILayout.Space();

            GUI.enabled = IsValid();
            if (GUILayout.Button("Generate", GUILayout.Height(30)))
                Generate();
            GUI.enabled = true;

            if (!IsValid())
                EditorGUILayout.HelpBox("Group name required, and every field needs a non-empty, unique name.", MessageType.Warning);

            EditorGUILayout.EndScrollView();
        }

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(_groupName)) return false;
            if (_fieldNames.Count == 0) return false;
            if (_fieldNames.Any(string.IsNullOrWhiteSpace)) return false;
            if (_fieldNames.Select(f => f.Trim()).Distinct().Count() != _fieldNames.Count) return false;
            return true;
        }

        private void Generate()
        {
            var fields = _fieldNames.Select(f => f.Trim()).ToList();
            string structName = $"{_groupName}Stats";
            string interfaceName = $"I{_groupName}Stats";
            string soName = $"{_groupName}StatSO";

            WriteFile(_primitivesOutputDir, structName + ".cs", BuildStructFile(structName, fields));

            if (_generateInterface)
                WriteFile(_interfaceOutputDir, interfaceName + ".cs", BuildInterfaceFile(interfaceName, fields));

            WriteFile(_soOutputDir, soName + ".cs", BuildSOFile(soName, structName, interfaceName, fields));

            Debug.Log($"✅ Files generated!");
            AssetDatabase.Refresh();

        }

        private void WriteFile(string dir, string fileName, string contents)
        {
            Directory.CreateDirectory(dir);
            string path = Path.Combine(dir, fileName);

            if (File.Exists(path) &&
                !EditorUtility.DisplayDialog("Overwrite?", $"{path} already exists. Overwrite?", "Overwrite", "Cancel"))
                return;

            File.WriteAllText(path, contents);
        }

        private string BuildStructFile(string structName, List<string> fields)
        {
            var sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine();
            sb.AppendLine($"namespace {_primitivesNamespace}");
            sb.AppendLine("{");
            sb.AppendLine("    [Serializable]");
            sb.AppendLine($"    public struct {structName}");
            sb.AppendLine("    {");
            sb.AppendLine($"        public Type RuntimeType => typeof({structName});");
            foreach (var f in fields)
                sb.AppendLine($"        public Stat {f};");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string BuildInterfaceFile(string interfaceName, List<string> fields)
        {
            var sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine();
            sb.AppendLine($"namespace {_coreInterfaceNamespace}");
            sb.AppendLine("{");
            sb.AppendLine($"    public interface {interfaceName} : IGameStat");
            sb.AppendLine("    {");
            foreach (var f in fields)
                sb.AppendLine($"        public float {f} {{ get; }}");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string BuildSOFile(string soName, string structName, string interfaceName, List<string> fields)
        {
            var sb = new StringBuilder();
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using System;");
            sb.AppendLine($"using {_primitivesNamespace};");
            sb.AppendLine($"using {_abstractionsNamespace};");
            if (_generateInterface)
                sb.AppendLine($"using {_coreInterfaceNamespace};");
            sb.AppendLine($"using Primitives.Stats;");
            sb.AppendLine();
            sb.AppendLine($"namespace {_unityNamespace}");
            sb.AppendLine("{");
            sb.AppendLine($"    [CreateAssetMenu(fileName = \"{soName}\", menuName = \"{_menuPrefix}{SplitCamelCase(_groupName)} Stats\")]");
            sb.AppendLine("    [Serializable]");
            sb.AppendLine($"    public class {soName} : StatSO");
            sb.AppendLine("    {");
            sb.AppendLine($"        [Header(\"{SplitCamelCase(_groupName)} Stats\")]");
            sb.AppendLine($"        public override Type StatType => typeof({(_generateInterface ? interfaceName : structName)});");
            sb.AppendLine();

            foreach (var f in fields)
            {
                string backing = "_" + char.ToLowerInvariant(f[0]) + f.Substring(1);
                sb.AppendLine($"        public float {f} => {backing};");
                sb.AppendLine($"        [SerializeField] float {backing};");
                sb.AppendLine();
            }

            sb.AppendLine("        public override object BuildRunTime()");
            sb.AppendLine("        {");
            sb.AppendLine($"            return new {structName}");
            sb.AppendLine("            {");
            foreach (var f in fields)
                sb.AppendLine($"                {f} = new Stat({f}),");
            sb.AppendLine("            };");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private string SplitCamelCase(string s)
        {
            return string.Concat(s.Select((c, i) => i > 0 && char.IsUpper(c) ? " " + c : c.ToString()));
        }
    }
}