using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shops : MonoBehaviour {

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

    private int curObjectNumber;
    private bool charSelect, knifSelect, capSelect, otherSelect;

    public Text clothName;
    public Text Price;
    public Text SelectButtonText;
    public GameObject buyBtn, selectBtn;

    public static bool store;
    private int charId, knifeId, capId;

    private AnimationManager animationManager;

    void Start() {
        //ProtectedPrefs.SetInt("Coins", 50000000);
        animationManager = this.GetComponent<AnimationManager>();
        store = false;
        curObjectNumber = 0;
        knifSelect = capSelect = otherSelect = false;
        if (!ProtectedPrefs.HasKey("Characters")) {
            ProtectedPrefs.SetInt("Characters", 0);
            ProtectedPrefs.SetInt("Cap", 0);
            ProtectedPrefs.SetInt("Character1", 1);
            ProtectedPrefs.SetInt("Cap1", 1);
            
        }
        
    }

    public void OpenStore() {
        store = true;
    }

    public void CloseStore()
    {
        store = false;
        ProtectedPrefs.Save();
    }

    public void lArrow() {
        curObjectNumber--;
    }
    public void rArrow()
    {
        curObjectNumber++;
    }

    public void BuyBtn() {
        if (charSelect) {
            if (charId == 1 && ProtectedPrefs.GetInt("Coins")>=170000) {
                ProtectedPrefs.SetInt("Character2", 1);
                ProtectedPrefs.SetInt("Coins", (ProtectedPrefs.GetInt("Coins") - 170000));
                animationManager.animationState = animationManager.Select;
            }
            else if (charId == 2 && ProtectedPrefs.GetInt("Coins") >= 1250000)
            {
                ProtectedPrefs.SetInt("Character3", 1);
                ProtectedPrefs.SetInt("Coins", (ProtectedPrefs.GetInt("Coins") - 1250000));
                animationManager.animationState = animationManager.Select;
            }
            else if (charId == 3 && ProtectedPrefs.GetInt("Coins") >= 600000)
            {
                ProtectedPrefs.SetInt("Character4", 1);
                ProtectedPrefs.SetInt("Coins", (ProtectedPrefs.GetInt("Coins") - 600000));
                animationManager.animationState = animationManager.Select;
            }
        }
        if (capSelect)
        {
            if (capId == 1 && ProtectedPrefs.GetInt("Coins") >= 70000)
            {
                ProtectedPrefs.SetInt("Cap2", 1);
                ProtectedPrefs.SetInt("Coins", (ProtectedPrefs.GetInt("Coins") - 70000));
                animationManager.animationState = animationManager.Select;
            }
            else if (capId == 2 && ProtectedPrefs.GetInt("Coins") >= 105000)
            {
                ProtectedPrefs.SetInt("Cap3", 1);
                ProtectedPrefs.SetInt("Coins", (ProtectedPrefs.GetInt("Coins") - 105000));
                animationManager.animationState = animationManager.Select;
            }
            else if (capId == 3 && ProtectedPrefs.GetInt("Coins") >= 140000)
            {
                ProtectedPrefs.SetInt("Cap4", 1);
                ProtectedPrefs.SetInt("Coins", (ProtectedPrefs.GetInt("Coins") - 140000));
                animationManager.animationState = animationManager.Select;
            }
        }
        if (knifSelect)
        {
            if (knifeId == 0 && ProtectedPrefs.GetInt("Coins") >= 15000)
            {
                ProtectedPrefs.SetInt("Knife1", 1);
                ProtectedPrefs.SetInt("Coins", (ProtectedPrefs.GetInt("Coins") - 15000));
                ProtectedPrefs.SetInt("mLamp", ProtectedPrefs.GetInt("mLamp") + 3);
                animationManager.animationState = animationManager.Select;
            }
            else if (knifeId == 1 && ProtectedPrefs.GetInt("Coins") >= 30000)
            {
                ProtectedPrefs.SetInt("Knife2", 1);
                ProtectedPrefs.SetInt("Coins", (ProtectedPrefs.GetInt("Coins") - 30000));
                ProtectedPrefs.SetInt("mLamp", ProtectedPrefs.GetInt("mLamp") + 4);
                animationManager.animationState = animationManager.Select;
            }
            else if (knifeId == 2 && ProtectedPrefs.GetInt("Coins") >= 45000)
            {
                ProtectedPrefs.SetInt("Knife3", 1);
                ProtectedPrefs.SetInt("Coins", (ProtectedPrefs.GetInt("Coins") - 45000));
                ProtectedPrefs.SetInt("mLamp", ProtectedPrefs.GetInt("mLamp") + 5);
                animationManager.animationState = animationManager.Select;
            }
            else if (knifeId == 3 && ProtectedPrefs.GetInt("Coins") >= 60000)
            {
                ProtectedPrefs.SetInt("Knife4", 1);
                ProtectedPrefs.SetInt("Coins", (ProtectedPrefs.GetInt("Coins") - 60000));
                ProtectedPrefs.SetInt("mLamp", ProtectedPrefs.GetInt("mLamp") + 6);
                animationManager.animationState = animationManager.Select;
            }
            else if (knifeId == 4 && ProtectedPrefs.GetInt("Coins") >= 75000)
            {
                ProtectedPrefs.SetInt("Knife5", 1);
                ProtectedPrefs.SetInt("Coins", (ProtectedPrefs.GetInt("Coins") - 75000));
                ProtectedPrefs.SetInt("mLamp", ProtectedPrefs.GetInt("mLamp") + 7);
                animationManager.animationState = animationManager.Select;
            }
        }
        if (otherSelect)
        {
            if (curObjectNumber == 0 && ProtectedPrefs.GetInt("Coins") >= 200000)
            {
                ProtectedPrefs.SetInt("Clock1", 1);
                ProtectedPrefs.SetInt("Coins", (ProtectedPrefs.GetInt("Coins") - 200000));
                ProtectedPrefs.SetInt("ClockStart", 1);
                animationManager.animationState = animationManager.Select;
            }
            else if (curObjectNumber == 1 && ProtectedPrefs.GetInt("Coins") >= 1500000)
            {
                ProtectedPrefs.SetInt("Ak471", 1);
                ProtectedPrefs.SetInt("Coins", (ProtectedPrefs.GetInt("Coins") - 1500000));
                ProtectedPrefs.SetInt("AkStart", 1);
                animationManager.animationState = animationManager.Select;
            }
            else if (curObjectNumber == 2 && ProtectedPrefs.GetInt("Coins") >= 350000)
            {
                ProtectedPrefs.SetInt("Oculus1", 1);
                ProtectedPrefs.SetInt("Coins", (ProtectedPrefs.GetInt("Coins") - 350000));
                ProtectedPrefs.SetInt("SpectatesStart", 1);
                animationManager.animationState = animationManager.Select;
            }
        }
    }

    public void charButton() {
        charSelect = true;
        curObjectNumber = 0;
        capSelect = knifSelect = otherSelect = false;
    }
    public void knifButton()
    {
        knifSelect = true;
        curObjectNumber = 0;
        capSelect = charSelect = otherSelect = false;
    }
    public void otherButton()
    {
        otherSelect = true;
        curObjectNumber = 0;
        capSelect = knifSelect = charSelect = false;
    }
    public void capButton()
    {
        capSelect = true;
        curObjectNumber = 0;
        charSelect = knifSelect = otherSelect = false;
    }

    public void SelectBtn() {
        if (charSelect)
        {
            if (charId == 0)
            {
                ProtectedPrefs.SetInt("Player", 1);
                animationManager.animationState = animationManager.Select;
                selectBtn.GetComponent<Button>().IsActive();
            }
            else if (charId == 1)
            {
                ProtectedPrefs.SetInt("Player", 2);
                animationManager.animationState = animationManager.Select;
                selectBtn.GetComponent<Button>().IsActive();
            }
            else if (charId == 2)
            {
                ProtectedPrefs.SetInt("Player", 3);
                animationManager.animationState = animationManager.Select;
                selectBtn.GetComponent<Button>().IsActive();
            }
            else if (charId == 3)
            {
                ProtectedPrefs.SetInt("Player", 4);
                animationManager.animationState = animationManager.Select;
                selectBtn.GetComponent<Button>().IsActive();
            }
        }

        if (capSelect)
        {
            if (capId == 0)
            {
                ProtectedPrefs.SetInt("Helmet", 1);
                animationManager.animationState = animationManager.Select;
                selectBtn.GetComponent<Button>().IsActive();
            }
            else if (capId == 1)
            {
                ProtectedPrefs.SetInt("Helmet", 2);
                animationManager.animationState = animationManager.Select;
                selectBtn.GetComponent<Button>().IsActive();
            }
            else if (capId == 2)
            {
                ProtectedPrefs.SetInt("Helmet", 3);
                animationManager.animationState = animationManager.Select;
                selectBtn.GetComponent<Button>().IsActive();
            }
            else if (capId == 3)
            {
                ProtectedPrefs.SetInt("Helmet", 4);
                animationManager.animationState = animationManager.Select;
                selectBtn.GetComponent<Button>().IsActive();
            }
        }

        if (knifSelect)
        {
            if (knifeId == 0)
            {
                ProtectedPrefs.SetInt("Whinger", 1);
                animationManager.animationState = animationManager.Select;
                selectBtn.GetComponent<Button>().IsActive();
            }
            else if (knifeId == 1)
            {
                ProtectedPrefs.SetInt("Whinger", 2);
                animationManager.animationState = animationManager.Select;
                selectBtn.GetComponent<Button>().IsActive();
            }
            else if (knifeId == 2)
            {
                ProtectedPrefs.SetInt("Whinger", 3);
                animationManager.animationState = animationManager.Select;
                selectBtn.GetComponent<Button>().IsActive();
            }
            else if (knifeId == 3)
            {
                ProtectedPrefs.SetInt("Whinger", 4);
                animationManager.animationState = animationManager.Select;
                selectBtn.GetComponent<Button>().IsActive();
            }
            else if (knifeId == 4)
            {
                ProtectedPrefs.SetInt("Whinger", 5);
                animationManager.animationState = animationManager.Select;
                selectBtn.GetComponent<Button>().IsActive();
            }
        }
        if (otherSelect)
        {
            if (curObjectNumber == 0)
            {
                if (ProtectedPrefs.GetInt("ClockStart") != 1)
                {
                    ProtectedPrefs.SetInt("ClockStart", 1);
                }
                else {
                    ProtectedPrefs.SetInt("ClockStart", 0);
                }
                
                animationManager.animationState = animationManager.Select;
                selectBtn.GetComponent<Button>().IsActive();
            }
            else if (curObjectNumber == 1)
            {
                if (ProtectedPrefs.GetInt("AkStart") != 1)
                {
                    ProtectedPrefs.SetInt("AkStart", 1);
                }
                else {
                    ProtectedPrefs.SetInt("AkStart", 0);
                }
                animationManager.animationState = animationManager.Select;
                selectBtn.GetComponent<Button>().IsActive();
            }
            else if (curObjectNumber == 2)
            {
                if (ProtectedPrefs.GetInt("SpectatesStart") != 1)
                {
                    ProtectedPrefs.SetInt("SpectatesStart", 1);
                }
                else {
                    ProtectedPrefs.SetInt("SpectatesStart", 0);
                }
                animationManager.animationState = animationManager.Select;
                selectBtn.GetComponent<Button>().IsActive();
            }
        }
    }

    void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            CloseStore();
        }

        if (store) {

            if (curObjectNumber < 0) { curObjectNumber = 0; }


            if (ProtectedPrefs.GetInt("Characters") == 2 && ProtectedPrefs.GetInt("Cap") == 0) {
                caps[0].SetActive(false);
                capChar3.SetActive(true);
            }
            else {
                capChar3.SetActive(false);
            }




            if (!charSelect)
            {

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

            }
            else {

                if (curObjectNumber > characters.Length - 1) { curObjectNumber = characters.Length - 1; }
                for (int c = 0; c < characters.Length; c++)
                {
                    if (c == curObjectNumber)
                    {
                        characters[c].SetActive(true);
                    }
                    else {
                        characters[c].SetActive(false);
                    }
                }
                if (curObjectNumber == 0)
                {
                    caps[ProtectedPrefs.GetInt("Cap")].SetActive(true);
                    bear.SetActive(false);
                    jacket[0].SetActive(false);
                    jacket[1].SetActive(false);
                    belt[0].SetActive(false);
                    belt[1].SetActive(false);
                    belt[2].SetActive(false);
                    MaskAnonim.SetActive(false);
                    clothName.text = "Rifat";
                    Price.text = "";
                    charId = 0;
                    if (ProtectedPrefs.GetInt("Character1") == 1) { selectBtn.SetActive(true); buyBtn.SetActive(false); }
                    else { selectBtn.SetActive(false); buyBtn.SetActive(true); }
                }
                else if (curObjectNumber == 1)
                {
                    caps[ProtectedPrefs.GetInt("Cap")].SetActive(true);
                    bear.SetActive(true);
                    jacket[0].SetActive(false);
                    jacket[1].SetActive(true);
                    belt[0].SetActive(true);
                    belt[1].SetActive(false);
                    belt[2].SetActive(false);
                    MaskAnonim.SetActive(false);
                    clothName.text = "Alim";
                    Price.text = "170 000";
                    charId = 1;

                    if (ProtectedPrefs.GetInt("Character2") == 1) { selectBtn.SetActive(true); buyBtn.SetActive(false); }
                    else { selectBtn.SetActive(false); buyBtn.SetActive(true); }
                }
                else if (curObjectNumber == 2)
                {
                    caps[ProtectedPrefs.GetInt("Cap")].SetActive(true);
                    bear.SetActive(false);
                    jacket[0].SetActive(false);
                    jacket[1].SetActive(false);
                    belt[0].SetActive(false);
                    belt[1].SetActive(true);
                    belt[2].SetActive(false);
                    MaskAnonim.SetActive(false);
                    clothName.text = "Umar";
                    Price.text = "1 250 000";
                    charId = 2;

                    if (ProtectedPrefs.GetInt("Character3") == 1) { selectBtn.SetActive(true); buyBtn.SetActive(false); }
                    else { selectBtn.SetActive(false); buyBtn.SetActive(true); }
                }
                else if (curObjectNumber == 3)
                {
                    caps[ProtectedPrefs.GetInt("Cap")].SetActive(true);
                    bear.SetActive(false);
                    ocular.SetActive(false);
                    jacket[0].SetActive(true);
                    jacket[1].SetActive(false);
                    belt[0].SetActive(false);
                    belt[1].SetActive(false);
                    belt[2].SetActive(true);
                    MaskAnonim.SetActive(true);
                    clothName.text = "Shamil'";
                    Price.text = "600 000";
                    charId = 3;

                    if (ProtectedPrefs.GetInt("Character4") == 1) { selectBtn.SetActive(true); buyBtn.SetActive(false); }
                    else { selectBtn.SetActive(false); buyBtn.SetActive(true); }
                }

                if (curObjectNumber == 2 && ProtectedPrefs.GetInt("Cap") == 0)
                {
                    caps[0].SetActive(false);
                    capChar3.SetActive(true);
                }
                else {
                    capChar3.SetActive(false);
                }

            }

            if (!capSelect)
            {
                if (ProtectedPrefs.HasKey("Cap"))
                {
                    for (int i = 0; i < caps.Length; i++)
                    {
                        if (ProtectedPrefs.GetInt("Cap") == i)
                        {
                            if (i == 0 && curObjectNumber == 2 && charSelect)
                            {
                                capChar3.SetActive(true);
                                caps[i].SetActive(false);
                            }
                            else {
                                capChar3.SetActive(false);
                                caps[i].SetActive(true);
                            }
                        }
                        else {
                            caps[i].SetActive(false);
                        }
                    }
                }
            }
            else {

                if (curObjectNumber > caps.Length - 1) { curObjectNumber = caps.Length - 1; }
                for (int cp = 0; cp < caps.Length; cp++)
                {
                    if (cp == curObjectNumber)
                    {
                        Price.text = (curObjectNumber+1)*35 + " 000";
                        if (cp == 0 && ProtectedPrefs.GetInt("Characters") == 2)
                        {
                            capChar3.SetActive(true);
                            caps[cp].SetActive(false);
                        }
                        else {
                            capChar3.SetActive(false);
                            caps[cp].SetActive(true);
                        }
                    }
                    else {
                        caps[cp].SetActive(false);
                    }
    
                }
                clothName.text = "Helmet " + (curObjectNumber + 1);
                capId = curObjectNumber;

                switch (curObjectNumber) {
                    case 0:
                        if (ProtectedPrefs.GetInt("Cap1") == 1) { selectBtn.SetActive(true); buyBtn.SetActive(false); }
                        else { selectBtn.SetActive(false); buyBtn.SetActive(true); }
                        break;
                    case 1:
                        if (ProtectedPrefs.GetInt("Cap2") == 1) { selectBtn.SetActive(true); buyBtn.SetActive(false); }
                        else { selectBtn.SetActive(false); buyBtn.SetActive(true); }
                        break;
                    case 2:
                        if (ProtectedPrefs.GetInt("Cap3") == 1) { selectBtn.SetActive(true); buyBtn.SetActive(false); }
                        else { selectBtn.SetActive(false); buyBtn.SetActive(true); }
                        break;
                    case 3:
                        if (ProtectedPrefs.GetInt("Cap4") == 1) { selectBtn.SetActive(true); buyBtn.SetActive(false); }
                        else { selectBtn.SetActive(false); buyBtn.SetActive(true); }
                        break;
                }
            }

            if (!knifSelect)
            {
                if (ProtectedPrefs.HasKey("Knife"))
                {
                    for (int i = 0; i < knifes.Length; i++)
                    {
                        if (ProtectedPrefs.GetInt("Knife") == i)
                        {
                            knifes[ProtectedPrefs.GetInt("Knife")].SetActive(true);
                        }
                        else {
                            knifes[i].SetActive(false);
                        }
                    }

                }
                else {
                    for (int i = 0; i < knifes.Length; i++)
                    {
                        knifes[i].SetActive(false);
                    }
                }
            }
            else {
                if (curObjectNumber > knifes.Length - 1) { curObjectNumber = knifes.Length - 1; }

                if (ProtectedPrefs.GetInt("Cap") == 0 && ProtectedPrefs.GetInt("Characters") == 2)
                {
                    capChar3.SetActive(true);
                    caps[0].SetActive(false);
                }
                else {
                    capChar3.SetActive(false);
                    caps[ProtectedPrefs.GetInt("Cap")].SetActive(true);
                }

                for (int cp = 0; cp < knifes.Length; cp++)
                {
                    Price.text = (curObjectNumber + 1) * 15 + " 000";
                    if (cp == curObjectNumber)
                    {
                        knifes[cp].SetActive(true);
                    }
                    else {
                        knifes[cp].SetActive(false);
                    }
                    clothName.text = "Whinger " + (curObjectNumber+1);
                    knifeId = curObjectNumber;
                }

                switch (curObjectNumber)
                {
                    case 0:
                        if (ProtectedPrefs.GetInt("Knife1") == 1) { selectBtn.SetActive(true); buyBtn.SetActive(false); }
                        else { selectBtn.SetActive(false); buyBtn.SetActive(true); }
                        break;
                    case 1:
                        if (ProtectedPrefs.GetInt("Knife2") == 1) { selectBtn.SetActive(true); buyBtn.SetActive(false); }
                        else { selectBtn.SetActive(false); buyBtn.SetActive(true); }
                        break;
                    case 2:
                        if (ProtectedPrefs.GetInt("Knife3") == 1) { selectBtn.SetActive(true); buyBtn.SetActive(false); }
                        else { selectBtn.SetActive(false); buyBtn.SetActive(true); }
                        break;
                    case 3:
                        if (ProtectedPrefs.GetInt("Knife4") == 1) { selectBtn.SetActive(true); buyBtn.SetActive(false); }
                        else { selectBtn.SetActive(false); buyBtn.SetActive(true); }
                        break;
                    case 4:
                        if (ProtectedPrefs.GetInt("Knife5") == 1) { selectBtn.SetActive(true); buyBtn.SetActive(false); }
                        else { selectBtn.SetActive(false); buyBtn.SetActive(true); }
                        break;
                }
            }

            if (!otherSelect)
            {
                if (ProtectedPrefs.HasKey("Clock"))
                {
                    clock.SetActive(true);
                }
                else {
                    clock.SetActive(false);
                }
                if (ProtectedPrefs.HasKey("Ak47"))
                {
                    ak47.SetActive(true);
                }
                else {
                    ak47.SetActive(false);
                }
                if (ProtectedPrefs.HasKey("Oculus"))
                {
                    ocular.SetActive(true);
                }
                else {
                    ocular.SetActive(false);
                }
                SelectButtonText.text = "Select";
            }
            else {

                if (ProtectedPrefs.GetInt("Player") == 4)
                {
                    if (curObjectNumber > 1) { curObjectNumber = 1; }
                }
                else {
                    if (curObjectNumber > 2) { curObjectNumber = 2; }
                }
                    
                if (curObjectNumber == 0)
                {
                    Price.text = "200 000";
                    if (ProtectedPrefs.GetInt("Clock1") == 1) {
                        selectBtn.SetActive(true); buyBtn.SetActive(false);
                        if (ProtectedPrefs.GetInt("ClockStart") == 1)
                        {
                            SelectButtonText.text = "Drop";
                            clock.SetActive(true);
                        }
                        else {
                            SelectButtonText.text = "Select";
                            clock.SetActive(false);
                        }
                    }
                    else { selectBtn.SetActive(false); buyBtn.SetActive(true); clock.SetActive(true); }
                }
                else if (curObjectNumber == 1)
                {
                    Price.text = "1 500 000";
                    if (ProtectedPrefs.GetInt("Ak471") == 1) {
                        selectBtn.SetActive(true); buyBtn.SetActive(false);
                        if (ProtectedPrefs.GetInt("AkStart") == 1)
                        {
                            SelectButtonText.text = "Drop";
                            ak47.SetActive(true);
                        }
                        else {
                            SelectButtonText.text = "Select";
                            ak47.SetActive(false);
                        }
                    }
                    else { selectBtn.SetActive(false); buyBtn.SetActive(true); ak47.SetActive(true); }
                }
                else if (curObjectNumber == 2) {
                    Price.text = "350 000";
                    if (ProtectedPrefs.GetInt("Oculus1") == 1) {
                        selectBtn.SetActive(true); buyBtn.SetActive(false);
                        if (ProtectedPrefs.GetInt("SpectatesStart") == 1)
                        {
                            SelectButtonText.text = "Drop";
                            ocular.SetActive(true);
                        }
                        else {
                            SelectButtonText.text = "Select";
                            ocular.SetActive(false);
                        }
                    }
                    else { selectBtn.SetActive(false); buyBtn.SetActive(true); ocular.SetActive(true); }
                }

                if (ProtectedPrefs.GetInt("Cap") == 0 && ProtectedPrefs.GetInt("Characters") == 2)
                {
                    capChar3.SetActive(true);
                    caps[0].SetActive(false);
                }
                else {
                    capChar3.SetActive(false);
                    caps[ProtectedPrefs.GetInt("Cap")].SetActive(true);
                }

                if (0 == curObjectNumber)
                {
                    ak47.SetActive(false);
                    ocular.SetActive(false);
                    clothName.text = "Gold clock";
                    
                }
                else if (1 == curObjectNumber)
                {
                    clock.SetActive(false);
                    ocular.SetActive(false);
                    clothName.text = "Premium gold AK-74";
                   
                }
                else if (2 == curObjectNumber)
                {
                    clock.SetActive(false);
                    ak47.SetActive(false);
                    clothName.text = "Gold sunglasses";
                   
                }

                
               
                
            }


            //******************************************************//
        }

        //if (ProtectedPrefs.GetInt("Character4") == 1) { ocular.SetActive(false); }
    }

}
