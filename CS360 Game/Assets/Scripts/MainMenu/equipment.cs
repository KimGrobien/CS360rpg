using UnityEngine;

[CreateAssetMenu]
public class equipment : ScriptableObject
{
    public string itemName;
    public int itemID;
    public Sprite Icon;
    public int attack_bonus;
    public int defense_bonus;
    public int heal;
    public int bountyPrice;
}
