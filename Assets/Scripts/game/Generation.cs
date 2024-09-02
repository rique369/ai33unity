using UnityEngine;
using System.Collections.Generic;

public class Generation : MonoBehaviour {

	public Transform[] buildPrefs;
	public Transform[] obstraclePrefs;
	public Transform floorPref;

	LinkedList<Transform> buildings = new LinkedList<Transform>();
	LinkedList<Transform> obstracles = new LinkedList<Transform>();

	Vector3 newPosition, newPositionOb;
	Vector3 startPosition = new Vector3(0,0, -99000);

	float buildLength = 37f;
	float obstracleLength = 49.65f;

	[HideInInspector]
	public Transform tPlayer;

	void Start(){
		tPlayer = GameObject.FindGameObjectWithTag ("Player").transform;
		tPlayer.transform.position = startPosition;
		for (int i = 0; i < 7; i++) {
			newPosition.z = i * buildLength + startPosition.z;
			Transform b = Instantiate (buildPrefs [Random.Range (0, buildPrefs.Length)], newPosition, Quaternion.identity) as Transform;
			buildings.AddLast(b);
		}

		for (int i = 0; i < 4; i++) {
			newPositionOb.z = i * obstracleLength + startPosition.z;
			Transform o;
			if (i < 2) {
				o = Instantiate (floorPref, newPositionOb, Quaternion.identity) as Transform;
			} else {
				o = Instantiate (obstraclePrefs [Random.Range (0, obstraclePrefs.Length)], newPositionOb, Quaternion.identity) as Transform;
			}
			obstracles.AddLast(o);
		}
	}

	void Update(){
		Transform fb = buildings.First.Value;
		Transform lb = buildings.Last.Value;
		if (Vector3.Distance (tPlayer.transform.position, fb.transform.position) > buildLength*2) {
			buildings.Remove (fb);
			Destroy (fb.gameObject);
			Transform newBuild = Instantiate (buildPrefs[Random.Range (0,buildPrefs.Length)], new Vector3 (0, 0, lb.localPosition.z + buildLength), Quaternion.identity) as Transform;
			buildings.AddLast (newBuild);
		}

		Transform fo = obstracles.First.Value;
		Transform lo = obstracles.Last.Value;
		if (Vector3.Distance (tPlayer.transform.position, fo.transform.position) > obstracleLength) {
			obstracles.Remove (fo);
			Destroy (fo.gameObject);
			Transform newObstracle = Instantiate (obstraclePrefs[Random.Range (0,obstraclePrefs.Length)], new Vector3 (0, 0, lo.localPosition.z + obstracleLength), Quaternion.identity) as Transform;
			obstracles.AddLast (newObstracle);
		}
	}

    void LateUpdate() {
        if (60 <= Application.targetFrameRate+5)
            Application.targetFrameRate = 60;
    }

}
