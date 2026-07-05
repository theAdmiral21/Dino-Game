using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Init Order Config")]
public class InitOrderConfigSO : ScriptableObject
{
    [Serializable]
    public class Entry
    {
        public string TypeName;
        public int Priority;
    }

    public List<Entry> Entries = new();

    public int GetPriority(string typeName, int fallback)
    {
        var entry = Entries.Find(e => e.TypeName == typeName);
        return entry != null ? entry.Priority : fallback;
    }

    public void SetPriority(string typeName, int priority)
    {
        var entry = Entries.Find(e => e.TypeName == typeName);
        if (entry != null) entry.Priority = priority;
        else Entries.Add(new Entry { TypeName = typeName, Priority = priority });
    }
}
