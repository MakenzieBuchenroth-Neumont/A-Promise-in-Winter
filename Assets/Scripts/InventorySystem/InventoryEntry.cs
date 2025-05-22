[System.Serializable]
public class InventoryEntry {
	public ItemData item;
	public int quantity;

	public InventoryEntry(ItemData item, int quantity) {
		this.item = item;
		this.quantity = quantity;
	}
}
