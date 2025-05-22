using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actors/Actor Data")]
public class ActorData : ScriptableObject {
	public int id;
	public string actorName;
	public Sprite bust;
	public Sprite face;
	public int level;
	public int maxHP;
	public int maxMP;
	public int attack;
	public int defense;
	public int agility;
	public int luck;

	public enum EquipmentSlot { Weapon, Armor, Accessory }

	[System.Serializable]
	public class EquipmentDefaults {
		public EquipmentSlot slot;
		public ItemData defaultItem;
	}

	public List<EquipmentDefaults> defaultEquipment;
}
