using System.Collections;
using UnityEngine;

public enum VariableOperation { Set, Add, Subtract, Multiply, Divide }
public enum VariableSourceType { Constant, Variable, PartyGold, PartySize }

[CreateAssetMenu(menuName = "Event System/Commands/Control Variable")]
public class ControlVariableCommand : BaseEventCommand {
	public int variableId = 1;
	public VariableOperation operation = VariableOperation.Set;

	[Header("Source")]
	public VariableSourceType sourceType = VariableSourceType.Constant;
	public int constantValue = 0;
	public int sourceVariableId = 1;

	public override IEnumerator Execute(EventExecutor executor) {
		int targetValue = GameVariables.Instance.Get(variableId);
		int inputValue = ResolveInputValue();

		int result = targetValue;

		switch (operation) {
			case VariableOperation.Set: result = inputValue; break;
			case VariableOperation.Add: result += inputValue; break;
			case VariableOperation.Subtract: result -= inputValue; break;
			case VariableOperation.Multiply: result *= inputValue; break;
			case VariableOperation.Divide: result = inputValue != 0 ? result / inputValue : 0; break;
		}

		GameVariables.Instance.Set(variableId, result);
		yield break;
	}

	private int ResolveInputValue() {
		switch (sourceType) {
			case VariableSourceType.Constant: return constantValue;
			case VariableSourceType.Variable: return GameVariables.Instance.Get(sourceVariableId);
			case VariableSourceType.PartyGold: return InventoryManager.Instance.GetGold();
			case VariableSourceType.PartySize: return PartyManager.Instance.Members.Count;
			default: return 0;
		}
	}

	public override string GetSummary() {
		string op = operation.ToString().ToUpper();
		string val = sourceType == VariableSourceType.Constant ? constantValue.ToString() :
					 sourceType == VariableSourceType.Variable ? $"V[{sourceVariableId}]" :
					 sourceType.ToString();
		return $"V[{variableId}] {op} {val}";
	}
}
