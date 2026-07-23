#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Runtime.InteropServices;

namespace Unity.Common.Unity.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(SerializedInterface<>), true)]
    public class SerializedInterfaceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var targetProp = property.FindPropertyRelative("_target");

            var interfaceType = GetInterfaceType(fieldInfo);
            if (interfaceType == null)
            {
                EditorGUI.LabelField(position, label.text, "Could not resolve interface type");
                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            var newObj = EditorGUI.ObjectField(
                position, label, targetProp.objectReferenceValue, typeof(UnityEngine.Object), true
            );

            if (newObj != targetProp.objectReferenceValue)
            {
                targetProp.objectReferenceValue = ValidateAssignment(newObj, interfaceType);
            }

            EditorGUI.EndProperty();
        }

        private UnityEngine.Object ValidateAssignment(UnityEngine.Object newObj, Type interfaceType)
        {
            if (newObj == null) return null;

            // Direct implementation by scriptable object or bare script
            if (interfaceType.IsInstanceOfType(newObj)) return newObj;

            // If a game object was dragged in, try and find the interface in it
            if (newObj is GameObject gObj)
            {
                var comp = gObj.GetComponent(interfaceType);
                if (comp != null) return comp;
            }

            Debug.LogError($"Object does not implement {interfaceType.Name}.");
            return null;
        }

        private static System.Type GetInterfaceType(System.Reflection.FieldInfo fieldInfo)
        {
            var fieldType = fieldInfo.FieldType;

            // Unwrap List<T> / T[] to get the element type
            if (fieldType.IsArray)
            {
                fieldType = fieldType.GetElementType();
            }
            else if (fieldType.IsGenericType &&
                     typeof(System.Collections.IEnumerable).IsAssignableFrom(fieldType) &&
                     fieldType.GetGenericTypeDefinition() != typeof(SerializedInterface<>))
            {
                fieldType = fieldType.GetGenericArguments()[0];
            }

            // fieldType should now be SerializedInterface<T> — walk up if it's a subclass
            while (fieldType != null && (!fieldType.IsGenericType ||
                   fieldType.GetGenericTypeDefinition() != typeof(SerializedInterface<>)))
            {
                fieldType = fieldType.BaseType;
            }

            return fieldType?.GetGenericArguments()[0];
        }
    }
}
#endif