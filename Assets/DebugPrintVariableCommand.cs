using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Event System/Debug Print")]
public class DebugPrintVariableCommand : BaseEventCommand {
	public int variableId;

	public override IEnumerator Execute(EventExecutor executor) {
		int value = GameVariables.Instance.Get(variableId);
		Debug.Log($"[Event] Variable {variableId} = {value}");
		yield break;
	}
}