using System.Collections;
using UnityEngine;

public interface IEventCommand {
	IEnumerator Execute(EventExecutor executor);
	string GetSummary(); // For editor/debug
}

