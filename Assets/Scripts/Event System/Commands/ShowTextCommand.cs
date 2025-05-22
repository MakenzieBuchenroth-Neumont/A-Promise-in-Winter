using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Event System/Commands/Show Text")]
public class ShowTextCommand : BaseEventCommand {
	[TextArea]
	public string message;
	public Sprite faceGraphic;
	public string actorName;
	public MessageBackgroundType backgroundType;
	public MessageWindowPosition windowPosition;

	public override IEnumerator Execute(EventExecutor executor) {
		// Display the message window with specified settings
		MessageWindow.Instance.Show(message, faceGraphic, actorName, backgroundType, windowPosition);

		// Wait until the player closes the message window
		while (MessageWindow.Instance.IsOpen) {
			yield return null;
		}
	}

	public override string GetSummary() {
		return $"Show Text: {message}";
	}
}

public enum MessageBackgroundType {
	Normal,
	Dim,
	Transparent
}

public enum MessageWindowPosition {
	Top,
	Middle,
	Bottom
}
