using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event System/Event Block")]
public class EventBlock : ScriptableObject {
	public List<BaseEventCommand> commands;
}
