using System.Collections.Generic;
using UnityEngine;

public class GameVariables : Singleton<GameVariables> {
	private Dictionary<int, int> variables = new();

	public int Get(int id) => variables.TryGetValue(id, out var val) ? val : 0;
	public void Set(int id, int value) => variables[id] = value;
}
