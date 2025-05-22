using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Event System/Commands/Jump to Label")]
public class JumpToLabelCommand : BaseEventCommand {
	public string labelName;

	public override IEnumerator Execute(EventExecutor executor) {
		if (executor.TryGetLabelIndex(labelName, out int targetIndex)) {
			executor.SetJumpIndex(targetIndex);
		}
		else {
			Debug.LogWarning($"JumpToLabelCommand: Label '{labelName}' not found.");
		}

		yield break;
	}

	public override string GetSummary() {
		return $"Jump to Label: {labelName}";
	}
}
