using System.Collections.Generic;
using UnityEngine;

public class PartyTestLoader : MonoBehaviour {
	public List<ActorData> testActors;

	void Start() {
		GameActors.Instance.LoadActors(testActors);
		var ids = new List<int>();
		foreach (var actor in testActors)
			ids.Add(actor.id);

		PartyManager.Instance.InitFromActorIds(ids);
	}
}
