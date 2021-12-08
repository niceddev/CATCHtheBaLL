using System;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;
	public static AudioManager instance;
	private AudioSource[] audioSources;

	void Awake() {
		if(instance == null){
			instance = this;
		}else{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);

		PlayGamesPlatform.Activate(); //GOOGLE PLAY GAMES SERVICE

		foreach (var s in sounds){
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
		}
	}	

	void Start(){
		if(!Social.localUser.authenticated){
			Social.localUser.Authenticate((bool success) => {}); //GOOGLE PLAY GAMES SERVICE
		}

		audioSources = GetComponents<AudioSource>();
		if(!PlayerPrefs.HasKey("Sound") || PlayerPrefs.GetString("Sound") == "yes")
			Mute(false);
		else
			Mute(true);
	}

	public void Play(string name) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if(PlayerPrefs.GetString("Sound") == "yes")
			s.source.Play();
	}

	public void Mute(bool mute) {
		foreach (AudioSource s in audioSources){
			if(mute){
				s.Stop();
			}else{
				s.Play();
			}
		}
	}
}
