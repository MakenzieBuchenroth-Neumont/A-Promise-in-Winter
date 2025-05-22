using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Event System/Commands/Control Self Switch")]
public class ControlSelfSwitchCommand : BaseEventCommand {
	public char selfSwitchName = 'A'; // A, B, C, D
	public bool setTo = true;

	public override IEnumerator Execute(EventExecutor executor) {
		var eventContext = executor.CurrentEventContext;
		string mapId = eventContext.mapId;
		int eventId = eventContext.eventId;

		SelfSwitchManager.Instance.Set(mapId, eventId, selfSwitchName, setTo);

		yield break;
	}

	public override string GetSummary() {
		return $"Self Switch {selfSwitchName} = {(setTo ? "ON" : "OFF")}";
	}
}
