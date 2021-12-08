using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour {

	void Start () {
		if(!PlayerPrefs.HasKey("Sound") && !PlayerPrefs.HasKey("Music")){
			PlayerPrefs.SetString("Sound","yes");
			PlayerPrefs.SetString("Music","yes");
		}
	}
	
	public void ToggleSound(){
		if(PlayerPrefs.GetString("Sound") == "no"){
			PlayerPrefs.SetString("Sound","yes");
			AudioManager.instance.Mute(false);
		}else{
			PlayerPrefs.SetString("Sound","no");
			AudioManager.instance.Mute(true);
		}
	}
	
	public void ToggleMusic(){
		if(PlayerPrefs.GetString("Music") == "no"){
			PlayerPrefs.SetString("Music","yes");
			PlayList.instance.Mute(false);
		}else{
			PlayerPrefs.SetString("Music","no");
			PlayList.instance.Mute(true);
		}
	}

}
