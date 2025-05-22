using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Event System/Commands/Control Timer")]
public class ControlTimerCommand : BaseEventCommand {
	public enum TimerAction { Start, Stop }

	public TimerAction action = TimerAction.Start;
	public int minutes = 0;
	public int seconds = 30;

	public BaseEventCommand[] onTimerEndCommands;

	public override IEnumerator Execute(EventExecutor executor) {
		if (action == TimerAction.Start) {
			GameTimer.Instance.StartTimer(minutes, seconds, onTimerEndCommands);
		}
		else {
			GameTimer.Instance.StopTimer();
		}

		yield break;
	}

	public override string GetSummary() {
		return action == TimerAction.Start
			? $"Start Timer ({minutes}:{seconds:00})"
			: "Stop Timer";
	}
}
