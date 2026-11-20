using System.IO;
using UnityEditor;
using UnityEngine;

// Editor-only recorder: captures the real Game view while the game is played.
[InitializeOnLoad]
public static class RecordClickTarget
{
    private static double nextFrame;
    private static int frame;
    private static bool recording;

    static RecordClickTarget() { EditorApplication.update += Tick; }

    private static void Tick()
    {
        if (File.Exists("Logs/record-click-target.request") && EditorApplication.isPlaying && !EditorApplication.isCompiling)
        {
            try { File.Delete("Logs/record-click-target.request"); }
            catch (IOException) { return; } // Wait if the request is still being written.
            Directory.CreateDirectory("Recordings/ClickTargetFrames");
            frame = 0;
            nextFrame = EditorApplication.timeSinceStartup;
            recording = true;
        }
        if (!recording || !EditorApplication.isPlaying || EditorApplication.isCompiling) return;
        if (File.Exists("Logs/stop-click-target.request"))
        {
            try { File.Delete("Logs/stop-click-target.request"); }
            catch (IOException) { return; }
            recording = false;
            Debug.Log("Click Target recording saved: " + frame + " frames.");
            return;
        }
        if (EditorApplication.timeSinceStartup < nextFrame) return;
        nextFrame = EditorApplication.timeSinceStartup + 0.125;
        ScreenCapture.CaptureScreenshot(Path.GetFullPath("Recordings/ClickTargetFrames/frame-" + frame.ToString("D4") + ".png"));
        frame++;
        if (frame >= 480) recording = false;
    }
}
