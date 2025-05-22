using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;
using DG.Tweening;

public class InventoryMenuManager : MonoBehaviour {
	public GameObject itemEntryPrefab;
	public Transform column1, column2, column3;

	public TMP_Text nameText, descriptionText;
	public Image iconImage;

	public Button itemTab, weaponTab, armorTab, keyItemTab;

	private ItemType currentTab = ItemType.Item;

	public Button useButton;
	public Button equipButton;
	private ItemData selectedItem;

	public GameObject tabHeaderPanel;     // the panel with tab buttons
	public GameObject itemDetailPanel;    // the panel with item info + use/equip

	public Button backButton;

	public CanvasGroup tabCanvasGroup;
	public CanvasGroup detailCanvasGroup;

	public GameObject inventoryPanelRoot;

	private bool isOpen = false;

	void Start() {
		gameObject.SetActive(true);

		itemTab.onClick.AddListener(() => SelectTab(ItemType.Item));
		weaponTab.onClick.AddListener(() => SelectTab(ItemType.Weapon));
		armorTab.onClick.AddListener(() => SelectTab(ItemType.Armor));
		keyItemTab.onClick.AddListener(() => SelectTab(ItemType.KeyItem));
		useButton.onClick.AddListener(() => UseSelectedItem());
		equipButton.onClick.AddListener(() => EquipSelectedItem());
		backButton.onClick.AddListener(DeselectItem);

		SelectTab(ItemType.Item);

		GameInputManager.Instance.OnOpenInventory += ToggleMenu;
	}

	public void ToggleMenu() {
		isOpen = !isOpen;
		inventoryPanelRoot.SetActive(isOpen);
	}

	public void SelectTab(ItemType tabType) {
		currentTab = tabType;
		RefreshInventoryDisplay();
	}

	private void RefreshInventoryDisplay() {
		foreach (Transform col in new[] { column1, column2, column3 }) {
			foreach (Transform child in col)
				Destroy(child.gameObject);
		}

		var items = InventoryManager.Instance.GetItemsByType(currentTab);
		for (int i = 0; i < items.Count; i++) {
			var item = items[i];
			var col = i % 3 == 0 ? column1 : i % 3 == 1 ? column2 : column3;

			var entry = Instantiate(itemEntryPrefab, col);
			var count = InventoryManager.Instance.GetQuantity(item);
			entry.transform.Find("Label").GetComponent<TMP_Text>().text = $"{item.itemName} x{count}";

			entry.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;

			var itemCopy = item;
			entry.GetComponent<Button>().onClick.AddListener(() => HandleItemClick(itemCopy));
		}
	}

	private void HandleItemClick(ItemData item) {
		selectedItem = item;

		nameText.text = item.itemName;
		descriptionText.text = item.description;
		iconImage.sprite = item.icon;

		useButton.gameObject.SetActive(item.type == ItemType.Item);
		equipButton.gameObject.SetActive(item.type == ItemType.Weapon || item.type == ItemType.Armor);

		ShowDetailPanel();
	}

	private void UseSelectedItem() {
		Debug.Log($"Used {selectedItem.itemName}");
		InventoryManager.Instance.RemoveItem(selectedItem);
		RefreshInventoryDisplay();
		DeselectItem();
	}

	private void EquipSelectedItem() {
		Debug.Log($"Equipped {selectedItem.itemName}");
		DeselectItem();
	}

	private void DeselectItem() {
		detailCanvasGroup.DOFade(0, 0.2f).OnComplete(() =>
		{
			itemDetailPanel.SetActive(false);
			tabHeaderPanel.SetActive(true);
			tabCanvasGroup.alpha = 0;
			tabCanvasGroup.DOFade(1, 0.2f);

			RefreshInventoryDisplay();
		});
	}

	private void ShowDetailPanel() {
		tabCanvasGroup.DOFade(0, 0.2f).OnComplete(() =>
		{
			tabHeaderPanel.SetActive(false);
			itemDetailPanel.SetActive(true);
			detailCanvasGroup.alpha = 0;
			detailCanvasGroup.DOFade(1, 0.2f);
		});
	}

}
