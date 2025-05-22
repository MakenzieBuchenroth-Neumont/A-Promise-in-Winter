using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MessageWindow : Singleton<MessageWindow> {

	[Header("UI References")]
	public GameObject windowRoot;
	public Image faceImage;
	public TMP_Text nameText;
	public TMP_Text messageText;
	public Image backgroundImage;  // still needed to display it
	public Sprite normalBackground;
	public Sprite dimBackground;
	public Sprite transparentBackground;


	[Header("Position Anchors")]
	public RectTransform topAnchor;
	public RectTransform middleAnchor;
	public RectTransform bottomAnchor;

	private static readonly Dictionary<int, string> colorTable = new() {
	{ 0, "#FFFFFF" }, // White
    { 1, "#FF0000" }, // Red
    { 2, "#00FF00" }, // Green
    { 3, "#0000FF" }, // Blue
    { 4, "#FFFF00" }, // Yellow
    { 5, "#00FFFF" }, // Cyan
    { 6, "#FF00FF" }, // Magenta
    { 7, "#888888" }  // Gray
};

	public bool IsOpen { get; private set; }

	protected override void Awake() {
		base.Awake();
		CloseImmediate();
	}

	public void Show(string message, Sprite face, string actorName, MessageBackgroundType bgType, MessageWindowPosition pos) {
		windowRoot.SetActive(true);
		IsOpen = true;

		// Set content
		messageText.text = ParseControlCharacters(message);
		faceImage.sprite = face;
		faceImage.gameObject.SetActive(face != null);
		nameText.text = actorName;
		nameText.gameObject.SetActive(!string.IsNullOrEmpty(actorName));

		// Set background
		switch (bgType) {
			case MessageBackgroundType.Normal:
				backgroundImage.sprite = normalBackground;
				backgroundImage.color = Color.white;
				break;
			case MessageBackgroundType.Dim:
				backgroundImage.sprite = dimBackground;
				backgroundImage.color = Color.white;
				break;
			case MessageBackgroundType.Transparent:
				backgroundImage.sprite = transparentBackground;
				backgroundImage.color = new Color(1, 1, 1, 0); // fully transparent
				break;
		}


		// Set position
		RectTransform rect = windowRoot.GetComponent<RectTransform>();
		switch (pos) {
			case MessageWindowPosition.Top: rect.position = topAnchor.position; break;
			case MessageWindowPosition.Middle: rect.position = middleAnchor.position; break;
			case MessageWindowPosition.Bottom: rect.position = bottomAnchor.position; break;
		}

		// Begin waiting for input
		StartCoroutine(WaitForInput());
	}

	private IEnumerator WaitForInput() {
		while (true) {
			if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
				break;
			yield return null;
		}

		CloseImmediate();
	}

	private string ParseControlCharacters(string rawText) {
		string parsed = rawText;

		// \V[n] → variable value
		parsed = System.Text.RegularExpressions.Regex.Replace(parsed, @"\\V\[(\d+)\]", match =>
		{
			int id = int.Parse(match.Groups[1].Value);
			return GameVariables.Instance.Get(id).ToString();
		});

		// \N[n] → named actor
		parsed = System.Text.RegularExpressions.Regex.Replace(parsed, @"\\N\[(\d+)\]", match =>
		{
			int id = int.Parse(match.Groups[1].Value);
			return GameActors.Instance.GetName(id);
		});

		// \P[n] → party member name at index n
		parsed = System.Text.RegularExpressions.Regex.Replace(parsed, @"\\P\[(\d+)\]", match =>
		{
			int index = int.Parse(match.Groups[1].Value);
			return PartyManager.Instance.GetPartyMemberName(index);

		});

		// \A[n] → actor name by ID (same as \N[n])
		parsed = System.Text.RegularExpressions.Regex.Replace(parsed, @"\\A\[(\d+)\]", match =>
		{
			int id = int.Parse(match.Groups[1].Value);
			return GameActors.Instance.GetName(id);
		});

		// \G → gold icon or label
		parsed = parsed.Replace("\\G", "<sprite name=\"gold\">");

		// \C[n] or \C[reset]
		parsed = System.Text.RegularExpressions.Regex.Replace(parsed, @"\\C\[(\d+|reset)\]", match =>
		{
			string arg = match.Groups[1].Value;

			if (arg.ToLower() == "reset") {
				return "</color>"; // close TMP color tag
			}

			if (int.TryParse(arg, out int index)) {
				string hex = colorTable.TryGetValue(index, out var h) ? h : "#FFFFFF";
				return $"<color={hex}>";
			}

			return ""; // fallback: strip malformed tag
		});

		// \n → actual line break
		parsed = parsed.Replace("\\n", "\n");

		// Count open color tags
		int openColors = System.Text.RegularExpressions.Regex.Matches(parsed, "<color=").Count;
		int closeColors = System.Text.RegularExpressions.Regex.Matches(parsed, "</color>").Count;
		int unclosedColors = openColors - closeColors;

		// Auto-close any unclosed <color=...> tags
		for (int i = 0; i < unclosedColors; i++) {
			parsed += "</color>";
		}

		return parsed;
	}

	public void CloseImmediate() {
		IsOpen = false;
		windowRoot.SetActive(false);
	}
}
