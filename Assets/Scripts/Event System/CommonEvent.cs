using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Event System/Common Event")]
public class CommonEvent : ScriptableObject {
	[Tooltip("A reusable block of event commands.")]
	public List<BaseEventCommand> commands = new();
}
