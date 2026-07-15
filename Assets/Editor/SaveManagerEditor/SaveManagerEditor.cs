#if UNITY_EDITOR
using Unity.Infrastructure.Lifecycle;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveManager))]
public class SaveManagerEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // keep all the normal serialized fields visible

        SaveManager manager = (SaveManager)target;

        GUILayout.Space(10);
        GUILayout.Label("Save Testing", EditorStyles.boldLabel);

        if (GUILayout.Button("Save To File"))
            manager.SerializeEntities();

        if (GUILayout.Button("Snap Shot Entities"))
            manager.SnapShotEntities();

        if (GUILayout.Button("Revert Entities"))
            manager.RevertEntities();
    }
}
#endif