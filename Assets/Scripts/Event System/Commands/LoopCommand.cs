using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event System/Commands/Loop")]
public class LoopCommand : BlockCommand {
	public override IEnumerator Execute(EventExecutor executor) {
		while (true) {
			foreach (var command in childCommands) {
				yield return command.Execute(executor);
				if (executor.ShouldBreakLoop) {
					executor.ShouldBreakLoop = false;
					yield break;
				}
			}
		}
	}

	public override string GetSummary() => "Loop";
}
