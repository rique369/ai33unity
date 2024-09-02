using UnityEngine;

public class ObRandom : MonoBehaviour {

    public GameObject[] coinLine;
    public GameObject[] powerUp;
    public GameObject airCoin;
    [HideInInspector]
    public Transform tPlayer;

    void Start () {
        tPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        int cl = Random.Range(0, coinLine.Length);
        coinLine[cl].SetActive(true);
        int pun = Random.Range(0, 3);
        if (pun == 2)
        {
            int pu = Random.Range(0, 25);
            if (pu < powerUp.Length)
            {
                powerUp[pu].SetActive(true);
            }
            
        }
    }

    void Update()
    {
        
        if (Vector3.Distance(tPlayer.position, this.gameObject.transform.position) < 200 && Vector3.Distance(tPlayer.position, this.gameObject.transform.position) > 75 && Controller.coinIsVisible)
        {
            airCoin.SetActive(true);
        }
        if (Vector3.Distance(tPlayer.position, this.gameObject.transform.position) > 200)
        {
            airCoin.SetActive(false);
        }
        if (!Controller.iFly)
        {
            airCoin.SetActive(false);
        }
    }
}
