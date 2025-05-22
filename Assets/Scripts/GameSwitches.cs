using System.Collections.Generic;
using UnityEngine;

public class GameSwitches : Singleton<GameSwitches> {
	private Dictionary<int, bool> switches = new();

	public void Set(int id, bool value) {
		switches[id] = value;
	}

	public bool Get(int id) {
		return switches.TryGetValue(id, out bool value) && value;
	}
}
