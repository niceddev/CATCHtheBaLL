using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour {

	public ParticleSystem dustEffect, jumpEffect;  

	void Start () {
		dustEffect.Play();
	}
	
	void Update () {
		if(GameCtrl.gameBegin)
			dustEffect.Stop();
		if (Input.GetMouseButton(0) && GameCtrl.ballOnBucket && !GameCtrl.gamePause &&Camera.main.ScreenToViewportPoint(Input.mousePosition).y < 0.9f)
			jumpEffect.Emit(17);
	}
}
