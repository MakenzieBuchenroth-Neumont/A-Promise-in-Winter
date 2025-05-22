using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Event System/Commands/Select Item")]
public class SelectItemCommand : BaseEventCommand {
	[Tooltip("Variable ID to store the selected item's ID in")]
	public int variableId = 1;

	[Tooltip("Type of item to filter (Item, Weapon, Armor, or All)")]
	public ItemType filterType = ItemType.All;

	[Tooltip("Show only items currently owned by the player")]
	public bool onlyOwned = true;

	public override IEnumerator Execute(EventExecutor executor) {
		bool waiting = true;
		ItemData selected = null;

		ItemSelectorWindow.Instance.Show(filterType, onlyOwned, (result) => {
			selected = result;
			waiting = false;
		});

		while (waiting) yield return null;

		GameVariables.Instance.Set(variableId, selected != null ? selected.itemID : -1);
	}

	public override string GetSummary() {
		string ownedText = onlyOwned ? "Only Owned" : "All";
		return $"Select Item ({filterType}, {ownedText}) → Variable {variableId}";
	}
}
