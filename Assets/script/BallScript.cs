using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour {

	public Camera cam;
	public SpriteRenderer[] objs;
	public Transform upPoint;
	public ParticleSystem coinEffect;
	private Rigidbody2D rb2d;

	void Start(){
	 	rb2d = GetComponent<Rigidbody2D>();
	}

	void Update(){
		if(!GameCtrl.gameBegin){
			rb2d.bodyType = RigidbodyType2D.Static;
		}else{
			rb2d.bodyType = RigidbodyType2D.Dynamic;
		}

		//keep ball in scene
		Vector3 pos = cam.WorldToViewportPoint(transform.position);
		pos.x = Mathf.Clamp01(pos.x);
		
		Vector3 speed = rb2d.velocity;
		if(transform.position.x < -3.15f || transform.position.x > 3.15f)
			speed.x = 0;

		transform.position = cam.ViewportToWorldPoint(pos);
		rb2d.velocity = speed;
		//

		//opacity of cave
		foreach(SpriteRenderer item in objs){
			if(upPoint.position.y >= item.transform.position.y)
				item.color = Color.Lerp(item.color, new Color(1,1,1,0.65f), Time.deltaTime * 0.5f);
		}
		//
	} 

	void OnTriggerEnter2D(Collider2D col){
		GameCtrl.ballOnBucket = true;	
		if(col.tag == "coin"){
			GameCtrl.money += 1;			
			coinEffect.Emit(4);
			AudioManager.instance.Play("pickup");
			PlayerPrefs.SetInt("Money", GameCtrl.money);
		}	
	}

	void OnTriggerExit2D(Collider2D col){
		GameCtrl.ballOnBucket = false;
	}
	
}
