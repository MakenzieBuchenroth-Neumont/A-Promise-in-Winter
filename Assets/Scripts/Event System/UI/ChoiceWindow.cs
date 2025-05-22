using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceWindow : Singleton<ChoiceWindow> {
	public GameObject windowRoot;
	public GameObject choiceButtonPrefab;
	public Transform choiceContainer;

	private Action<int> onChoiceSelected;

	public void Show(List<string> choices, Action<int> callback) {
		ClearChoices();
		onChoiceSelected = callback;

		for (int i = 0; i < choices.Count; i++) {
			var btnObj = Instantiate(choiceButtonPrefab, choiceContainer);
			var text = btnObj.GetComponentInChildren<TMP_Text>();
			text.text = choices[i];

			int choiceIndex = i;
			btnObj.GetComponent<Button>().onClick.AddListener(() => {
				OnChoiceSelected(choiceIndex);
			});
		}

		windowRoot.SetActive(true);
	}

	private void OnChoiceSelected(int index) {
		windowRoot.SetActive(false);
		onChoiceSelected?.Invoke(index);
	}

	private void ClearChoices() {
		foreach (Transform child in choiceContainer) {
			Destroy(child.gameObject);
		}
	}
}
