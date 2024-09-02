using UnityEngine;

public class DestroyEffect : MonoBehaviour {

	public float time;
	
	void Start(){
		Destroy(gameObject, time);	
	}
}
