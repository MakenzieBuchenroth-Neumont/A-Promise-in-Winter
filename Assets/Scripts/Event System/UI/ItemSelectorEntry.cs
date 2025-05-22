using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSelectorEntry : MonoBehaviour {
	public TMP_Text label;
	public Image icon;

	private ItemData item;

	public void Setup(ItemData itemData, UnityEngine.Events.UnityAction onClick) {
		Debug.Log($"Setting up entry for: {itemData.itemName}");
		item = itemData;

		if (label != null) {
			label.text = $"{item.itemName} x{InventoryManager.Instance.GetQuantity(item)}";
		}
		if (icon != null) {
			icon.sprite = item.icon;
		}

		GetComponent<Button>().onClick.RemoveAllListeners();
		GetComponent<Button>().onClick.AddListener(onClick);
	}
}
