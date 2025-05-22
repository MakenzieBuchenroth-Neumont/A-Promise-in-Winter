using System.Collections;
using UnityEngine;

public abstract class BaseEventCommand : ScriptableObject, IEventCommand {
	public abstract IEnumerator Execute(EventExecutor executor);
	public virtual string GetSummary() => name;
}
