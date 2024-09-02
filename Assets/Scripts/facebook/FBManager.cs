namespace Facebook.Unity
{
	using System;
	using UnityEngine;
	using UnityEngine.UI;
	using System.Collections;
	using System.Collections.Generic;
	using Facebook.MiniJSON;
    using Gley.MobileAds;

    internal class FBManager : MonoBehaviour{
        [HideInInspector]
		public FBManager instance;

		public GameObject UIFBLogin, UIFBNotLogin;
		private List<object> scorelist = null;
		public GameObject ScoreEntry;
		public GameObject ScrolList;
		private Texture2D pictureTexture;
		public static bool check = false;
        public static bool login = false;
        int lg;

		void Start(){
			instance = this;
			check = false;
            if (ProtectedPrefs.HasKey("login")) {
                lg = ProtectedPrefs.GetInt("login");
            }
            if (lg == 0)
            {
                login = false;
            }
            else {
                login = true;
            }
			API.Initialize();
		}

		void Awake(){
			if (FB.IsInitialized) {
				FB.ActivateApp();
			} else {
				FB.Init (this.OnInitComplete, this.OnHideUnity);
			}

		}

		void Update(){
			if (check) {
				OnInitComplete ();
				check = false;
			}
		}

		private void OnInitComplete(){
			FB.ActivateApp();
			if (FB.IsLoggedIn) {
				DealWithFBMenu(true);
			}else{
				DealWithFBMenu(false);
			}
		}

		private void OnHideUnity(bool isGameShow){
			if (!isGameShow) {
				Time.timeScale = 0; 
			} else {
				Time.timeScale = 1; 
			}
		}

		public void FBLogin(){
			FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email", "user_friends" }, this.HandleResul);
		}

		void HandleResul(IResult result){
			FB.LogInWithPublishPermissions (new List<string>(){"publish_actions"}, this.HandleResult);
		}
			
		void HandleResult(IResult result){
			if (FB.IsLoggedIn) {
				DealWithFBMenu(true);
			} else {
				DealWithFBMenu(false);
			}
		}

		public void DealWithFBMenu(bool isLoggedIn){
			if (isLoggedIn) {
				UIFBLogin.SetActive(true);
				UIFBNotLogin.SetActive(false);
                login = true;
                ProtectedPrefs.SetInt("login", 1);
            } else {
				UIFBLogin.SetActive(false);
				UIFBNotLogin.SetActive(true);
                login = false;
                ProtectedPrefs.SetInt("login", 0);
            }
		}

		public void ShareWithFriends(){
            if (FB.IsLoggedIn) {
                FB.FeedShare(
                    string.Empty,
                    new Uri("https://play.google.com/store/apps/details?id=com.vade.eastRunner"),
                    " DESERT PRINCE RUNNER",
                    " Come play this amazing game!",
                    string.Format("{0} {1}", "My best score: ", ProtectedPrefs.GetInt("HighScore").ToString()),
                    new Uri("http://cs627617.vk.me/v627617962/7156b/xMRVCMVBzWE.jpg"),
                    string.Empty,
                    this.sharCall);
            }
		}

		private void sharCall(IResult res){
			UIFBLogin.SetActive (true);
		}

		public void InviteFriend(){
			UIFBLogin.SetActive (false);
			FB.Mobile.AppInvite(new Uri("https://fb.me/1704835226456110"), new Uri("http://cs627617.vk.me/v627617962/7156b/xMRVCMVBzWE.jpg"), this.invResult);
		}

		private void invResult(IResult result){
			UIFBLogin.SetActive (true);
		}

		public void QueryScores(){
			FB.API ("/app/scores?fields=score,user.limit(20)", HttpMethod.GET, ScoreCallack);

		}

		private void ScoreCallack(IResult result){
			scorelist = Util.DeserializeScores(result.RawResult);
			print (result.RawResult);

			foreach (Transform child in ScrolList.transform) {
				GameControll.Destroy(child.gameObject);
			}

			foreach (object score in scorelist) {
				var entry = (Dictionary<string,object>) score;
				var user = (Dictionary<string,object>) entry["user"];

				GameObject ScrolPanel;
				ScrolPanel = Instantiate(ScoreEntry) as GameObject;
				ScrolPanel.transform.SetParent(ScrolList.transform);

				Transform ThisUserName = ScrolPanel.transform.Find("UserName");
				Transform ThisUserScore = ScrolPanel.transform.Find("UserScore");
				Text uName = ThisUserName.GetComponent<Text>();
				Text uScore = ThisUserScore.GetComponent<Text>();

				uName.text = user["name"].ToString();
				uScore.text = "Best score: " + entry["score"].ToString();

				Transform TheUserAvatar = ScrolPanel.transform.Find("UserAvatar");
				Image uAvatar = TheUserAvatar.GetComponent<Image>();

				FB.API (Util.GetPictureURL(user["id"].ToString(), 128,128), HttpMethod.GET, delegate(IGraphResult pictureResult)
					{
						string imageUrl = Util.DeserializePictureURLString(pictureResult.RawResult);
						StartCoroutine(LoadPictureEnumerator(imageUrl,pictureTexture =>
							{
								uAvatar.sprite = Sprite.Create (pictureTexture, new Rect(0,0,128,128), new Vector2(0,0));
							}));
					});

			}

		}
		delegate void LoadPictureCallback (Texture2D texture);

		IEnumerator LoadPictureEnumerator(string url, LoadPictureCallback callback)    
		{
			WWW www = new WWW(url);
			yield return www;
			callback(www.texture);
		}

		public string DeserializePictureURLString(string response)
		{
			return DeserializePictureURLObject(Json.Deserialize(response));
		}

		public string DeserializePictureURLObject(object pictureObj)
		{
			var picture = (Dictionary<string, object>)(((Dictionary<string, object>)pictureObj)["data"]);
			object urlH = null;
			if (picture.TryGetValue("url", out urlH))
			{
				return (string)urlH;
			}
			return null;
		}

		private string GetPictureURL(string facebookID, int? width = null, int? height = null, string type = null)
		{
			string url = string.Format("/{0}/picture", facebookID);
			string query = width != null ? "&width=" + width.ToString() : "";
			query += height != null ? "&height=" + height.ToString() : "";
			query += type != null ? "&type=" + type : "";
			query += "&redirect=false";
			if (query != "") url += ("?g" + query);
			return url;
		}

		public static IEnumerator SetScore(){
			if (FB.IsLoggedIn) {
				var scoreData = new Dictionary<string,string> ();
				scoreData ["score"] = ProtectedPrefs.GetInt("HighScore").ToString ();
				FB.API ("/me/scores", HttpMethod.POST, delegate(IGraphResult result) {
					print ("Score result" + result.Error);
				}, scoreData);
			}
			yield return 1;
		}

		public static IEnumerator Check(){
			check = true;
			yield return 1;
		}

		public void Out(){
            login = false;
            ProtectedPrefs.SetInt("login", 0);
            FB.LogOut ();
			UIFBLogin.SetActive (false);
			UIFBNotLogin.SetActive (true);
		}

	}
}