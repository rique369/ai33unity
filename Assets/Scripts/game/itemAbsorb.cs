using UnityEngine;
using UnityEngine.UI;

public class itemAbsorb : MonoBehaviour {

	public static bool doubleCoin = false;
	public static float cointimer = 0f;

	public Transform prefCoinDie;
	public Transform preftapDie;
	public Transform prefdoubleDie;
	public Transform prefmagnetDie;

    public GameObject pwEntry;
    public GameObject pwList;
    public Texture2D magnetIco, koverIco, botIco, starIco;
    public Texture2D mLoad, kvLoad, bLoad, sLoad;
    private Image mIco, kIco, bIco, sIco, mIco2, kIco2, bIco2, sIco2;
    private GameObject mPanel, kPanel, bPanel, sPanel;
    public static bool getLamp = false;

    public GameObject itemEffectObject;
    public Material itemEffectMaterial;
    public Texture2D bookEffectIco, pipeEffectIco, emeraldEffectIco, kalyanEffectIco, lampEffectIco, urnEffectIco, vazeEffectIco, goldEffectIco, rubinEffectIco;

    void Start(){
		doubleCoin = false;
        cointimer = 0f;
        if (!ProtectedPrefs.HasKey("mBook")) ProtectedPrefs.SetInt("mBook", 0);
        if (!ProtectedPrefs.HasKey("mPipe")) ProtectedPrefs.SetInt("mPipe", 0);
        if (!ProtectedPrefs.HasKey("mEmerald")) ProtectedPrefs.SetInt("mEmerald", 0);
        if (!ProtectedPrefs.HasKey("mGold")) ProtectedPrefs.SetInt("mGold", 0);
        if (!ProtectedPrefs.HasKey("mKalyan")) ProtectedPrefs.SetInt("mKalyan", 0);
        if (!ProtectedPrefs.HasKey("mLamp")) ProtectedPrefs.SetInt("mLamp", 0);
        if (!ProtectedPrefs.HasKey("mRubin")) ProtectedPrefs.SetInt("mRubin", 0);
        if (!ProtectedPrefs.HasKey("mUrna")) ProtectedPrefs.SetInt("mUrna", 0);
        if (!ProtectedPrefs.HasKey("mVaza")) ProtectedPrefs.SetInt("mVaza", 0);
    }

