using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicX : MonoBehaviour {

	private GameObject X;

	void Start () {
		X = transform.Find("X").gameObject;
	}
	
	void Update () {
		if(PlayerPrefs.GetString("Music") == "no")
			X.SetActive(true);
		else
			X.SetActive(false);
	}
}
