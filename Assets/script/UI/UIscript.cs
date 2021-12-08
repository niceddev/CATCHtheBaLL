using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class UIscript : MonoBehaviour {

	public string ANDROID_RATE_URL = "market://details?id=com.nicedev.catchtheball";
	public string MORE_GAMES_URL = "market://dev?id=4689293608076356158";
	public GameObject blackBg, ratePanel, infoPanel, pausePanel, exitPanel, secChance;
	public Button[] menuBut;
	public Animator[] menuAnimations;
	public Animator gameAnimations, stgAnim, gpgsAnim;
	public Animator logo, bgMenu, backButton;
	private bool infoPanOn = false, exitPanOn = true;

	void Start(){
		for (int i = 0; i < 2; i++)
			menuAnimations[i].SetBool("AnimIn", false);
		logo.SetBool("AnimIn", false);
		GameCtrl.gameOver = false;
	}

	void Update(){
		if(GameCtrl.gameBegin){
			gameAnimations.SetBool("AnimIn", true);
			exitPanOn = false;
		}
		//CLOSE BACK BUTTON
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(blackBg.activeSelf){
				blackBg.SetActive(false);
				ratePanel.SetActive(false);
				infoPanel.SetActive(false);
				exitPanel.SetActive(false);
				infoPanOn = false;
				exitPanOn = true;
				foreach(Button item in menuBut)
					item.interactable = true;
			}else{
				infoPanOn = true;
				if(exitPanOn && bgMenu.gameObject.transform.localPosition.y < -2000) ExitPanel();	
			}
			if(GameCtrl.gameOver) RestartGame();
			if(GameCtrl.gameBegin && !GameCtrl.gameOver){
				if(GameCtrl.gamePause)
					ContinueGame();
				else
					PauseGame();
			}
			
			if(stgAnim.GetBool("AnimIn") && stgAnim.gameObject.transform.localPosition.y == -700 && infoPanOn){
				stgAnim.SetBool("AnimIn", false);
				exitPanOn = true;
			}
			if(gpgsAnim.GetBool("AnimIn") && gpgsAnim.gameObject.transform.localPosition.y == -700){
				gpgsAnim.SetBool("AnimIn", false);
				exitPanOn = true;
			}
			if(infoPanOn && stgAnim.gameObject.transform.localPosition.y == -700 || gpgsAnim.gameObject.transform.localPosition.y == -700){
				if(bgMenu.GetBool("AnimIn") && backButton.GetBool("AnimIn")){
					bgMenu.SetBool("AnimIn", false);
					backButton.SetBool("AnimIn", false);
				}
				foreach (Animator item in menuAnimations)
					item.SetBool("AnimIn", false);
			}

		}
		if(menuAnimations[0].gameObject.transform.localPosition.y == -50)
			menuAnimations[0].SetBool("Idle", true);
	}

	public void ShowLeaderBoard(){
		if(Social.localUser.authenticated)
			Social.ShowLeaderboardUI();
		else
			Social.localUser.Authenticate((bool success) => {});
	}

	public void ShowAchievements(){
		if(Social.localUser.authenticated)
			Social.ShowAchievementsUI();
		else
			Social.localUser.Authenticate((bool success) => {});
	}

	public void PlayTheGame(){
		exitPanOn = false;
		GameCtrl.menuState = false;
		for (int i = 0; i < 2; i++)
			menuAnimations[i].SetBool("AnimIn", true);
		logo.SetBool("AnimIn", true);
	}

	public void RestartGame(){
		PlayList.instance.MusicVolume = 0.5f;
		// AdManager.loadCount--;
		SceneManager.LoadScene("main");
	}

	public void BackButton(){
		bgMenu.SetBool("AnimIn", false);
		backButton.SetBool("AnimIn", false);
		stgAnim.SetBool("AnimIn", false);
		gpgsAnim.SetBool("AnimIn", false);
		foreach (Animator item in menuAnimations)
			item.SetBool("AnimIn", false);
		menuAnimations[0].SetBool("Idle", false);
		exitPanOn = true;
	}

	public void SettingsItem(){
		bgMenu.SetBool("AnimIn", true);
		backButton.SetBool("AnimIn", true);
		stgAnim.SetBool("AnimIn", true);
		foreach (Animator item in menuAnimations)
			item.SetBool("AnimIn", true);
		menuAnimations[0].SetBool("Idle", false);
		exitPanOn = false;
	}

	public void GPGSItem(){
		bgMenu.SetBool("AnimIn", true);
		backButton.SetBool("AnimIn", true);
		gpgsAnim.SetBool("AnimIn", true);
		foreach (Animator item in menuAnimations)
			item.SetBool("AnimIn", true);
		menuAnimations[0].SetBool("Idle", false);
		exitPanOn = false;
	}

	//------Rate panel-----------------------------
	void OnApplicationFocus(){
		if(ratePanel.activeSelf)
			blackBg.SetActive(false);
		ratePanel.SetActive(false);
		foreach(Button item in menuBut)
			item.interactable = true;
	}

	public void OpenRatePanel(){
		exitPanOn = false;
		blackBg.SetActive(true);
		ratePanel.SetActive(true);
		foreach(Button item in menuBut)
			item.interactable = false;
	}

	public void CloseRatePanel(){
		blackBg.SetActive(false);
		ratePanel.SetActive(false);
		foreach(Button item in menuBut)
			item.interactable = true;
		exitPanOn = true;
	}
	//------Info panel-----------------------------
	public void InfoPanel(){
		infoPanOn = true;
		blackBg.SetActive(true);
		infoPanel.SetActive(true);
		foreach(Button item in menuBut)
			item.interactable = false;
	}

	public void CloseInfo(){
		infoPanOn = false;
		blackBg.SetActive(false);
		infoPanel.SetActive(false);
		foreach(Button item in menuBut)
			item.interactable = true;
	}
	//------Policy-----------------------------
	public void PolicyURL(){
		Application.OpenURL("https://niceddev.github.io/Privacy-Policy-Terms-Conditions/CATCH%20the%20BaLL/p_p");
	}
	//------Pause panel-----------------------------
	public void PauseGame(){
		blackBg.SetActive(true);
		pausePanel.SetActive(true);
		GameCtrl.gamePause = true;
		Time.timeScale = 0;
	}

	public void ContinueGame(){
		blackBg.SetActive(false);
		pausePanel.SetActive(false);
		GameCtrl.gamePause = false;
		Time.timeScale = 1;
	}
	//------Exit panel-----------------------------
	public void ExitPanel(){
		blackBg.SetActive(true);
		exitPanel.SetActive(true);
	}

	public void YesExit(){
		Application.Quit();
	}

	public void NoExit(){
		blackBg.SetActive(false);
		exitPanel.SetActive(false);
	}
	//Links
	public void MoreGames(){
		Application.OpenURL(MORE_GAMES_URL);
	}

	public void	RateIt(){
		Application.OpenURL(ANDROID_RATE_URL);
	}
}
