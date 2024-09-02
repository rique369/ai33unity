// Script use for manager sound in game
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudiosManager : MonoBehaviour {

	[System.Serializable]
	public class SoundGroup{
		public AudioClip audioClip;
		public string soundName;
		[Range(0.0f, 1.0f)]
		public float volume = 0.75f;
	}

	public AudioClip backgroundClip;
	
	public List<SoundGroup> musicClips = new List<SoundGroup>();
	
	public static AudiosManager instance;

    GameObject go;
    AudioSource sourcebg;

    public void Start(){
		instance = this;
		StartCoroutine(Background());
    }
	
	public void PlayingSound(string _soundName){
        if(sourcebg.mute != true)
		    AudioSource.PlayClipAtPoint(musicClips[FindSound(_soundName)].audioClip, Camera.main.transform.position,musicClips[FindSound(_soundName)].volume);
	}
	
	private int FindSound(string _soundName){
		int i = 0;
		while( i < musicClips.Count ){
			if(musicClips[i].soundName == _soundName){
				return i;	
			}
			i++;
		}
		return i;
	}
	
	IEnumerator Background()
	{
		yield return new WaitForSeconds(.1f);
		go = new GameObject ("Audio Background");
        sourcebg = go.AddComponent<AudioSource>();
		sourcebg.clip = backgroundClip;
		sourcebg.volume = 0.5f;
		sourcebg.loop = true;
        if (ProtectedPrefs.HasKey("Mute"))
        {
            if (ProtectedPrefs.GetInt("Mute") == 0)
            {
                sourcebg.mute = true;
            }
            else {
                sourcebg.mute = false;
            }
        }
        sourcebg.Play();

	}

    void Update() {
        if (Controller.iDie) sourcebg.mute = true;
        else if(GameControll.SaveMe && ProtectedPrefs.GetInt("Mute") != 0) sourcebg.mute = false;
    }

}
