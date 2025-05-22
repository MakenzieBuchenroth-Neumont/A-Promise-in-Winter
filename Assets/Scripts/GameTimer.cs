using UnityEngine;
using System;
using System.Collections;

public class GameTimer : Singleton<GameTimer> {
	private float timeRemaining = 0f;
	private bool running = false;

	public bool IsRunning => running;
	public float TimeRemaining => Mathf.Max(0, timeRemaining);
	private BaseEventCommand[] onEndCommands;

	public void StartTimer(int minutes, int seconds, BaseEventCommand[] onEnd = null) {
		timeRemaining = minutes * 60f + seconds;
		running = true;
		onEndCommands = onEnd;
	}

	private void Update() {
		if (!running) return;

		timeRemaining -= Time.deltaTime;

		if (timeRemaining <= 0f) {
			timeRemaining = 0f;
			running = false;

			if (onEndCommands != null && onEndCommands.Length > 0) {
				EventExecutor.Instance.StartCoroutine(RunTimerEndCommands());
			}
		}
	}

	public void StopTimer() {
		running = false;
		timeRemaining = 0f;
	}

	private IEnumerator RunTimerEndCommands() {
		foreach (var cmd in onEndCommands) {
			yield return cmd.Execute(EventExecutor.Instance);
		}
	}
}
