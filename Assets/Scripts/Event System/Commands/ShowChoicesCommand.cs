using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event System/Commands/Show Choices")]
public class ShowChoicesCommand : BaseEventCommand {
	[SerializeField] private List<string> choices = new();
	[SerializeField] private List<EventBlock> branches = new();

	public override IEnumerator Execute(EventExecutor executor) {
		int selectedIndex = -1;
		bool waiting = true;

		ChoiceWindow.Instance.Show(choices, (index) => {
			selectedIndex = index;
			waiting = false;
		});

		while (waiting) yield return null;

		// Run the chosen branch if available
		if (selectedIndex >= 0 && selectedIndex < branches.Count && branches[selectedIndex] != null) {
			yield return executor.RunBlock(branches[selectedIndex]);
		}
	}

	public override string GetSummary() {
		return $"Show Choices: {string.Join(" / ", choices)}";
	}
}
