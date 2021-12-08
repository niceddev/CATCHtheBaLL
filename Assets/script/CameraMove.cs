using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public GameObject ball, spaceBg;
	public float smoothSpeed = 0.12f, offsetY = 0.32f;
	private Animator animator;
	private Rigidbody2D ballRb2d;

	void Start(){
		ballRb2d = ball.GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		animator.enabled = false;
	}

	public GameObject GetCamera{
		get{
			return this.gameObject;
		}
	}

	void Update () {
		if(!GameCtrl.menuState){
			animator.enabled = true;
			if(GameCtrl.gameBegin){
				Vector2 smoothedPos = Vector2.Lerp(transform.position, ball.transform.position, smoothSpeed);
				if(GameCtrl.ballOnBucket && ballRb2d.velocity.y <= 0)
					transform.position = new Vector3(0, smoothedPos.y + smoothSpeed + offsetY, transform.position.z);
			}
		}

		if(GameCtrl.gameBegin)
			animator.enabled = false;

		if(GameCtrl.score > 75){
			spaceBg.transform.SetParent(this.transform);
			spaceBg.transform.localPosition = new Vector3(0,0,spaceBg.transform.localPosition.z);
		}
	}

}
