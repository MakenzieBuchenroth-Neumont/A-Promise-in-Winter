using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Event System/Commands/Exit Event Processing")]
public class ExitEventProcessingCommand : BaseEventCommand {
	public override IEnumerator Execute(EventExecutor executor) {
		executor.ShouldExitEvent = true;
		yield break;
	}

	public override string GetSummary() => "Exit Event Processing";
}
