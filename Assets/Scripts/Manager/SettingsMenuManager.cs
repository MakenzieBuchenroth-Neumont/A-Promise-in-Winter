using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class SettingsMenuManager : MonoBehaviour {
	public TMP_Text inventoryKeyText;
	public TMP_Text partyKeyText;
	public Button inventoryRebindButton;
	public Button partyRebindButton;
	public Button saveButton;

	private bool isOpen = false;

	public GameObject settingsMenuRoot;

	private enum BindingType { None, Inventory, Party }
	private BindingType rebinding = BindingType.None;

	void Start() {
		UpdateKeyTexts();

		inventoryRebindButton.onClick.AddListener(() => StartRebinding(BindingType.Inventory));
		partyRebindButton.onClick.AddListener(() => StartRebinding(BindingType.Party));
		saveButton.onClick.AddListener(() => GameInputManager.Instance.SaveKeybindings());
		GameInputManager.Instance.OnOpenSettings += ToggleMenu;
	}

	public void ToggleMenu() {
		isOpen = !isOpen;
		settingsMenuRoot.SetActive(isOpen);
	}

	void Update() {
		if (rebinding == BindingType.None) return;

		foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode))) {
			if (Input.GetKeyDown(key)) {
				switch (rebinding) {
					case BindingType.Inventory:
						GameInputManager.Instance.SetInventoryKey(key);
						break;
					case BindingType.Party:
						GameInputManager.Instance.SetPartyMenuKey(key);
						break;
				}

				rebinding = BindingType.None;
				UpdateKeyTexts();
				break;
			}
		}
	}

	void StartRebinding(BindingType type) {
		rebinding = type;
		if (type == BindingType.Inventory)
			inventoryKeyText.text = "Press any key...";
		else if (type == BindingType.Party)
			partyKeyText.text = "Press any key...";
	}

	void UpdateKeyTexts() {
		inventoryKeyText.text = $"Inventory: {GameInputManager.Instance.openInventoryKey}";
		partyKeyText.text = $"Party Menu: {GameInputManager.Instance.openPartyMenuKey}";
	}
}
