using UnityEngine;
using System;
using DG.Tweening;

public class GameInputManager : Singleton<GameInputManager> {
	public KeyCode openInventoryKey = KeyCode.I;
	public KeyCode openPartyMenuKey = KeyCode.P;
	public KeyCode openSettingsKey = KeyCode.Escape;

	public event Action OnOpenInventory;
	public event Action OnOpenPartyMenu;
	public event Action OnOpenSettings;

	protected override void Awake() {
		base.Awake();
		
		// Initialize DOTween
		DOTween.Init(false, true, LogBehaviour.ErrorsOnly)
			.SetCapacity(200, 50);
		
		LoadKeybindings();
	}

	void Update() {
		if (Input.GetKeyDown(openInventoryKey))
			OnOpenInventory?.Invoke();

		if (Input.GetKeyDown(openPartyMenuKey))
			OnOpenPartyMenu?.Invoke();

		if (Input.GetKeyDown(openSettingsKey))
			OnOpenSettings?.Invoke();

	}

	public void SetInventoryKey(KeyCode key) => openInventoryKey = key;
	public void SetPartyMenuKey(KeyCode key) => openPartyMenuKey = key;

	public void SaveKeybindings() {
		PlayerPrefs.SetInt("Key_Inventory", (int)openInventoryKey);
		PlayerPrefs.SetInt("Key_Party", (int)openPartyMenuKey);
		PlayerPrefs.Save();
	}

	public void LoadKeybindings() {
		if (PlayerPrefs.HasKey("Key_Inventory"))
			openInventoryKey = (KeyCode)PlayerPrefs.GetInt("Key_Inventory");
		if (PlayerPrefs.HasKey("Key_Party"))
			openPartyMenuKey = (KeyCode)PlayerPrefs.GetInt("Key_Party");
	}
}
