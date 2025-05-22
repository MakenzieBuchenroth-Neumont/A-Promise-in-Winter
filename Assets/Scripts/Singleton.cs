using Unity.VisualScripting;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour, ISingleton where T : MonoBehaviour {
	private static T instance;
	public static T Instance {
		get {
			if (instance == null)
				instance = FindObjectOfType<T>();
			return instance;
		}
	}

	protected virtual void Awake() {
		if (instance == null) instance = this as T;
		else if (instance != this) Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}
}
