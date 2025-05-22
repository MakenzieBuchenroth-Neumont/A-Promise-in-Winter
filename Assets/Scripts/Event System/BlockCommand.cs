using UnityEngine;
using System.Collections.Generic;

public abstract class BlockCommand : BaseEventCommand {
	[Tooltip("Child commands executed as part of this block.")]
	public List<BaseEventCommand> childCommands = new();
}
