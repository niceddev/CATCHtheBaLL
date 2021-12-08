using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkinCtrl : MonoBehaviour {

	private int cost;
	private bool bought;

	void Start(){
		cost = int.Parse(transform.Find("Cost").transform.Find("Text").GetComponent<Text>().text);
		if(PlayerPrefs.GetInt(GetComponent<Image>().sprite.name) == 1){
			transform.Find("Cost").gameObject.SetActive(false);
			GetComponent<Image>().material = null;
			bought = true;
		}
	}

	public void BuySkin(){
		if(EventSystem.current.currentSelectedGameObject.GetComponent<Image>().material != null && GameCtrl.money >= cost && !bought){
			EventSystem.current.currentSelectedGameObject.GetComponent<Image>().material = null;
			EventSystem.current.currentSelectedGameObject.transform.Find("Cost").gameObject.SetActive(false);
			PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - int.Parse(EventSystem.current.currentSelectedGameObject.transform.Find("Cost").transform.Find("Text").GetComponent<Text>().text));
			bought = true;
			//bought
			PlayerPrefs.SetInt(EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name, 1);
		}
	}

}
