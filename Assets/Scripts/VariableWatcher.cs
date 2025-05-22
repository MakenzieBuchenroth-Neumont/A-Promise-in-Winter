using UnityEngine;

public class VariableWatcher : MonoBehaviour {
	public int watchVariableId = 1;

	void OnEnable() {
		GameVariables.Instance.OnVariableChanged += HandleVariableChanged;
	}

	void OnDisable() {
		GameVariables.Instance.OnVariableChanged -= HandleVariableChanged;
	}

	private void HandleVariableChanged(int id, int value) {
		if (id == watchVariableId) {
			Debug.Log($"Variable[{id}] changed to {value}");
		}
	}
}
