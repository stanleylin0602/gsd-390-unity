using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class SetupClickTarget
{
    private const string ScenePath = "Assets/ClickTarget/ClickTarget.unity";

    public static void Create()
    {
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        Camera camera = new GameObject("Main Camera").AddComponent<Camera>();
        camera.tag = "MainCamera";
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(0.055f, 0.075f, 0.12f);

        GameObject target = new GameObject("Target");
        target.AddComponent<ClickTargetGame>();
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(target, "Assets/ClickTarget/Target.prefab");
        Object.DestroyImmediate(target);
        PrefabUtility.InstantiatePrefab(prefab);
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), ScenePath);
        AssetDatabase.SaveAssets();
        Debug.Log("CLICK_TARGET_SETUP_OK: Scene saved with Target prefab and ClickTargetGame component.");
    }

    [MenuItem("Tools/Play Click Target")]
    public static void OpenAndPlay()
    {
        EditorSceneManager.OpenScene(ScenePath);
        EditorApplication.isPlaying = true;
    }
}
