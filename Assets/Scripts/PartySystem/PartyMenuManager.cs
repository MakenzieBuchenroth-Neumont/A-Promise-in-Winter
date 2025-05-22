using static ActorData;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyMenuManager : MonoBehaviour {
	public GameObject panelPrefab;
	public Transform panelParent;
	//public GameObject actorOptionsMenu;

	private List<GameObject> currentPanels = new();
	private ActorInstance selectedActor;

	public GameObject partyPanelRoot;

	private bool isOpen = false;

	private void Start() {
		Debug.Log("PartyMenuManager Start called.");
		gameObject.SetActive(true);
		Show();
		GameInputManager.Instance.OnOpenPartyMenu += ToggleMenu;
	}

	public void ToggleMenu() {
		isOpen = !isOpen;
		partyPanelRoot.SetActive(isOpen);
	}

	public void Show() {
		ClearPanels();

		foreach (var actor in PartyManager.Instance.Members) {
			var panelGO = Instantiate(panelPrefab, panelParent);
			UpdatePanel(panelGO, actor);

			var clickable = panelGO.AddComponent<ActorPanelClickHandler>();
			clickable.Init(this, actor);

			currentPanels.Add(panelGO);
		}

		//actorOptionsMenu.SetActive(false);
	}

	private void ClearPanels() {
		foreach (var panel in currentPanels)
			Destroy(panel);
		currentPanels.Clear();
	}

	private void UpdatePanel(GameObject panel, ActorInstance actor) {
		Debug.Log($"actor.Name = {actor.Name}");

		Debug.Log($"Updating panel for: {actor.Name}");

		#region BackgroundImage
		var backgroundObj = panel.transform.Find("BackgroundImage");
		var backgroundImg = backgroundObj.GetComponent<Image>();
		if (backgroundImg == null)
			Debug.LogError("BackgroundImage missing from ActorPanel prefab!");
		else
			backgroundImg.sprite = actor.Bust;
		#endregion

		#region NameText
		var nameObj = panel.transform.Find("NameText");
		var nameText = nameObj?.GetComponent<TMP_Text>();
		if (nameText == null)
			Debug.LogError("NameText missing from ActorPanel prefab!");
		else
			nameText.text = actor.Name;
		#endregion

		var stats = $"HP: {actor.currentHP}/{actor.MaxHP}\n" +
					$"MP: {actor.currentMP}/{actor.MaxMP}";
		panel.transform.Find("StatsText").GetComponent<TMP_Text>().text = stats;

		var weapon = actor.GetEquipped(EquipmentSlot.Weapon)?.itemName ?? "-";
		var armor = actor.GetEquipped(EquipmentSlot.Armor)?.itemName ?? "-";
		var accessory = actor.GetEquipped(EquipmentSlot.Accessory)?.itemName ?? "-";

		string eq = $"Wpn: {weapon}\nArm: {armor}\nAcc: {accessory}";
		panel.transform.Find("EquipmentText").GetComponent<TMP_Text>().text = eq;
	}

	public void FocusActor(ActorInstance actor, Vector3 screenPos) {
		selectedActor = actor;
		//actorOptionsMenu.SetActive(true);
		//actorOptionsMenu.transform.position = screenPos; // Show near the clicked panel
	}

	public void OnEquipButton() {
		Debug.Log($"Equip clicked for {selectedActor.Name}");
		// TODO: Show equipment menu for selectedActor
	}

	public void OnStatusButton() {
		Debug.Log($"Status clicked for {selectedActor.Name}");
		// TODO: Show status details
	}

	public void OnBackButton() {
		//actorOptionsMenu.SetActive(false);
	}
}
