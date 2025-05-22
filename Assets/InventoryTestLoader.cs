using UnityEngine;

public class InventoryTestLoader : MonoBehaviour {
	public ItemData[] testItems;

	void Start() {
		// Add each item to inventory
		foreach (var item in testItems) {
			InventoryManager.Instance.AddItem(item, Random.Range(1, 5));
		}
	}
}
