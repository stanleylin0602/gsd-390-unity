using System.Collections.Generic;
using UnityEngine;

public class CoinRewardDemo : MonoBehaviour
{
    //attributes: SerializeField lets me change the timer in the inspector
    //Range keeps it between 1 and 5 seconds.
    [SerializeField, Range(1f, 5f)] private float collectionInterval = 3f;

    //generics: List<int> keeps the recent rewards in a list of integers
    //used to show the last few rewards on screen.
    private readonly List<int> recentRewards = new List<int>();
    private float timer;
    private float elapsed;
    private string message = "Watch the coin travel to the bank!";

    //stores coin total
    private int coins = 0;

    private void Start() //void start used, runs once when the component starts
    {
        Debug.Log("Coin reward demo started!", this);

        //adds a camera if the scene doesn't have one so the game view can display.
        if (Camera.main == null)
        {
            Camera camera = new GameObject("Demo Camera").AddComponent<Camera>();
            camera.tag = "MainCamera";
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.05f, 0.08f, 0.14f);
        }

        // polymorphism: reward variable holding a CoinReward object
        //calling GetAmount uses the overridden method in CoinReward.
        Reward reward = new CoinReward();

        //custom method with a parameter, passes the reward amount to AddCoins.
        AddCoins(reward.GetAmount());
    }

    private void Update() //void update used, runs every frame
    {
        //statics: Time.deltaTime gives the time since the last frame
        //used to count seconds so the timer doesn't depend on frame rate.
        elapsed += Time.deltaTime;
        timer += Time.deltaTime;
        float interval = Mathf.Max(1f, collectionInterval);
        while (timer >= interval)
        {
            timer -= interval;
            Reward reward = new CoinReward();
            AddCoins(reward.GetAmount());
        }
    }

    private bool TryGetLatestReward(out int amount)
    {
        //out parameter: amount gives back the latest reward to display
        //the bool tells us if there was a reward in the list.
        amount = 0;
        if (recentRewards.Count == 0) return false;
        amount = recentRewards[recentRewards.Count - 1];
        return true;
    }

    private void OnGUI()
    {
        //draws the coin and total in game view using the timer from Update.
        Matrix4x4 previousMatrix = GUI.matrix;
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,
            new Vector3(Screen.width / 960f, Screen.height / 540f, 1f));
        GUIStyle title = new GUIStyle(GUI.skin.label) { fontSize = 32 };
        GUIStyle label = new GUIStyle(GUI.skin.label) { fontSize = 22 };
        GUI.Label(new Rect(45, 30, 850, 50), "COIN REWARD / PART 2", title);
        GUI.Label(new Rect(45, 90, 850, 40), "Coins in bank: " + coins, label);
        GUI.Label(new Rect(45, 135, 850, 40), message, label);

        float progress = timer / Mathf.Max(1f, collectionInterval);
        float x = Mathf.Lerp(80f, 760f, progress);
        //static method Mathf.Sin makes the coin move up and down.
        float y = 260f + Mathf.Sin(elapsed * 4f) * 25f;
        GUI.color = new Color(1f, 0.78f, 0.2f);
        GUI.Box(new Rect(x, y, 70, 70), "");
        GUI.color = Color.black;
        GUI.Label(new Rect(x + 15, y + 17, 60, 40), "+10", label);
        GUI.color = new Color(0.35f, 0.85f, 0.8f);
        GUI.Box(new Rect(810, 245, 110, 110), "BANK");
        GUI.Box(new Rect(45, 385, 870f * progress, 12), "");
        GUI.color = Color.white;
        if (TryGetLatestReward(out int latest))
            GUI.Label(new Rect(45, 420, 850, 35), "Latest reward: " + latest
                + "   |   Recent: " + string.Join(", ", recentRewards), label);
        if (GUI.Button(new Rect(45, 475, 170, 40), "Reset demo"))
        {
            coins = 0;
            timer = 0f;
            elapsed = 0f;
            recentRewards.Clear();
            message = "Watch the coin travel to the bank!";
        }
        GUI.matrix = previousMatrix;
    }

    private void AddCoins(int amount)
    {
        //access the coins field to remember its previous value.
        int previousCoins = coins;

        //modify the coins field by adding the amount parameter.
        coins += amount;
        recentRewards.Add(amount);
        if (recentRewards.Count > 5) recentRewards.RemoveAt(0);
        message = "Collected " + amount + " coins! " + previousCoins + " -> " + coins;

        //prints message in debug console
        Debug.Log("Collected " + amount + " coins! Total: "
            + previousCoins + " -> " + coins, this);
    }
}

//base class
public abstract class Reward
{
    public abstract int GetAmount();
}

public class CoinReward : Reward
{
    //override supplies the coin-specific behavior used by the base reference
    public override int GetAmount()
    {
        return 10;
    }
}
