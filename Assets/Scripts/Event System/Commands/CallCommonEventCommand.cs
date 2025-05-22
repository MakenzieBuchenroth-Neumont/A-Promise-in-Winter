using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Event System/Commands/Call Common Event")]
public class CallCommonEventCommand : BaseEventCommand {
	public CommonEvent commonEvent;

	public override IEnumerator Execute(EventExecutor executor) {
		if (commonEvent == null) {
			Debug.LogWarning("CallCommonEventCommand: No CommonEvent assigned.");
			yield break;
		}

		foreach (var command in commonEvent.commands) {
			yield return command.Execute(executor);

			if (executor.ShouldExitEvent)
				yield break;

			if (executor.ShouldBreakLoop)
				yield break;
		}
	}

	public override string GetSummary() {
		return commonEvent != null ? $"Call Common Event: {commonEvent.name}" : "Call Common Event: <None>";
	}
}