	void OnTriggerEnter(Collider items) {
		if(items.gameObject.tag == "Coin") {
			Instantiate (prefCoinDie, new Vector3 (transform.position.x + 1.5f, transform.position.y + 13F, transform.position.z + 5F), transform.rotation);
			if(!doubleCoin)
                GameControll.coin++;
			else
                GameControll.coin = GameControll.coin + 2;
			AudiosManager.instance.PlayingSound("Coin");
			Destroy(items.gameObject);
		}
		if (items.gameObject.tag == "Magnet") {
			Instantiate (prefmagnetDie, new Vector3 (transform.position.x, transform.position.y + 2F, transform.position.z + 15F), transform.rotation);
			if (Controller.iMagnet) {
				Controller.cM = Controller.cM - Controller.magnetTime;
			} else {
				Controller.iMagnet = true;
                mg();
            }
			AudiosManager.instance.PlayingSound("Power");
            Destroy(items.gameObject);
        }
		else if (items.gameObject.tag == "Kover") {
			if (Controller.iFly) {
				Controller.cF = Controller.cF - Controller.flyTime;
			} else {
				Controller.iFly = true;
                kv();
            }
            AudiosManager.instance.PlayingSound("Power");
            Destroy(items.gameObject);
        }
		else if (items.gameObject.tag == "Bots") {
			Instantiate (preftapDie, new Vector3 (transform.position.x, transform.position.y + 2F, transform.position.z + 15F), transform.rotation);
			if (Controller.doubleJump) {
				Controller.cJ = Controller.cJ - Controller.doubleJumpTime;
			} else {
				Controller.doubleJump = true;
                bt();
            }
            AudiosManager.instance.PlayingSound("Power");
            Destroy(items.gameObject);
        }
		else if (items.gameObject.tag == "Star") {
			Instantiate (prefdoubleDie, new Vector3 (transform.position.x, transform.position.y + 2F, transform.position.z + 15F), transform.rotation);
			if (doubleCoin) {
				cointimer = cointimer - Controller.doubleCoinTime;
			} else {
				doubleCoin = true;
                dc();
            }
            AudiosManager.instance.PlayingSound("Power");
            Destroy(items.gameObject);
        }

        else if(items.gameObject.tag == "Book")
        {
            itemEffectMaterial.mainTexture = bookEffectIco;
            GameObject effect = Instantiate(itemEffectObject, new Vector3(transform.position.x, transform.position.y + 7.5F, transform.position.z -7F), transform.rotation) as GameObject;
            effect.transform.SetParent(this.transform);

            ProtectedPrefs.SetInt("mBook", ProtectedPrefs.GetInt("mBook") + 1);
            AudiosManager.instance.PlayingSound("Power");
            Destroy(items.gameObject);
        }
        else if (items.gameObject.tag == "Pipe")
        {
            itemEffectMaterial.mainTexture = pipeEffectIco;
            GameObject effect = Instantiate(itemEffectObject, new Vector3(transform.position.x, transform.position.y + 7.5F, transform.position.z - 7F), transform.rotation) as GameObject;
            effect.transform.SetParent(this.transform);

            ProtectedPrefs.SetInt("mPipe", ProtectedPrefs.GetInt("mPipe") + 1);
            AudiosManager.instance.PlayingSound("Power");
            Destroy(items.gameObject);
        }
        else if (items.gameObject.tag == "Emerald")
        {
            itemEffectMaterial.mainTexture = emeraldEffectIco;
            GameObject effect = Instantiate(itemEffectObject, new Vector3(transform.position.x, transform.position.y + 7.5F, transform.position.z - 7F), transform.rotation) as GameObject;
            effect.transform.SetParent(this.transform);

            ProtectedPrefs.SetInt("mEmerald", ProtectedPrefs.GetInt("mEmerald") + 1);
            AudiosManager.instance.PlayingSound("Power");
            Destroy(items.gameObject);
        }
        else if (items.gameObject.tag == "Gold")
        {
            itemEffectMaterial.mainTexture = goldEffectIco;
            GameObject effect = Instantiate(itemEffectObject, new Vector3(transform.position.x, transform.position.y + 7.5F, transform.position.z - 7F), transform.rotation) as GameObject;
            effect.transform.SetParent(this.transform);

            ProtectedPrefs.SetInt("mGold", ProtectedPrefs.GetInt("mGold") + 1);
            AudiosManager.instance.PlayingSound("Power");
            Destroy(items.gameObject);
        }
        else if (items.gameObject.tag == "Kalyan")
        {
            itemEffectMaterial.mainTexture = kalyanEffectIco;
            GameObject effect = Instantiate(itemEffectObject, new Vector3(transform.position.x, transform.position.y + 7.5F, transform.position.z - 7F), transform.rotation) as GameObject;
            effect.transform.SetParent(this.transform);

            ProtectedPrefs.SetInt("mKalyan", ProtectedPrefs.GetInt("mKalyan") + 1);
            AudiosManager.instance.PlayingSound("Power");
            Destroy(items.gameObject);
        }
        else if (items.gameObject.tag == "Lamp")
        {
            itemEffectMaterial.mainTexture = lampEffectIco;
            GameObject effect = Instantiate(itemEffectObject, new Vector3(transform.position.x, transform.position.y + 7.5F, transform.position.z - 7F), transform.rotation) as GameObject;
            effect.transform.SetParent(this.transform);

            ProtectedPrefs.SetInt("mLamp", ProtectedPrefs.GetInt("mLamp") + 1);
            AudiosManager.instance.PlayingSound("Power");
            Destroy(items.gameObject);
            getLamp = true;

        }
        else if (items.gameObject.tag == "Rubin")
        {
            itemEffectMaterial.mainTexture = rubinEffectIco;
            GameObject effect = Instantiate(itemEffectObject, new Vector3(transform.position.x, transform.position.y + 7.5F, transform.position.z - 7F), transform.rotation) as GameObject;
            effect.transform.SetParent(this.transform);

            ProtectedPrefs.SetInt("mRubin", ProtectedPrefs.GetInt("mRubin") + 1);
            AudiosManager.instance.PlayingSound("Power");
            Destroy(items.gameObject);
        }
        else if (items.gameObject.tag == "Urna")
        {
            itemEffectMaterial.mainTexture = urnEffectIco;
            GameObject effect = Instantiate(itemEffectObject, new Vector3(transform.position.x, transform.position.y + 7.5F, transform.position.z - 7F), transform.rotation) as GameObject;
            effect.transform.SetParent(this.transform);

            ProtectedPrefs.SetInt("mUrna", ProtectedPrefs.GetInt("mUrna") + 1);
            AudiosManager.instance.PlayingSound("Power");
            Destroy(items.gameObject);
        }
        else if (items.gameObject.tag == "Vaza")
        {
            itemEffectMaterial.mainTexture = vazeEffectIco;
            GameObject effect = Instantiate(itemEffectObject, new Vector3(transform.position.x, transform.position.y + 7.5F, transform.position.z - 7F), transform.rotation) as GameObject;
            effect.transform.SetParent(this.transform);

            ProtectedPrefs.SetInt("mVaza", ProtectedPrefs.GetInt("mVaza") + 1);
            AudiosManager.instance.PlayingSound("Power");
            Destroy(items.gameObject);
        }
    }

