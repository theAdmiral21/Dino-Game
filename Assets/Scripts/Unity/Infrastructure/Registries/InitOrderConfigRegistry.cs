
public static class InitOrderConfigRegistry
{
    private static InitOrderConfigSO _config;

    public static InitOrderConfigSO Config
    {
        get
        {
            if (_config == null)
            {
#if UNITY_EDITOR
                // Editor-only load by path/label, so this works without a Resources folder in builds
                var guids = UnityEditor.AssetDatabase.FindAssets("t:InitOrderConfigSO");
                if (guids.Length > 0)
                {
                    var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
                    _config = UnityEditor.AssetDatabase.LoadAssetAtPath<InitOrderConfigSO>(path);
                }
#endif
            }
            return _config;
        }
    }
}
