using TMPro;
using UnityEngine;

public class TimerDisplay : MonoBehaviour {
	public TextMeshProUGUI timerText;
	public GameObject panel; // optional parent to toggle visibility

	[Header("Color")]
	public Color normalColor = Color.white;
	public Color warningColor = Color.red;
	public float warningThreshold = 10f;

	[Header("Audio")]
	public AudioSource audioSource;
	public AudioClip warningBeep;
	public float beepInterval = 1f;

	[Header("Beep Timer")]
	private float beepTimer = 0f;

	private void Update() {
		float time = GameTimer.Instance.TimeRemaining;
		int minutes = Mathf.FloorToInt(time / 60f);
		int seconds = Mathf.FloorToInt(time % 60f);
		timerText.text = $"{minutes:00}:{seconds:00}";

		if (time <= warningThreshold) {
			// Flash red color
			float lerp = Mathf.PingPong(Time.time * 4f, 1f);
			timerText.color = Color.Lerp(normalColor, warningColor, lerp);

			// Play ticking sound at intervals
			beepTimer -= Time.deltaTime;
			if (beepTimer <= 0f && warningBeep != null && audioSource != null) {
				audioSource.PlayOneShot(warningBeep);
				beepTimer = beepInterval;
			}
		}
		else {
			timerText.color = normalColor;
			beepTimer = 0f;
		}
	}
}
