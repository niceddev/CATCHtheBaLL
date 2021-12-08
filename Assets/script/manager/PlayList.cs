using System;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class PlayList : MonoBehaviour {

	public AudioClip[] clips;
	public static PlayList instance;
	private AudioSource audioSource;

	public float MusicVolume{
		get{
			return audioSource.volume;
		}
		set{
			audioSource.volume = value;
		}
	}

	void Awake() {
		if(instance == null){
			instance = this;
		}else{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);

		audioSource = gameObject.AddComponent<AudioSource>();
	}	

	private AudioClip GetRandomClip(){
		return clips[UnityEngine.Random.Range(0, clips.Length)];
	}

	void Start(){
		if(!Social.localUser.authenticated){
			Social.localUser.Authenticate((bool success) => {}); //GOOGLE PLAY GAMES SERVICE
		}

		if(!PlayerPrefs.HasKey("Music") || PlayerPrefs.GetString("Music") == "yes")
			Mute(false);
		else
			Mute(true);

		if(!PlayerPrefs.HasKey("Music") || PlayerPrefs.GetString("Music") == "yes" && !audioSource.isPlaying){
			audioSource.clip = GetRandomClip();
			audioSource.Play();
		}
	}

	void Update(){
		if(!audioSource.isPlaying && PlayerPrefs.GetString("Music") == "yes"){
			audioSource.clip = GetRandomClip();
			audioSource.Play();
		}
	}

	public void Mute(bool mute) {
		if(mute)
			audioSource.Stop();
		else
			audioSource.Play();
	}
}
