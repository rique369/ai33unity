using UnityEngine;
using System.Collections;

public class characterCustomization : MonoBehaviour {

    public GameObject[] characters;
    public GameObject[] caps;
    public GameObject capChar3;
    public GameObject[] knifes;
    public GameObject[] jacket;
    public GameObject bear;
    public GameObject[] belt;
    public GameObject ocular;
    public GameObject ak47;
    public GameObject clock;
    public GameObject MaskAnonim;

    void Start() {
        Customize();
    }

    public void Customize() {
        if (ProtectedPrefs.GetInt("Player") == 1)
        {
            characters[0].SetActive(true);
            characters[1].SetActive(false);
            characters[2].SetActive(false);
            characters[3].SetActive(false);
            bear.SetActive(false);
            jacket[0].SetActive(false);
            jacket[1].SetActive(false);
            belt[0].SetActive(false);
            belt[1].SetActive(false);
            belt[2].SetActive(false);
            MaskAnonim.SetActive(false);
        }
        else if (ProtectedPrefs.GetInt("Player") == 2)
        {
            characters[0].SetActive(false);
            characters[1].SetActive(true);
            characters[2].SetActive(false);
            characters[3].SetActive(false);
            bear.SetActive(true);
            jacket[0].SetActive(false);
            jacket[1].SetActive(true);
            belt[0].SetActive(true);
            belt[1].SetActive(false);
            belt[2].SetActive(false);
            MaskAnonim.SetActive(false);
        }
        else if (ProtectedPrefs.GetInt("Player") == 3)
        {
            characters[0].SetActive(false);
            characters[1].SetActive(false);
            characters[2].SetActive(true);
            characters[3].SetActive(false);
            bear.SetActive(false);
            jacket[0].SetActive(false);
            jacket[1].SetActive(false);
            belt[0].SetActive(false);
            belt[1].SetActive(true);
            belt[2].SetActive(false);
            MaskAnonim.SetActive(false);
        }
        else if (ProtectedPrefs.GetInt("Player") == 4)
        {
            characters[0].SetActive(false);
            characters[1].SetActive(false);
            characters[2].SetActive(false);
            characters[3].SetActive(true);
            bear.SetActive(false);
            jacket[0].SetActive(true);
            jacket[1].SetActive(false);
            belt[0].SetActive(false);
            belt[1].SetActive(false);
            belt[2].SetActive(true);
            MaskAnonim.SetActive(true);
            ocular.SetActive(false);
        }
        else
        {
            characters[0].SetActive(true);
            characters[1].SetActive(false);
            characters[2].SetActive(false);
            characters[3].SetActive(false);
            bear.SetActive(false);
            jacket[0].SetActive(false);
            jacket[1].SetActive(false);
            belt[0].SetActive(false);
            belt[1].SetActive(false);
            belt[2].SetActive(false);
            MaskAnonim.SetActive(false);
        }

        if (ProtectedPrefs.GetInt("Helmet") == 1)
        {
            if (ProtectedPrefs.GetInt("Player") != 3)
            {
                caps[0].SetActive(true);
                caps[1].SetActive(false);
                caps[2].SetActive(false);
                caps[3].SetActive(false);
                capChar3.SetActive(false);
            }
            else {
                caps[0].SetActive(false);
                caps[1].SetActive(false);
                caps[2].SetActive(false);
                caps[3].SetActive(false);
                capChar3.SetActive(true);
            }
        }
        else if (ProtectedPrefs.GetInt("Helmet") == 2)
        {
            caps[0].SetActive(false);
            caps[1].SetActive(true);
            caps[2].SetActive(false);
            caps[3].SetActive(false);
            capChar3.SetActive(false);
        }
        else if (ProtectedPrefs.GetInt("Helmet") == 3)
        {
            caps[0].SetActive(false);
            caps[1].SetActive(false);
            caps[2].SetActive(true);
            caps[3].SetActive(false);
            capChar3.SetActive(false);
        }
        else if (ProtectedPrefs.GetInt("Helmet") == 4)
        {
            caps[0].SetActive(false);
            caps[1].SetActive(false);
            caps[2].SetActive(false);
            caps[3].SetActive(true);
            capChar3.SetActive(false);
        }
        else
        {
            if (ProtectedPrefs.GetInt("Player") != 3)
            {
                caps[0].SetActive(true);
                caps[1].SetActive(false);
                caps[2].SetActive(false);
                caps[3].SetActive(false);
                capChar3.SetActive(false);
            }
            else {
                caps[0].SetActive(false);
                caps[1].SetActive(false);
                caps[2].SetActive(false);
                caps[3].SetActive(false);
                capChar3.SetActive(true);
            }

        }

        if (ProtectedPrefs.GetInt("Whinger") == 1)
        {
            knifes[0].SetActive(true);
            knifes[1].SetActive(false);
            knifes[2].SetActive(false);
            knifes[3].SetActive(false);
            knifes[4].SetActive(false);
        }
        else if (ProtectedPrefs.GetInt("Whinger") == 2)
        {
            knifes[0].SetActive(false);
            knifes[1].SetActive(true);
            knifes[2].SetActive(false);
            knifes[3].SetActive(false);
            knifes[4].SetActive(false);
        }
        else if (ProtectedPrefs.GetInt("Whinger") == 3)
        {
            knifes[0].SetActive(false);
            knifes[1].SetActive(false);
            knifes[2].SetActive(true);
            knifes[3].SetActive(false);
            knifes[4].SetActive(false);
        }
        else if (ProtectedPrefs.GetInt("Whinger") == 4)
        {
            knifes[0].SetActive(false);
            knifes[1].SetActive(false);
            knifes[2].SetActive(false);
            knifes[3].SetActive(true);
            knifes[4].SetActive(false);
        }
        else if (ProtectedPrefs.GetInt("Whinger") == 5)
        {
            knifes[0].SetActive(false);
            knifes[1].SetActive(false);
            knifes[2].SetActive(false);
            knifes[3].SetActive(false);
            knifes[4].SetActive(true);
        }
        else
        {
            knifes[0].SetActive(false);
            knifes[1].SetActive(false);
            knifes[2].SetActive(false);
            knifes[3].SetActive(false);
            knifes[4].SetActive(false);
        }

        if (ProtectedPrefs.GetInt("ClockStart") == 1)
        {
            clock.SetActive(true);
        }
        else
        {
            clock.SetActive(false);
        }

        if (ProtectedPrefs.GetInt("AkStart") == 1)
        {
            ak47.SetActive(true);
        }
        else
        {
            ak47.SetActive(false);
        }

        if (ProtectedPrefs.GetInt("SpectatesStart") == 1 && ProtectedPrefs.GetInt("Player") != 4)
        {
            ocular.SetActive(true);
        }
        else
        {
            ocular.SetActive(false);
        }

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            Customize();
        }
    }
}
