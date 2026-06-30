using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Ai.Behavior;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class BehaviorCreatorWindow : EditorWindow
    {
        private string _behaviorName = "";
        private string _contextName = "";

        private GUIContent _dropDownSelection = new();

        public static void Open()
        {
            GetWindow<BehaviorCreatorWindow>("Create Behavior Node");
        }

        private void OnGUI()
        {
            if (_contextName == "") _dropDownSelection.text = "None";

            GUILayout.Label("Create Behavior Node", EditorStyles.boldLabel);

            // behavior name field with focus
            GUI.SetNextControlName("BehaviorNameField");
            _behaviorName = EditorGUILayout.TextField("Behavior Name", _behaviorName);
            EditorGUI.FocusTextInControl("BehaviorNameField");

            GUILayout.Label("Selected Behavior Context", EditorStyles.boldLabel);

            if (EditorGUILayout.DropdownButton(_dropDownSelection, FocusType.Keyboard))
            {
                BuildDropDown();
            }

            // Submit button and null check
            if (GUILayout.Button("Create"))
            {
                if (string.IsNullOrWhiteSpace(_behaviorName))
                {
                    Debug.LogWarning("Please enter a behavior name");
                    return;
                }
                if (string.IsNullOrWhiteSpace(_contextName))
                {
                    Debug.LogWarning("Please select a context");
                    return;
                }

                BehaviorGenerator.CreateBehavior(_behaviorName, _contextName);
                Close();
            }

            GUILayout.Space(10);
        }

        private GenericMenu BuildDropDown()
        {
            GenericMenu dropDown = new GenericMenu();
            List<Type> behaviors = GetContexts();
            foreach (var behavior in behaviors)
            {
                // just get the end of the string
                string[] contextName = behavior.ToString().Split('.');
                GUIContent content = new GUIContent { text = contextName[^1] };
                dropDown.AddItem(content, false, OnItemSelected, content);
            }
            dropDown.DropDown(GUILayoutUtility.GetLastRect());
            return dropDown;
        }

        private void OnItemSelected(object userData)
        {
            GUIContent item = userData as GUIContent;
            if (item == null) return;
            _contextName = item.text;
            _dropDownSelection.text = item.text;
        }

        private static List<Type> GetContexts()
        {
            // This is where game play code lives
            Assembly assembly = Assembly.Load("Application");

            Type interfaceType = typeof(IBehaviorContext);
            // use linq to get the types
            var behaviors = assembly.GetTypes().Where(t => interfaceType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract).ToList();

            return behaviors;
        }
    }
}