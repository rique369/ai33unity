using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public GameObject mainPanel;
    public GameObject quitPanel;
    public AudioSource bAudio;
    public GameObject uMuteBtn, muteBtn, fbWindow, fbNotLog, leaderBoard, storeWindow, cameraSecond, iapPanel;
    public Animator animator;
    bool fb;
    public Text coins;
    private int magLevel;
    private int botLevel;
    private int koverLevel;
    private int dcLevel;

    public Image magnetP;
    public Image ShouseP;
    public Image KoverP;
    public Image dcP;

    public Text tMagnit;
    public Text tShouses;
    public Text tKover;
    public Text tdoublecoin;
    public Text tnoAds;

    void Start() {
        fb = false;
        if (ProtectedPrefs.HasKey("Mute")) {
            if (ProtectedPrefs.GetInt("Mute") == 0)
            {
                bAudio.mute = true;
                muteBtn.SetActive(true);
                uMuteBtn.SetActive(false);
            }
            else {
                bAudio.mute = false;
                uMuteBtn.SetActive(true);
                muteBtn.SetActive(false);
            }
        }
    }

    public void Play() {
        SceneManager.LoadScene("main");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Mute() {
        ProtectedPrefs.SetInt("Mute", 0);
        bAudio.mute = true;
    }

    public void uMute()
    {
        ProtectedPrefs.SetInt("Mute", 1);
        bAudio.mute = false;
    }

    public void FbButton() {
        fb = !fb;
        if (!fb)
        {
            fbWindow.SetActive(false);
        }
        if (!Facebook.Unity.FBManager.login)
        {
            fbNotLog.SetActive(true);
        }
    }

    public void Shop() {
        coins.text = ProtectedPrefs.GetInt("Coins").ToString();
    }

    public void noADS() {
        if (ProtectedPrefs.GetInt("Coins") >= 999000 && ProtectedPrefs.GetInt("bunner") == 0)
        {
            ProtectedPrefs.SetInt("bunner", 1);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 999000);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            mainPanel.SetActive(false);
            quitPanel.SetActive(true);
            fbWindow.SetActive(false);
            fbNotLog.SetActive(false);
            leaderBoard.SetActive(false);
            storeWindow.SetActive(false);
            cameraSecond.SetActive(false);
            iapPanel.SetActive(false);
        }
        if (!Facebook.Unity.FBManager.login)
        {
            fbWindow.SetActive(false);
        }
        else {
            fbNotLog.SetActive(false);
        }

        if (ProtectedPrefs.GetInt("bunner") == 0)
        {
            tnoAds.text = "999000";
        }
        else {
            tnoAds.text = "Full!";
        }
        if (ProtectedPrefs.HasKey("Coins"))
        {
            coins.text = ProtectedPrefs.GetInt("Coins").ToString();
        }

        if (ProtectedPrefs.HasKey("m_LvL"))
        {
            magLevel = ProtectedPrefs.GetInt("m_LvL");
        }
        else {
            magLevel = 0;
            magnetP.fillAmount = 0f;
            tMagnit.text = "750";
        }
        if (magLevel == 1)
        {
            magnetP.fillAmount = 0.2f;
            tMagnit.text = "2 500";
        }
        else if (magLevel == 2)
        {
            magnetP.fillAmount = 0.4f;
            tMagnit.text = "5 000";
        }
        else if (magLevel == 3)
        {
            magnetP.fillAmount = 0.6f;
            tMagnit.text = "7 500";
        }
        else if (magLevel == 4)
        {
            magnetP.fillAmount = 0.8f;
            tMagnit.text = "10 000";
        }
        else if (magLevel == 5)
        {
            magnetP.fillAmount = 1f;
            tMagnit.text = "Full!";
        }

        if (ProtectedPrefs.HasKey("b_LvL"))
        {
            botLevel = ProtectedPrefs.GetInt("b_LvL");
        }
        else {
            botLevel = 0;
            ShouseP.fillAmount = 0f;
            tShouses.text = "750";
        }
        if (botLevel == 1)
        {
            ShouseP.fillAmount = 0.2f;
            tShouses.text = "2 500";
        }
        else if (botLevel == 2)
        {
            ShouseP.fillAmount = 0.4f;
            tShouses.text = "5 000";
        }
        else if (botLevel == 3)
        {
            ShouseP.fillAmount = 0.6f;
            tShouses.text = "7 500";
        }
        else if (botLevel == 4)
        {
            ShouseP.fillAmount = 0.8f;
            tShouses.text = "10 000";
        }
        else if (botLevel == 5)
        {
            ShouseP.fillAmount = 1f;
            tShouses.text = "Full!";
        }

        if (ProtectedPrefs.HasKey("k_LvL"))
        {
            koverLevel = ProtectedPrefs.GetInt("k_LvL");
        }
        else {
            koverLevel = 0;
            KoverP.fillAmount = 0f;
            tKover.text = "750";
        }
        if (koverLevel == 1)
        {
            KoverP.fillAmount = 0.2f;
            tKover.text = "2 500";
        }
        else if (koverLevel == 2)
        {
            KoverP.fillAmount = 0.4f;
            tKover.text = "5 000";
        }
        else if (koverLevel == 3)
        {
            KoverP.fillAmount = 0.6f;
            tKover.text = "7 500";
        }
        else if (koverLevel == 4)
        {
            KoverP.fillAmount = 0.8f;
            tKover.text = "10 000";
        }
        else if (koverLevel == 5)
        {
            KoverP.fillAmount = 1f;
            tKover.text = "Full!";
        }

        if (ProtectedPrefs.HasKey("d_LvL"))
        {
            dcLevel = ProtectedPrefs.GetInt("d_LvL");
        }
        else {
            dcLevel = 0;
            dcP.fillAmount = 0f;
            tdoublecoin.text = "750";
        }
        if (dcLevel == 1)
        {
            dcP.fillAmount = 0.2f;
            tdoublecoin.text = "2 500";
        }
        else if (dcLevel == 2)
        {
            dcP.fillAmount = 0.4f;
            tdoublecoin.text = "5 000";
        }
        else if (dcLevel == 3)
        {
            dcP.fillAmount = 0.6f;
            tdoublecoin.text = "7 500";
        }
        else if (dcLevel == 4)
        {
            dcP.fillAmount = 0.8f;
            tdoublecoin.text = "10 000";
        }
        else if (dcLevel == 5)
        {
            dcP.fillAmount = 1f;
            tdoublecoin.text = "Full!";
        }

    }

    public void MagnetUp()
    {
        if (ProtectedPrefs.GetInt("Coins") >= 750 && magLevel == 0)
        {
            ProtectedPrefs.SetInt("m_LvL", 1);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 750);
        }
        else if (ProtectedPrefs.GetInt("Coins") >= 2500 && magLevel == 1)
        {
            ProtectedPrefs.SetInt("m_LvL", 2);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 2500);
        }
        else if (ProtectedPrefs.GetInt("Coins") >= 5000 && magLevel == 2)
        {
            ProtectedPrefs.SetInt("m_LvL", 3);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 5000);
        }
        else if (ProtectedPrefs.GetInt("Coins") >= 7500 && magLevel == 3)
        {
            ProtectedPrefs.SetInt("m_LvL", 4);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 7500);
        }
        else if (ProtectedPrefs.GetInt("Coins") >= 10000 && magLevel == 4)
        {
            ProtectedPrefs.SetInt("m_LvL", 5);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 10000);
        }
    }

    public void ShousUp()
    {
        if (ProtectedPrefs.GetInt("Coins") >= 750 && botLevel == 0)
        {
            ProtectedPrefs.SetInt("b_LvL", 1);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 750);
        }
        else if (ProtectedPrefs.GetInt("Coins") >= 2500 && botLevel == 1)
        {
            ProtectedPrefs.SetInt("b_LvL", 2);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 2500);
        }
        else if (ProtectedPrefs.GetInt("Coins") >= 5000 && botLevel == 2)
        {
            ProtectedPrefs.SetInt("b_LvL", 3);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 5000);
        }
        else if (ProtectedPrefs.GetInt("Coins") >= 7500 && botLevel == 3)
        {
            ProtectedPrefs.SetInt("b_LvL", 4);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 7500);
        }
        else if (ProtectedPrefs.GetInt("Coins") >= 10000 && botLevel == 4)
        {
            ProtectedPrefs.SetInt("b_LvL", 5);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 10000);
        }
    }

    public void KoverUp()
    {
        if (ProtectedPrefs.GetInt("Coins") >= 750 && koverLevel == 0)
        {
            ProtectedPrefs.SetInt("k_LvL", 1);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 750);
        }
        else if (ProtectedPrefs.GetInt("Coins") >= 2500 && koverLevel == 1)
        {
            ProtectedPrefs.SetInt("k_LvL", 2);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 2500);
        }
        else if (ProtectedPrefs.GetInt("Coins") >= 5000 && koverLevel == 2)
        {
            ProtectedPrefs.SetInt("k_LvL", 3);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 5000);
        }
        else if (ProtectedPrefs.GetInt("Coins") >= 7500 && koverLevel == 3)
        {
            ProtectedPrefs.SetInt("k_LvL", 4);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 7500);
        }
        else if (ProtectedPrefs.GetInt("Coins") >= 10000 && koverLevel == 4)
        {
            ProtectedPrefs.SetInt("k_LvL", 5);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 10000);
        }
    }

    public void DoubleUp()
    {
        if (ProtectedPrefs.GetInt("Coins") >= 750 && dcLevel == 0)
        {
            ProtectedPrefs.SetInt("d_LvL", 1);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 750);
        }
        else if (ProtectedPrefs.GetInt("Coins") >= 2500 && dcLevel == 1)
        {
            ProtectedPrefs.SetInt("d_LvL", 2);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 2500);
        }
        else if (ProtectedPrefs.GetInt("Coins") >= 5000 && dcLevel == 2)
        {
            ProtectedPrefs.SetInt("d_LvL", 3);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 5000);
        }
        else if (ProtectedPrefs.GetInt("Coins") >= 7500 && dcLevel == 3)
        {
            ProtectedPrefs.SetInt("d_LvL", 4);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 7500);
        }
        else if (ProtectedPrefs.GetInt("Coins") >= 10000 && dcLevel == 4)
        {
            ProtectedPrefs.SetInt("d_LvL", 5);
            ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") - 10000);
        }
    }
}
