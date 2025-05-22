using System.Collections.Generic;

[System.Serializable]
public class ActorSaveData {
	public int actorId;
	public int level;
	public int currentHP;
	public int currentMP;
	public int exp;
	public List<int> equippedItemIDs = new();
}
