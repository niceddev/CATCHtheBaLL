using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour {

	public GameObject moon, mars, asterBelt, jupiter, saturn, uranus, neptune; 
	private GameObject[] atmosphere;

	void Start(){
		atmosphere = GameObject.FindGameObjectsWithTag("atmosphereLayer");
	}

	void FixedUpdate () {
		if(GameCtrl.score > 100){
			foreach (GameObject item in atmosphere){
				Destroy(item.gameObject);
			}
		}	
		if(GameCtrl.score > 155){
			Destroy(moon);
			Destroy(mars);
		}
		if(GameCtrl.score > 265){
			Destroy(asterBelt);
			Destroy(jupiter);
		}
		if(GameCtrl.score > 365){
			Destroy(saturn);
		}
		if(GameCtrl.score > 555){
			Destroy(uranus);
			Destroy(neptune);
		}
	}
}
