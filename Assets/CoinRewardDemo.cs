using UnityEngine;

public class CoinRewardDemo : MonoBehaviour
{
    //stores coin total
    private int coins = 0;

    private void Start() //void start used, runs once when the component starts
    {
        Debug.Log("Coin reward demo started!", this);

        // polymorphism: reward variable holding a CoinReward object
        //calling GetAmount uses the overridden method in CoinReward.
        Reward reward = new CoinReward();

        //custom method with a parameter, passes the reward amount to AddCoins.
        AddCoins(reward.GetAmount());
    }

    private void AddCoins(int amount)
    {
        //access the coins field to remember its previous value.
        int previousCoins = coins;

        //modify the coins field by adding the amount parameter.
        coins += amount;

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
