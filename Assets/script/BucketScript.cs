using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BucketScript : MonoBehaviour {

	public static float speed = 2.5f;
	public static BucketScript instance;
	public GameObject starCoin;
	private Rigidbody2D rb2d;

	public Vector2 GetBucket{
		get{
			return transform.position;
		}
		set{
			transform.position = value;
		}
	}

	public RigidbodyType2D BucketBodyType{
		get{
			return GetComponent<Rigidbody2D>().bodyType;
		}
		set{
			if(transform.position.x <= 2.5f || transform.position.x >= 2.5f)
				GetComponent<Rigidbody2D>().bodyType = value;
		}
	}

	void Awake(){
		instance = this;
	}

	void Start () {
	 	rb2d = GetComponent<Rigidbody2D>();
		int randDir = Random.Range(0,2);

		if(randDir == 0)
			rb2d.velocity = new Vector2(-speed, 0);
		else
			rb2d.velocity = new Vector2(speed, 0);

		//starCoin
		if(randDir == 0 && GameCtrl.score > 5){
			GameObject coin = Instantiate(starCoin, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity) as GameObject;
			coin.transform.SetParent(gameObject.transform);
		}
	}

	void Update() {
		if (transform.position.x > -GameCtrl.screenSize.x - transform.localScale.x/1.5f)
			rb2d.velocity = new Vector2(-speed, 0);
		else if (transform.position.x < GameCtrl.screenSize.x + transform.localScale.x/1.5f)
			rb2d.velocity = new Vector2(speed, 0);

		Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if(screenPosition.y < 0 && GameCtrl.gameBegin)
			Destroy(gameObject);
	}

}
