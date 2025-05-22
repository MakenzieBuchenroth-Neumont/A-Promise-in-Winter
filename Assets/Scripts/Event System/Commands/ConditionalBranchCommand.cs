using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Event System/Commands/Conditional Branch")]
public class ConditionalBranchCommand : BlockCommand {
	public enum ConditionType {
		Switch,
		VariableEquals,
		VariableGreater,
		VariableLess,
		TimerRunning
	}

	public ConditionType conditionType = ConditionType.Switch;

	[Header("Switch")]
	public int switchId;
	public bool switchExpectedValue = true;

	[Header("Variable")]
	public int variableId;
	public int compareValue;

	[Header("Else Block")]
	public List<BaseEventCommand> elseCommands = new();

	public override IEnumerator Execute(EventExecutor executor) {
		bool result = EvaluateCondition();

		var commandsToRun = result ? childCommands : elseCommands;

		foreach (var cmd in commandsToRun)
			yield return cmd.Execute(executor);
	}

	private bool EvaluateCondition() {
		switch (conditionType) {
			case ConditionType.Switch:
				return GameSwitches.Instance.Get(switchId) == switchExpectedValue;
			case ConditionType.VariableEquals:
				return GameVariables.Instance.Get(variableId) == compareValue;
			case ConditionType.VariableGreater:
				return GameVariables.Instance.Get(variableId) > compareValue;
			case ConditionType.VariableLess:
				return GameVariables.Instance.Get(variableId) < compareValue;
			case ConditionType.TimerRunning:
				return GameTimer.Instance.IsRunning;
			default:
				return false;
		}
	}

	public override string GetSummary() {
		return $"If {conditionType}...";
	}
}
