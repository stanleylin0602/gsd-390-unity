using UnityEngine;

public class CoinRewardDemo : MonoBehaviour
{
    // Field: stores the player's current coin total.
    private int coins = 0;

    // Unity calls Start once when this enabled component begins running.
    private void Start()
    {
        Debug.Log("Coin reward demo started!", this);

        // Polymorphism: a Reward variable holds a CoinReward object.
        // Calling GetAmount uses the overridden method in CoinReward.
        Reward reward = new CoinReward();

        // Custom method with a parameter: pass the reward amount to AddCoins.
        AddCoins(reward.GetAmount());
    }

    private void AddCoins(int amount)
    {
        // Access the coins field to remember its previous value.
        int previousCoins = coins;

        // Modify the coins field by adding the amount parameter.
        coins += amount;

        // Access the updated field and show the result in Unity's Console.
        Debug.Log("Collected " + amount + " coins! Total: "
            + previousCoins + " -> " + coins, this);
    }
}

// A standalone base class defines a method that derived rewards implement.
public abstract class Reward
{
    public abstract int GetAmount();
}

public class CoinReward : Reward
{
    // Override supplies the coin-specific behavior used by the base reference.
    public override int GetAmount()
    {
        return 10;
    }
}
