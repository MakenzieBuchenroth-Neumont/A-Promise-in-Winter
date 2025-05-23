using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using System.Collections;

public class ScrollingTextWindow : Singleton<ScrollingTextWindow> {
	public CanvasGroup panelGroup;
	public TMP_Text scrollingText;
	public float scrollDuration = 6f;
	public KeyCode speedUpKey = KeyCode.Space;

	public KeyCode cancelKey = KeyCode.Z;

	private Tween currentTween;
	private Action onCompleteCallback;

	public void Show(string text, Action onComplete) {
		scrollingText.text = text;

		panelGroup.alpha = 0;
		panelGroup.gameObject.SetActive(true);
		DOTween.To(() => panelGroup.alpha, x => panelGroup.alpha = x, 1f, 0.3f);

		var rt = scrollingText.rectTransform;
		rt.anchoredPosition = new Vector2(0, -600); // start below

		currentTween = DOTween.To(() => rt.anchoredPosition.y, y => rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, y), 600f, scrollDuration)
			.SetEase(Ease.Linear)
			.OnComplete(() => {
				DOTween.To(() => panelGroup.alpha, x => panelGroup.alpha = x, 0f, 0.3f)
					.OnComplete(() => {
						panelGroup.gameObject.SetActive(false);
						onComplete?.Invoke();
					});
			});

		StartCoroutine(CheckForSpeedUp());
		onCompleteCallback = onComplete;
	}

	private IEnumerator CheckForSpeedUp() {
		while (currentTween != null && currentTween.IsPlaying()) {
			if (Input.GetKeyDown(speedUpKey)) {
				currentTween.timeScale = 2f;
			}

			if (Input.GetKeyDown(cancelKey)) {
				// Immediately complete the scroll
				currentTween.Kill();
				scrollingText.rectTransform.anchoredPosition = new Vector2(0, 600); // target position
				break;
			}

			yield return null;
		}

		// After skipping or completing, fade out
		DOTween.To(() => panelGroup.alpha, x => panelGroup.alpha = x, 0f, 0.3f)
			.OnComplete(() =>
		{
			panelGroup.gameObject.SetActive(false);
			onCompleteCallback?.Invoke(); // will be assigned in Show()
		});
	}

}