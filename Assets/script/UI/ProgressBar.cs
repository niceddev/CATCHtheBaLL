using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

	public static ProgressBar instance;
	public GameObject loadingBar;

	public GameObject RadBar{
		get{
			return loadingBar;
		}
	}

	void Awake(){
		instance = this;
	}

    void Update(){
		if(GameCtrl.gameOver)
			loadingBar.GetComponent<Animator>().SetBool("GameOver", true);
	}
}