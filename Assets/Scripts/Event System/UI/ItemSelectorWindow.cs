using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections.Generic;

public class ItemSelectorWindow : Singleton<ItemSelectorWindow> {
	public CanvasGroup panelGroup;
	public Transform listParent;
	public GameObject entryPrefab;
	public Button cancelButton;

	private System.Action<ItemData> onSelect;

	public void Show(ItemType filterType, bool onlyOwned, System.Action<ItemData> onSelect) {
		this.onSelect = onSelect;

		// Show and fade in panel
		panelGroup.gameObject.SetActive(true);
		panelGroup.DOKill();
		panelGroup.alpha = 0;
		panelGroup.DOFade(1f, 0.3f);
		panelGroup.interactable = true;
		panelGroup.blocksRaycasts = true;

		Clear();

		List<ItemData> items = new();

		if (filterType == ItemType.All) {
			items.AddRange(InventoryManager.Instance.GetItemsByType(ItemType.Item));
			items.AddRange(InventoryManager.Instance.GetItemsByType(ItemType.Weapon));
			items.AddRange(InventoryManager.Instance.GetItemsByType(ItemType.Armor));
			items.AddRange(InventoryManager.Instance.GetItemsByType(ItemType.KeyItem));
		}
		else {
			items = InventoryManager.Instance.GetItemsByType(filterType);
		}

		foreach (var item in items) {
			if (onlyOwned && InventoryManager.Instance.GetQuantity(item) <= 0)
				continue;

			var entry = Instantiate(entryPrefab, listParent);
			entry.GetComponent<ItemSelectorEntry>().Setup(item, () => {
				Hide(); // fade out after selection
				onSelect?.Invoke(item);
			});
		}

		cancelButton.onClick.RemoveAllListeners();
		cancelButton.onClick.AddListener(() => {
			Hide();
			onSelect?.Invoke(null);
		});
	}

	public void Hide() {
		panelGroup.DOKill();
		panelGroup.DOFade(0f, 0.2f).OnComplete(() =>
		{
			panelGroup.gameObject.SetActive(false);
		});
		panelGroup.interactable = false;
		panelGroup.blocksRaycasts = false;

	}

	private void Clear() {
		foreach (Transform child in listParent)
			Destroy(child.gameObject);
	}
}
