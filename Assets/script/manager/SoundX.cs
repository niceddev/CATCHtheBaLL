using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundX : MonoBehaviour {

	private GameObject X;

	void Start () {
		X = transform.Find("X").gameObject;
	}
	
	void Update () {
		if(PlayerPrefs.GetString("Sound") == "no")
			X.SetActive(true);
		else
			X.SetActive(false);
	}
}
