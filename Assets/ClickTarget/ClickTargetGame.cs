using UnityEngine;

// This component lives on the Target prefab. It handles input, time, and drawing.
public class ClickTargetGame : MonoBehaviour
{
    public int clicksToWin = 10;
    public float timeLimit = 15f;

    private int clicks;
    private float timeLeft;
    private bool started;
    private bool finished;

    private Rect TargetRect => new Rect(Screen.width / 2f - 80, Screen.height / 2f - 50, 160, 160);

    private void Start()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        clicks = 0;
        timeLeft = timeLimit;
        started = false;
        finished = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ResetGame();
        if (finished) return;

        // Start timing with the first click, so the player has time to read.
        if (started)
        {
            timeLeft = Mathf.Max(0, timeLeft - Time.deltaTime);
            if (timeLeft <= 0)
            {
                finished = true;
                return;
            }
        }

        // Input uses bottom-left coordinates; the on-screen GUI uses top-left.
        Vector2 mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        if (Input.GetMouseButtonDown(0) && TargetRect.Contains(mouse))
        {
            started = true;
            clicks++;
            if (clicks >= clicksToWin) finished = true;
        }
    }

    private void OnGUI()
    {
        GUIStyle title = new GUIStyle(GUI.skin.label)
        {
            fontSize = 32, alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold
        };
        GUIStyle text = new GUIStyle(GUI.skin.label)
        {
            fontSize = 20, alignment = TextAnchor.MiddleCenter
        };
        GUI.Label(new Rect(0, 25, Screen.width, 50), "CLICK THE TARGET", title);
        GUI.Label(new Rect(0, 80, Screen.width, 35),
            "Click the square " + clicksToWin + " times before time runs out.", text);
        GUI.Label(new Rect(0, 120, Screen.width, 35),
            "Clicks: " + clicks + " / " + clicksToWin + "     Time: " + timeLeft.ToString("0.0") + "s", text);

        GUI.color = finished ? (clicks >= clicksToWin ? Color.green : Color.gray) : new Color(0.25f, 0.8f, 1f);
        GUI.DrawTexture(TargetRect, Texture2D.whiteTexture);
        GUI.color = Color.black;
        GUI.Label(TargetRect, finished ? "DONE" : "CLICK ME", title);
        GUI.color = Color.white;

        string message = finished ? (clicks >= clicksToWin ? "You Win!" : "You Lose!")
            : started ? "Keep clicking!" : "Your first click starts the timer.";
        GUI.Label(new Rect(0, TargetRect.yMax + 20, Screen.width, 45), message, title);
        if (GUI.Button(new Rect(Screen.width / 2f - 90, TargetRect.yMax + 80, 180, 40), "Restart (R)"))
            ResetGame();
    }
}
