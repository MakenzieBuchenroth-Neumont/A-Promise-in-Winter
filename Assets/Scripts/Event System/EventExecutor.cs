using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class EventExecutor : Singleton<EventExecutor> {
	public EventBlock eventBlock;
	public EventContext CurrentEventContext;

	public bool ShouldBreakLoop { get; set; } = false;
	public bool ShouldExitEvent { get; set; } = false;

	private Dictionary<string, int> labelLookup = new();
	private int? jumpIndex = null;

	public void StartEvent() {
		StartCoroutine(RunEvent());
	}

	private IEnumerator RunEvent() {
		CacheLabels(eventBlock.commands);
		foreach (var command in eventBlock.commands) {
			yield return command.Execute(this);

			if (ShouldExitEvent) {
				yield break;
			}

			if (jumpIndex.HasValue) {
				int i = jumpIndex.Value - 1;
				jumpIndex = null;
			}
		}
	}

	public IEnumerator RunBlock(EventBlock block) {
		foreach (var cmd in block.commands) {
			yield return cmd.Execute(this);
		}
	}

	public void CacheLabels(List<BaseEventCommand> commands) {
		labelLookup.Clear();
		for (int i = 0; i < commands.Count; i++) {
			if (commands[i] is LabelCommand label) {
				if (!labelLookup.ContainsKey(label.labelName))
					labelLookup[label.labelName] = i;
			}
		}
	}

	public bool TryGetLabelIndex(string labelName, out int index) {
		return labelLookup.TryGetValue(labelName, out index);
	}

	public void SetJumpIndex(int index) {
		jumpIndex = index;
	}
}
