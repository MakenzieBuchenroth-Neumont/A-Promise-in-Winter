using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class InventoryManager : Singleton<InventoryManager> {
	private List<InventoryEntry> inventory = new();

	public IReadOnlyList<InventoryEntry> Items => inventory;

	private int gold = 0;

	public int GetGold() => gold;

	public void AddGold(int amount) => gold += amount;
	public void SubtractGold(int amount) => Mathf.Max(0, gold - amount);
	public void SetGold(int value) => gold = Mathf.Max(0, value);

	protected override void Awake() {
		base.Awake();
		Debug.Log("InventoryManager initialized");
	}

	public void AddItem(ItemData item, int amount = 1) {
		var existing = inventory.Find(e => e.item == item);
		if (existing != null) {
			existing.quantity += amount;
		}
		else {
			inventory.Add(new InventoryEntry(item, amount));
		}
	}

	public bool RemoveItem(ItemData item, int amount = 1) {
		var entry = inventory.Find(e => e.item == item);
		if (entry == null || entry.quantity < amount)
			return false;

		entry.quantity -= amount;
		if (entry.quantity <= 0)
			inventory.Remove(entry);
		return true;
	}

	public List<ItemData> GetItemsByType(ItemType type) {
		if (type == ItemType.All) {
			return inventory.Select(i => i.item).ToList();
		}

		return inventory
			.Where(i => i.item.type == type)
			.Select(i => i.item)
			.ToList();
	}

	public ItemData GetItemByID(int id) {
		foreach (var entry in inventory) {
			if (entry.item.itemID == id)
				return entry.item;
		}
		return null;
	}

	public int GetQuantity(ItemData item) {
		var entry = inventory.Find(e => e.item == item);
		return entry?.quantity ?? 0;
	}
}
