using UnityEngine;
using System.Collections;

public class CameraPos : MonoBehaviour {
	[HideInInspector]
	public GameObject target;
    public Vector3 startPos = new Vector3(0, 2.6f, -5f);
	public float angle = 20f;

	private Vector3 posCamera;
	private Vector3 angleCam;
    private int FPSLimit = 60;
    private Camera cam;

    void Start(){
        QualitySettings.vSyncCount = 0;
        target = GameObject.FindGameObjectWithTag ("Player");
        cam = this.GetComponent<Camera>();
        cam.nearClipPlane = 3;
    }

    void Update() {
       if (FPSLimit != Application.targetFrameRate)
            Application.targetFrameRate = FPSLimit;
    }

	void LateUpdate(){
        if(Controller.kd != 2 && Controller.iDie) cam.nearClipPlane = 1;
        if (GameControll.pause) return;
		posCamera.x = Mathf.Lerp(posCamera.x, target.transform.position.x, 5 * Time.deltaTime);
		posCamera.y = Mathf.Lerp(posCamera.y, target.transform.position.y + startPos.y, 5 * Time.deltaTime);
		posCamera.z = Mathf.Lerp(posCamera.z, target.transform.position.z + startPos.z, 10f); 
		this.transform.position = posCamera;
		angleCam.x = angle;
		angleCam.y = Mathf.Lerp(angleCam.y, 0, 1 * Time.deltaTime);
		angleCam.z = transform.eulerAngles.z;
		this.transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, angleCam, 1 * Time.deltaTime);
    }
}
