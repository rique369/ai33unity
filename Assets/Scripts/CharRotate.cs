using UnityEngine;
using System.Collections;

public class CharRotate : MonoBehaviour {

	public Transform Player;
	private Touch initialTouch = new Touch();
	private float distance, deltaX;
	private bool hasSwiped = false;
	
	void Update () {
        if (Shops.store)
        {
            if (deltaX > 0F && hasSwiped)
            {
                Player.transform.Rotate(new Vector3(0, 3, 0), Space.Self);
            }
            else if (deltaX < 0F && hasSwiped)
            {
                Player.transform.Rotate(new Vector3(0, -3, 0), Space.Self);
            }

            foreach (Touch th in Input.touches)
            {
                if (th.phase == TouchPhase.Began)
                {
                    initialTouch = th;
                }
                else if (th.phase == TouchPhase.Moved && !hasSwiped)
                {
                    deltaX = initialTouch.position.x - th.position.x;
                    distance = Mathf.Sqrt((deltaX * deltaX));

                    if (distance != 0F)
                    {

                        hasSwiped = true;
                    }
                }
                else if (th.phase == TouchPhase.Ended)
                {
                    initialTouch = new Touch();
                    distance = 0;
                    hasSwiped = false;
                }
            }
        }
        else {
            Player.transform.eulerAngles = new Vector3(0, 180, 0);
        }
	}
}
