using UnityEngine;

// Scriptable class that holds data for equipment and bounty items
[CreateAssetMenu]
public class equipment : ScriptableObject
{
    public string itemName;
    public int itemID;
    public Sprite Icon;
    public bool owned;

    public int attack_bonus;
    public int defense_bonus;
    public int heal;
    public int bountyPrice;

    public void ToggleOwned()
    {
        owned = !owned;
    }
}