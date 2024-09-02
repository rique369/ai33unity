using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class DailyGift : MonoBehaviour
{
    [Header("Settings")]
    public bool LockeInActiveGifts;
    public bool UseOnlineTime;
    public bool ChangeDayName;
    public bool ChangeGiftInfo;
    
    [Header("Others")] 
    private int DaysCount;
    private int MaxDayCount = 5;

    private DateTime LastGiftDateTime;

    private bool CanUserGetGift;

    public Action<int> OnUserWantGift;
    
    private string OnlineTimeUrl = "http://www.timeapi.org/utc/now";

    private string[] dayNames;
    private string[] giftInfos;
    private int coinGift;
    public Text giftText;
    public Scrollbar giftProgress;

    void Awake()
    {
        if (ProtectedPrefs.HasKey("Coins")) {
            coinGift = ProtectedPrefs.GetInt("Coins");
        }

        this.Start((days) =>
        {
            days++; 

            switch (days)
            {
                case 1:
                    giftText.text = "DAY 1: Your bonus + 300 Coin";
                    ProtectedPrefs.SetInt("Coins", coinGift + 300);
                    giftProgress.size = 0.2f;
                    break;
                case 2:
                    giftText.text = "DAY 2: Your bonus + 900 Coin";
                    ProtectedPrefs.SetInt("Coins", coinGift + 900);
                    giftProgress.size = 0.4f;
                    break;
                case 3:
                    giftText.text = "DAY 3: Your bonus + 1500 Coin";
                    ProtectedPrefs.SetInt("Coins", coinGift + 1500);
                    giftProgress.size = 0.6f;
                    break;
                case 4:
                    giftText.text = "DAY 4: Your bonus + 2000 Coin";
                    ProtectedPrefs.SetInt("Coins", coinGift + 2000);
                    giftProgress.size = 0.8f;
                    break;
                case 5:
                    giftText.text = "DAY 5: Your bonus + 5000 Coin";
                    ProtectedPrefs.SetInt("Coins", coinGift + 5000);
                    giftProgress.size = 1.0f;
                    break;

            }
        },
        5, 
        new []
        {
            "1 Day",
            "2 Day",
            "3 Day",
            "4 Day",
            "5 Day"
        },
        new []
        {
            "300 GOlD",
            "900 GOlD",
            "1500 GOlD",
            "2000 GOlD",
            "5000 GOlD"
        }
        );
    }

    void Save()
    {
        ProtectedPrefs.SetInt("DaysCount",DaysCount);   
        ProtectedPrefs.SetString("LastGiftDateTime",LastGiftDateTime.ToLongDateString());
    }
    void Load()
    {
        DaysCount = ProtectedPrefs.GetInt("DaysCount");

        if (ProtectedPrefs.HasKey("LastGiftDateTime"))
        LastGiftDateTime = DateTime.Parse(ProtectedPrefs.GetString("LastGiftDateTime"));
    }

     void Start()
    {
        GetGift();
        switch (ProtectedPrefs.GetInt("DaysCount"))
        {
            case 1:
                giftText.text = "DAY 1: Your bonus + 300 Coin";
                giftProgress.size = 0.2f;
                break;
            case 2:
                giftText.text = "DAY 2: Your bonus + 900 Coin";
                giftProgress.size = 0.4f;
                break;
            case 3:
                giftText.text = "DAY 3: Your bonus + 1500 Coin";
                giftProgress.size = 0.6f;
                break;
            case 4:
                giftText.text = "DAY 4: Your bonus + 2000 Coin";
                giftProgress.size = 0.8f;
                break;
            case 5:
                giftText.text = "DAY 5: Your bonus + 5000 Coin";
                giftProgress.size = 1.0f;
                break;

        }
    }

    /// <summary>
    /// Метод для запуска скрипта
    /// </summary>
    /// <param name="callback"> Функция в которую вернётся день (начиная с 0) за который пользовтелю нужно вручить подарок
    /// По нажатию пользователем кнопки "Забрать подарок".
    /// </param>
    /// <param name="_MaxDaysCount">Максимальное количество дней подряд за которое пользователь получит подарок, и счёт сбросится на новый день</param>
    /// <param name="dayNames">Массив для изменений названий дней (Работает только если опция ChangeDayName включена) </param>
    /// <param name="giftInfos">Массив для изменения описания подарка (Работает только если опция ChangeGiftInfo включена)</param>
    public void Start(Action<int> callback, int _MaxDaysCount =5 , string[] dayNames= null, string[] giftInfos = null)
    {
        Load();

        this.dayNames = dayNames;
        this.giftInfos = giftInfos;

#if UNITY_EDITOR
        if (ChangeDayName && dayNames != null && dayNames.Length != _MaxDaysCount)
        {
            Debug.LogWarning("dayNames array size dont equals to _MaxDaysCount");
        }
        if (ChangeGiftInfo && giftInfos != null && giftInfos.Length != _MaxDaysCount)
        {
            Debug.LogWarning("giftInfos array size dont equals to _MaxDaysCount");
        }
#endif

        if (callback != null)
            OnUserWantGift = callback;

        DateTime now = DateTime.Now;

        if (UseOnlineTime)
        {
            WWW www = new WWW(OnlineTimeUrl); 

            while (!www.isDone)  {  }

            if (!string.IsNullOrEmpty(www.error) || string.IsNullOrEmpty(www.text))
            {
                DateTime.TryParse(www.text, out now);

                if (now == DateTime.MinValue)
                {
#if UNITY_EDITOR
                    Debug.LogError("Server response parsing error or no Internet access. This error showing only in Edit mode");
                    giftText.text = "Error: no Internet connection!";
#endif


                    DaysCount = 0;
                    CanUserGetGift = false;
                    return;
                }
                else
                {
                    giftText.text = "Error: no Internet connection!";
                }

            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning("Server response is emptry or no Internet access");
                giftText.text = "Error: no Internet connection!";
#endif
            }


        }

        

            if (now.AddDays(-1).Day == LastGiftDateTime.Day &&
            now.AddDays(-1).Month == LastGiftDateTime.Month && now.AddDays(-1).Year == LastGiftDateTime.Year)
        {
            CanUserGetGift = true;
        }
            else if (now.Day == LastGiftDateTime.Day &&
                 now.Month == LastGiftDateTime.Month && now.Year == LastGiftDateTime.Year)
        {
            CanUserGetGift = false;
        }
        else
        {
            DaysCount = 0;
            CanUserGetGift = true;
        }

    }

    internal void GetGift()
    {
        if (CanUserGetGift)
        {
            if (OnUserWantGift != null)
                OnUserWantGift(DaysCount);

            DaysCount++;

            if (DaysCount == MaxDayCount)
            {
                DaysCount = 0;
            }

            LastGiftDateTime = DateTime.Now;

            CanUserGetGift = false;

        }

        Save();
            
    }
}
