using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionsList : MonoBehaviour {

    public Text book, pipe, emerald, gold, kalyan, rubine, urna, vaze, coincollect;


    private int bc, pc, ec, gc, kc, rc, uc, vc;

    void Awake() {
        if (!ProtectedPrefs.HasKey("mb")) ProtectedPrefs.SetInt("mb", 0);
        if (!ProtectedPrefs.HasKey("mp")) ProtectedPrefs.SetInt("mp", 0);
        if (!ProtectedPrefs.HasKey("me")) ProtectedPrefs.SetInt("me", 0);
        if (!ProtectedPrefs.HasKey("mg")) ProtectedPrefs.SetInt("mg", 0);
        if (!ProtectedPrefs.HasKey("mk")) ProtectedPrefs.SetInt("mk", 0);
        if (!ProtectedPrefs.HasKey("mr")) ProtectedPrefs.SetInt("mr", 0);
        if (!ProtectedPrefs.HasKey("mu")) ProtectedPrefs.SetInt("mu", 0);
        if (!ProtectedPrefs.HasKey("mv")) ProtectedPrefs.SetInt("mv", 0);
        if (!ProtectedPrefs.HasKey("cc")) ProtectedPrefs.SetInt("cc", 0);
    }

    void Start() {
        bc = ProtectedPrefs.GetInt("mBook");
        pc = ProtectedPrefs.GetInt("mPipe");
        ec = ProtectedPrefs.GetInt("mEmerald");
        gc = ProtectedPrefs.GetInt("mGold");
        kc = ProtectedPrefs.GetInt("mKalyan");
        rc = ProtectedPrefs.GetInt("mRubin");
        uc = ProtectedPrefs.GetInt("mUrna");
        vc = ProtectedPrefs.GetInt("mVaza");

        if (bc >= 230)
        {
            book.text = "compleit";
            if (ProtectedPrefs.GetInt("mb") == 0)
            {
                ProtectedPrefs.SetInt("mb", 1);
                ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") + 10000);
            }
        }
        else {
            book.text = ProtectedPrefs.GetInt("mBook").ToString() + " / 230";
        }

        if (pc >= 300)
        {
            pipe.text = "compleit";
            if (ProtectedPrefs.GetInt("mp") == 0)
            {
                ProtectedPrefs.SetInt("mp", 1);
                ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") + 15000);
            }
        }
        else {
            pipe.text = ProtectedPrefs.GetInt("mPipe").ToString() + " / 300";
        }

        if (ec >= 350)
        {
            emerald.text = "compleit";
            if (ProtectedPrefs.GetInt("me") == 0)
            {
                ProtectedPrefs.SetInt("me", 1);
                ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") + 25000);
            }
        }
        else {
            emerald.text = ProtectedPrefs.GetInt("mEmerald").ToString() + " / 350";
        }

        if (gc >= 500)
        {
            gold.text = "compleit";
            if (ProtectedPrefs.GetInt("mg") == 0)
            {
                ProtectedPrefs.SetInt("mg", 1);
                ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") + 50000);
            }
        }
        else {
            gold.text = ProtectedPrefs.GetInt("mGold").ToString() + " / 500";
        }

        if (kc >= 550)
        {
            kalyan.text = "compleit";
            if (ProtectedPrefs.GetInt("mk") == 0)
            {
                ProtectedPrefs.SetInt("mk", 1);
                ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") + 70000);
            }
        }
        else {
            kalyan.text = ProtectedPrefs.GetInt("mKalyan").ToString() + " / 550";
        }

        if (rc >= 650)
        {
            rubine.text = "compleit";
            if (ProtectedPrefs.GetInt("mr") == 0)
            {
                ProtectedPrefs.SetInt("mr", 1);
                ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") + 100000);
            }
        }
        else {
            rubine.text = ProtectedPrefs.GetInt("mRubin").ToString() + " / 650";
        }

        if (uc >= 750)
        {
            urna.text = "compleit";
            if (ProtectedPrefs.GetInt("mu") == 0)
            {
                ProtectedPrefs.SetInt("mu", 1);
                ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") + 180000);
            }
        }
        else {
            urna.text = ProtectedPrefs.GetInt("mUrna").ToString() + " / 750";
        }

        if (vc >= 800)
        {
            vaze.text = "compleit";
            if (ProtectedPrefs.GetInt("mv") == 0)
            {
                ProtectedPrefs.SetInt("mv", 1);
                ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") + 250000);
            }
        }
        else {
            vaze.text = ProtectedPrefs.GetInt("mVaza").ToString() + " / 800";
        }

        if (ProtectedPrefs.GetInt("Coins") >= 500000)
        {
            coincollect.text = "compleit";
            if (ProtectedPrefs.GetInt("cc") == 0)
            {
                ProtectedPrefs.SetInt("cc", 1);
                ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") + 350000);
            }
        }
        else {
            coincollect.text = "none";
        }
    }
}
