using UnityEngine;

public class CoinRotation : MonoBehaviour {
	
	
	void OnTriggerEnter(Collider col){
		if(col.tag == "Coin"){
			col.GetComponent<Animation>().Play ();
		}
	}
	
}
