using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using static ActorData;

public class ActorInstance {
	public ActorData baseData;

	public int level;
	public int currentHP;
	public int currentMP;
	public int exp;

	public Dictionary<EquipmentSlot, ItemData> equipment = new();
	public List<string> tags = new(); // For class types, traits, etc.

	public ActorInstance(ActorData data) {
		baseData = data;
		level = data.level;
		currentHP = data.maxHP;
		currentMP = data.maxMP;
		exp = 0;

		InitializeEquipment();
	}

	public string Name => baseData.actorName;
	public Sprite Face => baseData.face;
	public Sprite Bust => baseData.bust;

	public int MaxHP => baseData.maxHP;
	public int MaxMP => baseData.maxMP;

	public void GainExp(int amount) {
		exp += amount;
		// TODO: Implement level-up logic later
	}

	public void InitializeEquipment() {
		if (baseData.defaultEquipment == null) return;

		foreach (var slot in baseData.defaultEquipment) {
			equipment[slot.slot] = slot.defaultItem;
		}
	}

	public ItemData GetEquipped(EquipmentSlot slot) {
		return equipment.TryGetValue(slot, out var item) ? item : null;
	}

	public void Equip(EquipmentSlot slot, ItemData item) {
		equipment[slot] = item;
	}

	public bool HasTag(string tag) => tags.Contains(tag);
}
