using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsParallax : MonoBehaviour {

	public Transform[] layers;
	private Transform cameraTransform;
	private float targetPos = 50f;

	void Start () {
		cameraTransform = Camera.main.transform;
	}

	void Update(){
		float pos0 = layers[0].position.y;
		float pos1 = layers[1].position.y;
		float pos2 = layers[2].position.y;
			
		if(cameraTransform.position.y > pos1)
			layers[0].position = new Vector3(layers[0].position.x, pos2 + targetPos, layers[0].position.z);
		if(cameraTransform.position.y > pos2)
			layers[1].position = new Vector3(layers[1].position.x, pos0 + targetPos, layers[1].position.z);
		if(cameraTransform.position.y > pos0)
			layers[2].position = new Vector3(layers[2].position.x, pos1 + targetPos, layers[2].position.z);
	}
	
}
