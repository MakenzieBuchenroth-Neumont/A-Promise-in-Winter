using UnityEngine;

public enum ItemType { Weapon, Armor, Item, KeyItem, All }

[CreateAssetMenu(menuName = "Items/Item Data")]
public class ItemData : ScriptableObject {
	public int itemID;
	public string itemName;
	public string description;
	public Sprite icon;

	public ItemType type;
	public ActorData.EquipmentSlot? equipSlot; // Null for non-equippable items

	public int attackBonus;
	public int defenseBonus;
	public int value; 
}
