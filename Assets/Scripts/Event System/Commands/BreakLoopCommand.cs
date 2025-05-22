using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Event System/Commands/Break Loop")]
public class BreakLoopCommand : BlockCommand {
	public override IEnumerator Execute(EventExecutor executor) {
		executor.ShouldBreakLoop = true;
		yield break;
	}

	public override string GetSummary() => "Break Loop";
}
