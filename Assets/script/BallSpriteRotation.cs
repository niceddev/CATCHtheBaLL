using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpriteRotation : MonoBehaviour {

	public Transform targetPos;
	private float rotationSpeed = 15f;
	private SpriteRenderer mainSprite;

	void Start(){
		mainSprite = GetComponent<SpriteRenderer>();
	}

	void Update () {
		transform.position = targetPos.position;
		if(!GameCtrl.ballOnBucket){
			transform.Rotate(Vector3.forward * rotationSpeed);
		}
		if(PlayerPrefs.HasKey(SnapScrolling.instance.skins[SnapScrolling.instance.GetSelectedSkinID].ballSkin.name))
			mainSprite.sprite = SnapScrolling.instance.skins[SnapScrolling.instance.GetSelectedSkinID].ballSkin;
	}
}
