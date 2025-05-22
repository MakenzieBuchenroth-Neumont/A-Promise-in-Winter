using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Event System/Commands/Label")]
public class LabelCommand : BaseEventCommand {
	public string labelName;

	public override IEnumerator Execute(EventExecutor executor) {
		// Labels do nothing when run
		yield break;
	}

	public override string GetSummary() {
		return $"Label: {labelName}";
	}
}
