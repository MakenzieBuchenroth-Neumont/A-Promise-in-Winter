using System.IO;
using UnityEngine;

public static class SaveSystem {
	private static string path = Application.persistentDataPath + "/save.json";

	public static void SaveGame() {
		var partyData = PartyManager.Instance.SaveParty();
		string json = JsonUtility.ToJson(partyData, true);
		File.WriteAllText(path, json);
		Debug.Log("Game saved.");
	}

	public static void LoadGame() {
		if (!File.Exists(path)) {
			Debug.LogWarning("No save file found.");
			return;
		}

		string json = File.ReadAllText(path);
		var partyData = JsonUtility.FromJson<PartySaveData>(json);
		PartyManager.Instance.LoadParty(partyData);
		Debug.Log("Game loaded.");
	}
}
