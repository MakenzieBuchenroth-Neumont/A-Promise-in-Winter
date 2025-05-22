using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Event System/Commands/Show Scrolling Text")]
public class ShowScrollingTextCommand : BaseEventCommand {
	[TextArea(5, 10)]
	public string text;

	[Tooltip("Wait for the scrolling to finish before continuing the event.")]
	public bool waitForCompletion = true;

	public override IEnumerator Execute(EventExecutor executor) {
		bool done = false;

		ScrollingTextWindow.Instance.Show(text, () => {
			done = true;
		});

		if (waitForCompletion)
			yield return new WaitUntil(() => done);
	}

	public override string GetSummary() {
		return waitForCompletion ? $"Scroll Text (Wait)" : "Scroll Text (No Wait)";
	}
}
