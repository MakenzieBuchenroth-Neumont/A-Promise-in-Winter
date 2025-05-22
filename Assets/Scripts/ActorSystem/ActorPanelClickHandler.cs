using UnityEngine;
using UnityEngine.EventSystems;

public class ActorPanelClickHandler : MonoBehaviour, IPointerClickHandler {
	private PartyMenuManager menuManager;
	private ActorInstance actor;

	public void Init(PartyMenuManager manager, ActorInstance actorData) {
		menuManager = manager;
		actor = actorData;
	}

	public void OnPointerClick(PointerEventData eventData) {
		menuManager.FocusActor(actor, eventData.position);
	}
}
