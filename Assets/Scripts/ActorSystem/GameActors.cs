using System.Collections.Generic;
using UnityEngine;

public class GameActors : Singleton<GameActors> {
	private Dictionary<int, ActorData> actorDatabase = new();

	public ActorData GetActor(int id) {
		actorDatabase.TryGetValue(id, out var actor);
		return actor;
	}

	public void RegisterActor(ActorData actor) {
		if (actor == null || actorDatabase.ContainsKey(actor.id)) return;
		actorDatabase[actor.id] = actor;
	}

	public void LoadActors(List<ActorData> actors) {
		actorDatabase.Clear();
		foreach (var actor in actors) {
			RegisterActor(actor);
		}
	}

	public string GetName(int id) {
		var actor = GetActor(id);
		return actor != null ? actor.actorName : $"[N{id}]";
	}
}
