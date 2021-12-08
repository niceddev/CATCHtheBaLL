using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameCtrl : MonoBehaviour {

	private const float GAP = 2.5f;
	private const string LEADERBOARD = "CgkIxojX9YAXEAIQAQ";
	private const string ACHIEV1 = "CgkIxojX9YAXEAIQAw";
	private const string ACHIEV2 = "CgkIxojX9YAXEAIQAg";
	private const string ACHIEV3 = "CgkIxojX9YAXEAIQBA";
	private const string ACHIEV4 = "CgkIxojX9YAXEAIQBQ";
	private const string ACHIEV5 = "CgkIxojX9YAXEAIQBg";

	public static int score, money;
	private	int targetScore = 10;
	private	float scoreIncrementTarget = GAP;
	
	public GameObject[] canvasObjects;
	public GameObject ball, bucketPrefab, cam;
	public Text scoreText, maxScore, moneyText, gameMoneyText;
	public static bool ballOnBucket, gameOver = false, gameBegin, menuState, chanceOff, gamePause;
	public static Vector2 screenSize, lastBucketPos;

	private Camera cameraaa;
	private Rigidbody2D rb;
	private float jumpHeight = 25;
	private List<GameObject> buckets;
	private int numberOfBuckets = 4;

	void Start () {
		Time.timeScale = 1f;
		chanceOff = false;
		score = 0;
		money = PlayerPrefs.GetInt("Money");
		gameBegin = false;
		gameOver = false;
		gamePause = false;
		PlayList.instance.MusicVolume = 0.8f;
		menuState = true;
		BucketScript.speed = 2.5f;
		rb = ball.GetComponent<Rigidbody2D>();
		buckets = new List<GameObject>();
		screenSize = cam.GetComponent<Camera>().ScreenToWorldPoint(cam.transform.position);
		cameraaa = cam.GetComponent<Camera>();
		Vector2 spawnPos = new Vector2();

		for(int i = 0; i < numberOfBuckets; i++){
			spawnPos.x = Random.Range(-2.5f, 2.5f);
			spawnPos.y += GAP;
			buckets.Add(Instantiate(bucketPrefab, spawnPos, Quaternion.identity));
		}
	}
		
	void Update () {
		if(Camera.main.transform.position.y <= 100)
			foreach(GameObject item in canvasObjects)
				item.SetActive(false); //hide menu	
		if(Camera.main.transform.position.y <= 3)
			gameBegin = true; // very necessary
		
		if(!menuState){
			GameDifficultySystem();
			ScoreSystem();
			GameOver();
		}

		if(gameBegin){
			JumpScript();
			CreateNewBucket();
		} 

		lastBucketPos = (Vector2)buckets[buckets.Count - 3].gameObject.transform.position;

		maxScore.text = PlayerPrefs.GetInt("Score").ToString();
		moneyText.text = PlayerPrefs.GetInt("Money").ToString();
		gameMoneyText.text = PlayerPrefs.GetInt("Money").ToString();
		money = PlayerPrefs.GetInt("Money");
		AchievmentsCtrl();
	}
	
	private void ScoreSystem(){
		if(ball.transform.position.y > scoreIncrementTarget && rb.velocity.y <= 0 && ballOnBucket){
			score++;
			scoreText.text = score.ToString(); 
			scoreIncrementTarget += GAP;
			AudioManager.instance.Play("score");
		}
	}

	private void GameDifficultySystem(){
		if(score >= targetScore && BucketScript.speed < 3.8f){
			BucketScript.speed += 0.1f;
			targetScore += 10;
		}

		if(score % 5 == 1)
			StopEverySecondBucket();
		else if((score > 20 && score < 40) || (score > 60 && score < 100) || (score > 130 && score < 150) || (score > 180 && score < 210) || (score > 250 && score < 300) || (score > 340 && score < 360) || (score > 390 && score < 430))
			StopEverySecondBucket();
		else
			BucketScript.instance.BucketBodyType = RigidbodyType2D.Kinematic;
	}

	private void StopEverySecondBucket(){
		if(buckets.Count % 2 == 0){
			BucketScript.instance.BucketBodyType = RigidbodyType2D.Static;
		}
	}

	private void CreateNewBucket(){
		if(cam.transform.position.y > buckets.Last().gameObject.transform.position.y - GAP){
			buckets.Add(Instantiate(bucketPrefab, new Vector2(Random.Range(-2.5f, 2.5f),buckets.Last().gameObject.transform.position.y + GAP), Quaternion.identity));
		}
	}

	private void JumpScript(){
		if (Input.GetMouseButton(0) && ballOnBucket && rb.velocity.y <= 0 && cameraaa.ScreenToViewportPoint(Input.mousePosition).y < 0.9f && !gamePause){
			rb.velocity = transform.up * jumpHeight;
			AudioManager.instance.Play("jump");
		}
	}

	private void GameOver(){
		Vector2 ballPosY = cam.GetComponent<Camera>().WorldToScreenPoint(ball.transform.position);
		if (ballPosY.y < -100 && score > 0){
			gameOver = true;
			PlayList.instance.MusicVolume = 0.5f;
			Time.timeScale = 0f;
			if(PlayerPrefs.GetInt("Score") < score){
				PlayerPrefs.SetInt("Score", score);
				Social.ReportScore(score, LEADERBOARD, (bool success) => {});
			}
		}else if(ballPosY.y < -100 && score == 0){
			ball.transform.position = new Vector2(0,0.4f);
		}
	}
	
	public void GetAchiev(string id){
		Social.ReportProgress(id, 100.0f, (bool success) => {});
	}

	private void AchievmentsCtrl(){
		switch(score){
			case 55: GetAchiev(ACHIEV1);
			break;
			case 90: GetAchiev(ACHIEV2);
			break;
			case 132: GetAchiev(ACHIEV3);
			break;
			case 205: GetAchiev(ACHIEV4);
			break;
			case 230: GetAchiev(ACHIEV5);
			break;
		}
	}
}
