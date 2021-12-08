using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDestroy : MonoBehaviour {

	private SpriteRenderer mat;

	void Start(){
		mat = GetComponent<SpriteRenderer>();
	}

	void Update(){
		if(GameCtrl.score > 25)
			mat.color = new Color32(53, 152, 217, 255);
		if(GameCtrl.score > 56)
			mat.color = new Color32(237, 138, 25, 255);
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "ball")
			Destroy(gameObject);
	}

}
