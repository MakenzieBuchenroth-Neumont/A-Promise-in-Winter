using System.Collections.Generic;
using UnityEngine;

public class GameVariables : Singleton<GameVariables> {
	public event System.Action<int, int> OnVariableChanged;
	private Dictionary<int, int> variables = new();

	public int Get(int id) => variables.TryGetValue(id, out var val) ? val : 0;
	public void Set(int id, int value) { 
		variables[id] = value;
		OnVariableChanged?.Invoke(id, value);
	}
}
