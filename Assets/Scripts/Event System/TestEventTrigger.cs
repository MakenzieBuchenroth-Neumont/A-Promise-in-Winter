using UnityEngine;

public class TestEventTrigger : MonoBehaviour {
	public EventExecutor executor;

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (executor != null) {
				executor.StartEvent();
			}
			else {
				Debug.LogWarning("EventExecutor not assigned!");
			}
		}
	}
}
