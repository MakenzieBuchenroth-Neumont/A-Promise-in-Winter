using System.Collections.Generic;
using UnityEngine;
using static ActorData;

public class PartyManager : Singleton<PartyManager> {
	private List<ActorInstance> partyMembers = new();

	public IReadOnlyList<ActorInstance> Members => partyMembers;

	public void InitFromActorIds(List<int> actorIds) {
		partyMembers.Clear();

		foreach (int id in actorIds) {
			var data = GameActors.Instance.GetActor(id);
			if (data != null)
				partyMembers.Add(new ActorInstance(data));
		}
	}

	public void AddToParty(int actorId) {
		var data = GameActors.Instance.GetActor(actorId);
		if (data != null && !partyMembers.Exists(a => a.baseData.id == actorId))
			partyMembers.Add(new ActorInstance(data));
	}

	public void RemoveFromParty(int actorId) {
		partyMembers.RemoveAll(a => a.baseData.id == actorId);
	}

	public ActorInstance GetPartyMember(int index) {
		if (index < 0 || index >= partyMembers.Count) return null;
		return partyMembers[index];
	}

	public string GetPartyMemberName(int index) {
		var actor = GetPartyMember(index);
		return actor != null ? actor.Name : $"[P{index}]";
	}

	public PartySaveData SaveParty() {
		var saveData = new PartySaveData();

		foreach (var actor in partyMembers) {
			var actorData = new ActorSaveData {
				actorId = actor.baseData.id,
				level = actor.level,
				currentHP = actor.currentHP,
				currentMP = actor.currentMP,
				exp = actor.exp,
				equippedItemIDs = new List<int>()
			};

			foreach (var kvp in actor.equipment) {
				actorData.equippedItemIDs.Add((int)(kvp.Value?.itemID)); // or .itemID if using ID field
			}

			saveData.members.Add(actorData);
		}

		return saveData;
	}

	public void LoadParty(PartySaveData saveData) {
		partyMembers.Clear();

		foreach (var saved in saveData.members) {
			var data = GameActors.Instance.GetActor(saved.actorId);
			if (data == null) continue;

			var instance = new ActorInstance(data) {
				level = saved.level,
				currentHP = saved.currentHP,
				currentMP = saved.currentMP,
				exp = saved.exp
			};

			// Equip saved items
			for (int i = 0; i < saved.equippedItemIDs.Count; i++) {
				var item = InventoryManager.Instance.GetItemByID(saved.equippedItemIDs[i]);
				var slot = (EquipmentSlot)i;
				instance.Equip(slot, item);
			}

			partyMembers.Add(instance);
		}
	}

}