	void Update(){
		if (doubleCoin) {
			cointimer += Time.deltaTime;
			if(cointimer>=Controller.doubleCoinTime){
				doubleCoin = false;
				cointimer = 0f;
			}
		}

        if (!GameControll.pause)
        {
            if (Controller.doubleJump)
            {
                bIco.fillAmount = 1 - (Controller.cJ / (Controller.doubleJumpTime-0.3f));
            }
            else {
                if (bPanel != null) Destroy(bPanel.gameObject);
            }


            if (Controller.iFly)
            {
                kIco.fillAmount = 1 - (Controller.cF / (Controller.flyTime + 2.7f));
            }
            else {
                if (kPanel != null) Destroy(kPanel.gameObject);
            }


            if (Controller.iMagnet)
            {
                mIco.fillAmount = 1 - (Controller.cM / (Controller.magnetTime - 0.3f));
            }
            else {
                if(mPanel != null) Destroy(mPanel.gameObject);
            }


            if (itemAbsorb.doubleCoin)
            {
                sIco.fillAmount = 1- (itemAbsorb.cointimer / (Controller.doubleCoinTime-0.3f));
            }
            else {
                if (sPanel != null) Destroy(sPanel.gameObject);
            }

        }
    }

    private void mg()
    {
        mPanel = Instantiate(pwEntry) as GameObject;
        mPanel.transform.SetParent(pwList.transform);

        Transform ThisIcon2 = mPanel.transform.Find("Icon2");
        mIco2 = ThisIcon2.GetComponent<Image>();
        mIco2.sprite = Sprite.Create(magnetIco, new Rect(0, 0, magnetIco.width, magnetIco.height), new Vector2(0, 0), 128f);

        Transform ThisIcon = mPanel.transform.Find("Icon");
        mIco = ThisIcon.GetComponent<Image>();
        mIco.sprite = Sprite.Create(mLoad, new Rect(0, 0, mLoad.width, mLoad.height), new Vector2(0, 0), 128f);
    }

    private void bt()
    {
        bPanel = Instantiate(pwEntry) as GameObject;
        bPanel.transform.SetParent(pwList.transform);

        Transform ThisIcon2 = bPanel.transform.Find("Icon2");
        bIco2 = ThisIcon2.GetComponent<Image>();
        bIco2.sprite = Sprite.Create(botIco, new Rect(0, 0, botIco.width, botIco.height), new Vector2(0, 0));

        Transform ThisIcon = bPanel.transform.Find("Icon");
        bIco = ThisIcon.GetComponent<Image>();
        bIco.sprite = Sprite.Create(bLoad, new Rect(0, 0, bLoad.width, bLoad.height), new Vector2(0, 0));
    }

    private void kv()
    {
        kPanel = Instantiate(pwEntry) as GameObject;
        kPanel.transform.SetParent(pwList.transform);

        Transform ThisIcon2 = kPanel.transform.Find("Icon2");
        kIco2 = ThisIcon2.GetComponent<Image>();
        kIco2.sprite = Sprite.Create(koverIco, new Rect(0, 0, koverIco.width, koverIco.height), new Vector2(0, 0));

        Transform ThisIcon = kPanel.transform.Find("Icon");
        kIco = ThisIcon.GetComponent<Image>();
        kIco.sprite = Sprite.Create(kvLoad, new Rect(0, 0, kvLoad.width, kvLoad.height), new Vector2(0, 0));
    }

    private void dc()
    {
        sPanel = Instantiate(pwEntry) as GameObject;
        sPanel.transform.SetParent(pwList.transform);

        Transform ThisIcon2 = sPanel.transform.Find("Icon2");
        sIco2 = ThisIcon2.GetComponent<Image>();
        sIco2.sprite = Sprite.Create(starIco, new Rect(0, 0, starIco.width, starIco.height), new Vector2(0, 0));

        Transform ThisIcon = sPanel.transform.Find("Icon");
        sIco = ThisIcon.GetComponent<Image>();
        sIco.sprite = Sprite.Create(sLoad, new Rect(0, 0, sLoad.width, sLoad.height), new Vector2(0, 0));
    }
}
