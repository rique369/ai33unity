using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour {

	public GameObject magnetAbsorbPos;

	private bool isLoop;
	private Collider b;


	void OnTriggerStay(Collider cc) {
		if(cc.gameObject.tag == "Coin" && Controller.iMagnet && !GameControll.pause) {
            b = cc;
            isLoop = true;	
			StartCoroutine (Com());

		}
	}
	IEnumerator Com(){
		while(isLoop){
			b.transform.position =  Vector3.Lerp(b.transform.position, magnetAbsorbPos.transform.position,Controller.speed*Time.smoothDeltaTime);
			b.transform.localScale =  Vector3.Lerp(b.transform.localScale, new Vector3(0.25f,0.25f,0.25f), Controller.speed/6*Time.smoothDeltaTime);
			if(Vector3.Distance(b.transform.position, magnetAbsorbPos.transform.position) < 4f){
               isLoop = false;	
			}
			yield return 0;
		}
	}
	
}
