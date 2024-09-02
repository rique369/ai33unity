using UnityEngine;
using System.Collections;

public class ignoreColl : MonoBehaviour {


    void OnTriggerStay(Collider cc)
    {
        if (cc.gameObject.tag == "Blocks" && GameControll.SaveMe && !GameControll.pause)
        {
            cc.gameObject.SetActive(false);
        }
    }
   
}
