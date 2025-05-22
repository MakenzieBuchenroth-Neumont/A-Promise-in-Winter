using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NumberInputWindow : Singleton<NumberInputWindow> {
	public GameObject windowRoot;
	public TMP_InputField inputField;
	public Button upButton;
	public Button downButton;
	public Button confirmButton;

	public AudioClip keySound;
	public AudioClip submitSound;
	public AudioSource audioSource;

	private Action<int> onComplete;
	private int currentValue = 0;
	private int maxDigits = 4;

	private void Start() {
		upButton.onClick.AddListener(() => AdjustValue(1));
		downButton.onClick.AddListener(() => AdjustValue(-1));
		confirmButton.onClick.AddListener(Submit);
		inputField.onValueChanged.AddListener(OnInputChanged);

		windowRoot.SetActive(false);
	}

	public void Show(int digits, Action<int> callback) {
		maxDigits = Mathf.Clamp(digits, 1, 8);
		onComplete = callback;
		currentValue = 0;

		inputField.characterLimit = maxDigits;
		inputField.text = currentValue.ToString();
		windowRoot.SetActive(true);
		inputField.ActivateInputField();
	}

	private void OnInputChanged(string val) {
		if (int.TryParse(val, out int result)) {
			currentValue = Mathf.Clamp(result, 0, GetMaxValue());
			PlaySound(keySound);
		}
	}


	private void AdjustValue(int delta) {
		currentValue += delta;
		currentValue = Mathf.Clamp(currentValue, 0, GetMaxValue());
		inputField.text = currentValue.ToString();
		PlaySound(keySound);
	}


	private int GetMaxValue() {
		return (int)Mathf.Pow(10, maxDigits) - 1;
	}

	private void Submit() {
		PlaySound(submitSound);
		onComplete?.Invoke(currentValue);
		windowRoot.SetActive(false);
	}

	private void PlaySound(AudioClip clip, float pitchVariation = 0.05f) {
		if (clip != null && audioSource != null) {
			float originalPitch = audioSource.pitch;
			float randomPitch = UnityEngine.Random.Range(1f - pitchVariation, 1f + pitchVariation);
			audioSource.pitch = randomPitch;

			audioSource.PlayOneShot(clip);

			// Reset pitch so it doesn't affect future non-randomized sounds
			audioSource.pitch = originalPitch;
		}
	}
}
