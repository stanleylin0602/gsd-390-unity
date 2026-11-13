using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

//saves game view screenshots in the editor to make a GIF.
[InitializeOnLoad]
public static class RecordCoinDemo
{
    private static double nextFrame;
    static RecordCoinDemo() { EditorApplication.update += Tick; }

    [MenuItem("Tools/Record Coin Demo")]
    public static void Record()
    {
        if (EditorApplication.isPlaying) return;
        EditorSceneManager.OpenScene("Assets/CoinRewardDemo.unity");
        Directory.CreateDirectory("Recordings/Part2Frames");
        SessionState.SetInt("CoinDemoFrame", 0);
        SessionState.SetBool("CoinDemoRecording", true);
        EditorApplication.isPlaying = true;
    }

    private static void Tick()
    {
        if (File.Exists("Logs/record-demo.request") && !EditorApplication.isPlayingOrWillChangePlaymode)
        {
            File.Delete("Logs/record-demo.request");
            Record();
        }
        if (!SessionState.GetBool("CoinDemoRecording", false) || !EditorApplication.isPlaying
            || EditorApplication.isCompiling || Time.time < 0.3f) return;
        if (EditorApplication.timeSinceStartup < nextFrame) return;
        nextFrame = EditorApplication.timeSinceStartup + 0.1;
        int frame = SessionState.GetInt("CoinDemoFrame", 0);
        if (frame >= 100)
        {
            SessionState.SetBool("CoinDemoRecording", false);
            EditorApplication.isPlaying = false;
            Debug.Log("Recorded 100 Game View frames in Recordings/Part2Frames.");
            return;
        }
        ScreenCapture.CaptureScreenshot(Path.GetFullPath("Recordings/Part2Frames/frame-" + frame.ToString("D3") + ".png"));
        SessionState.SetInt("CoinDemoFrame", frame + 1);
    }
}
