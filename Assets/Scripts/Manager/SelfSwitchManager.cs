using System.Collections.Generic;
using UnityEngine;

public class SelfSwitchManager : Singleton<SelfSwitchManager> {
	private Dictionary<string, bool> selfSwitches = new();

	public void Set(string mapId, int eventId, char switchName, bool value) {
		string key = GetKey(mapId, eventId, switchName);
		selfSwitches[key] = value;
	}

	public bool Get(string mapId, int eventId, char switchName) {
		string key = GetKey(mapId, eventId, switchName);
		return selfSwitches.TryGetValue(key, out bool result) && result;
	}

	private string GetKey(string mapId, int eventId, char switchName) {
		return $"{mapId}_{eventId}_{switchName}";
	}
}
