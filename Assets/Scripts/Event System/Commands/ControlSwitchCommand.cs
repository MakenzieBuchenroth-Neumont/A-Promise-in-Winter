using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Event System/Commands/Control Switch")]
public class ControlSwitchCommand : BaseEventCommand {
	[Tooltip("ID of the switch to control.")]
	public int switchId = 1;

	[Tooltip("Set to ON or OFF.")]
	public bool setTo = true;

	public override IEnumerator Execute(EventExecutor executor) {
		GameSwitches.Instance.Set(switchId, setTo);
		yield break;
	}

	public override string GetSummary() {
		return $"Switch [{switchId}] = {(setTo ? "ON" : "OFF")}";
	}
}
