using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public float hoverScale = 1.1f;
	public float transitionSpeed = 8f;

	private Vector3 originalScale;
	private Vector3 targetScale;

	private void Awake() {
		originalScale = transform.localScale;
		targetScale = originalScale;
	}

	private void Update() {
		transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * transitionSpeed);
	}

	public void OnPointerEnter(PointerEventData eventData) {
		targetScale = originalScale * hoverScale;
	}

	public void OnPointerExit(PointerEventData eventData) {
		targetScale = originalScale;
	}
}
