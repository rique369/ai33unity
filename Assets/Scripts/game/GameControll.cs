using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Facebook.Unity;
using Gley.MobileAds;
using Gley.MobileAds.Internal;

public class GameControll : MonoBehaviour {

    public static int coin;
	public static bool pause;
    public Text coine, distance;
    public Text goCoin, goScore, goBScore, lamlCount;
    public GameObject MainMenuPanel, GameOverPanel, powerPanel, SaveLifePanel;
    private AudioListener al;
    private int lcoin, lCount;
    private float timerl = 0;
    private bool lifesave = false;
    public GameObject saveButton;
    private Image sl;
    public static bool SaveMe = false, loadS;
    public static bool showAd = false;
    private bool changeScorecolor;
    private float cc = 0;

    void Start()
    {
        coin = 0;
        changeScorecolor = false;
        distance.color = new Color(1, 1, 1, 1);
        pause = false;
        showAd = false;
        al = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>();
        al.enabled = true;
        if (ProtectedPrefs.HasKey("Coins"))
        {
            lcoin = ProtectedPrefs.GetInt("Coins");
        }
        else {
            lcoin = 0;
        }
        GetLamp();
        sl = saveButton.GetComponent<Image>();
        AdController.HideBanner();
        
    }
    public static int total_deaths = 2;

    void Update()
    {
        if (lifesave == true && Controller.iDie && !loadS && !SaveMe)
        {
            showAd = false;
            SaveLifePanel.SetActive(true);
            timerl += Time.deltaTime;
            sl.fillAmount -= Time.deltaTime / 1.8F;
            if (timerl >= 1.8F)
            {
                timerl = 0;
                sl.fillAmount = 1;
                SaveLifePanel.SetActive(false);
                Invoke("GameOver", 0.5f);
                loadS = true;
            }
        }
        
        if (itemAbsorb.getLamp) GetLamp();
        coine.text = coin.ToString();
        distance.text = Controller.Distance.ToString("00000000") + "0";
        if (Controller.Distance > 10 && Controller.Distance > ProtectedPrefs.GetFloat("HighScore") && !changeScorecolor) {
            cc += Time.deltaTime;
            if (cc < 1)
            {
                distance.color = new Color(1, 0, 0, 1);
            }
            else if (cc > 1 && cc < 1.5f)
            {
                distance.color = new Color(1, 1, 1, 1);
            }
            else if (cc > 1.5f && cc < 2)
            {
                distance.color = new Color(1, 0, 0, 1);
            }
            else if (cc > 2 && cc < 2.5f)
            {
                distance.color = new Color(1, 1, 1, 1);
            }
            else if(cc>2.5f){
                distance.color = new Color(1f, 0.92f, 0.016f, 1f);
                cc = 0;
                changeScorecolor = true;
            }
            
        }
        if (Controller.iDie && ProtectedPrefs.GetInt("mLamp") < 1)
        {
            MainMenuPanel.SetActive(false);
            Invoke("GameOver", 0.5f);
        }
        else if (Controller.iDie && ProtectedPrefs.GetInt("mLamp") > 0 && !lifesave)
        {
            MainMenuPanel.SetActive(false);
            lifesave = true;
            loadS = false;
        }
    }

    public void SaveLife()
    {
        SaveLifePanel.SetActive(false);
        ProtectedPrefs.SetInt("mLamp", lCount - 1);
        Controller.iDie = false;
        SaveMe = true;
        lifesave = false;
        GetLamp();
        MainMenuPanel.SetActive(true);
        timerl = 0;
        sl.fillAmount = 1;
    }

    public void GetLamp()
    {
        if (ProtectedPrefs.HasKey("mLamp"))
        {
            lCount = ProtectedPrefs.GetInt("mLamp");
        }
        else {
            lCount = 0;
        }
        itemAbsorb.getLamp = false;
        lamlCount.text = lCount.ToString();

    }
    private bool game_over;
    private void GameOver()
    {
        if (game_over) return;
        game_over = true;
        showAd    = true;
        GameOverPanel.SetActive(true);
        powerPanel.SetActive(false);
        ProtectedPrefs.SetInt("Coins", lcoin + coin);
        goCoin.text = "Coins: " + coin.ToString();
        goScore.text = "Score: " + Controller.Distance.ToString("f0") + "0";
        if (Controller.Distance > ProtectedPrefs.GetFloat("HighScore"))
        {
            ProtectedPrefs.SetFloat("HighScore", Controller.Distance);
        }
        if (ProtectedPrefs.HasKey("HighScore"))
        {
            goBScore.text = "Best Score: " + ProtectedPrefs.GetFloat("HighScore").ToString("f0") + "0";
        }
        else
        {
            goBScore.text = "Best Score: " + Controller.Distance.ToString("f0") + "0";
        }

        AdController.ShowBanner();
        total_deaths--;
        if (total_deaths <= 0 && API.IsInterstitialAvailable())
        {
            total_deaths = 2;
            AdController.ShowInterstitilar();
        }
    }

    public void isPause() {
        pause = true;
        al.enabled = false;
    }

    public void isContinue()
    {
        pause = false;
        al.enabled = true;
    }

    public void isRestart()
    {
        SceneManager.LoadScene("main");
    }

    public void isMainMenu()
    {
        pause = false;
        SceneManager.LoadScene("menu");
    }
}
