using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Event System/Commands/Input Number")]
public class InputNumberCommand : BaseEventCommand {
	[Tooltip("Variable ID to store the result in")]
	public int variableId = 1;

	[Tooltip("Number of digits (1–8)")]
	[Range(1, 8)]
	public int digits = 4;

	public override IEnumerator Execute(EventExecutor executor) {
		bool waiting = true;
		int result = 0;

		NumberInputWindow.Instance.Show(digits, (input) => {
			result = input;
			waiting = false;
		});

		while (waiting) yield return null;

		GameVariables.Instance.Set(variableId, result);
	}

	public override string GetSummary() {
		return $"Input Number ({digits} digits) → Variable {variableId}";
	}
}
