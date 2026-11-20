# Click the Target

Open this project in Unity 6000.0.62f1. Choose **Tools > Play Click Target**.

Click the blue square 10 times in 15 seconds to win. The first click starts the
timer. Running out of time displays **You Lose!**. Click **Restart (R)** or press
R to try again. The counter and timer stop when the game ends.

- Scene: `Assets/ClickTarget/ClickTarget.unity`
- Custom component: `Assets/ClickTarget/ClickTargetGame.cs`
- Prefab: `Assets/ClickTarget/Target.prefab`, instantiated in the scene
- `Update()` reads mouse/keyboard input and decreases the timer using `Time.deltaTime`.
- Text is drawn with Unity's built-in GUI to avoid additional UI setup or packages.

For your demonstration, show a win by clicking 10 times, then restart, click once,
and wait 15 seconds to show a loss. Submit the repository link and video/GIF as
requested by your assignment.
