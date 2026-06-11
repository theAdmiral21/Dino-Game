using UnityEngine;
using UnityEditor;

namespace Editor
{
    public class ActionGeneratorWindow : EditorWindow
    {
        private string _actionName = "";

        private bool _createRequest = true;
        private bool _createResult = true;
        private bool _createRule = true;
        public static void Open()
        {
            GetWindow<ActionGeneratorWindow>("Create Action");
        }

        private void OnGUI()
        {
            GUILayout.Label("Create New Action", EditorStyles.boldLabel);

            GUI.SetNextControlName("ActionNameField");
            _actionName = EditorGUILayout.TextField("Action Name", _actionName);
            EditorGUI.FocusTextInControl("ActionNameField");

            _createRequest = EditorGUILayout.Toggle("Request", _createRequest);
            _createResult = EditorGUILayout.Toggle("Result", _createResult);
            _createRule = EditorGUILayout.Toggle("Rule", _createRule);

            if (GUILayout.Button("Create"))
            {
                if (string.IsNullOrWhiteSpace(_actionName))
                {
                    Debug.LogWarning("Please enter an action name");
                    return;
                }

                ActionGenerator.CreateAction(_actionName,
                                            _createRequest,
                                            _createResult,
                                            _createRule);
                Close();
            }

            GUILayout.Space(10);
        }
    }
}