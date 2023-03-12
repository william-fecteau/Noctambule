using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public int startingMoney = 200; // The amount of money the player starts with
    public int currentMoney; // The current amount of money the player has

    private void Start() {
        currentMoney = startingMoney;
    }

    // Add money to the player's current amount
    public void AddMoney(int amount) {
        currentMoney += amount;
    }

    // Subtract money from the player's current amount
    public void SubtractMoney(int amount) {
        currentMoney -= amount;
    }

    // Check if the player has enough money to make a purchase
    public bool CanAfford(int amount) {
        return currentMoney >= amount;
    }
}
