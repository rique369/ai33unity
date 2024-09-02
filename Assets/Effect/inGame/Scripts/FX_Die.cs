using UnityEngine;
using System.Collections;

//Rotates object at constant velocity

public class FX_Die : MonoBehaviour
{
	// Rotation speed vector
	public Vector3 rotation;
	
	void Update()
	{
		transform.Rotate(rotation * Time.deltaTime);
	}
}
