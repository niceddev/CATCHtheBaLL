using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour{

    public static SnapScrolling instance;
    [Header("Skins")]
    public BallSkin[] skins;
    [Header("Controllers")]
    [Range(0, 500)]
    public int panOffset;
    [Range(0f, 20f)]
    public float snapSpeed;
    [Range(0f, 10f)]
    public float scaleOffset;
    [Range(1f, 20f)]
    public float scaleSpeed;
    [Header("Other Objects")]
    public GameObject panPrefab;
    public ScrollRect scrollRect;

    private GameObject[] instPans;
    private Vector2[] pansPos;
    private Vector2[] pansScale;

    private RectTransform contentRect;
    private Vector2 contentVector;

    private int selectedPanID;
    private bool isScrolling, turnOn = false;

    public int GetSelectedSkinID{get{return selectedPanID;}}

    void Awake(){
        instance = this;
    }

	private void Start(){
	    contentRect = GetComponent<RectTransform>();
        instPans = new GameObject[skins.Length];
        pansPos = new Vector2[skins.Length];
        pansScale = new Vector2[skins.Length];
	    for (int i = 0; i < skins.Length; i++){
	        instPans[i] = Instantiate(panPrefab, transform, false);
	        if (i == 0) continue;
	        instPans[i].transform.localPosition = new Vector2(instPans[i-1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset, 
                instPans[i].transform.localPosition.y);
	        pansPos[i] = -instPans[i].transform.localPosition;
            //add skins
            instPans[0].GetComponent<Image>().sprite = skins[0].ballSkin;
            instPans[0].GetComponent<Image>().material = null;
            instPans[0].transform.Find("Cost").gameObject.SetActive(false);
            instPans[i].GetComponent<Image>().sprite = skins[i].ballSkin;
            instPans[i].transform.Find("Cost").transform.Find("Text").gameObject.GetComponent<Text>().text = skins[i].cost.ToString();
	    }
        selectedPanID = PlayerPrefs.GetInt("SelectedSkin");
        StartCoroutine(WaitOneSec());
	}

    private void FixedUpdate(){
        if (contentRect.anchoredPosition.x >= pansPos[0].x && !isScrolling || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x && !isScrolling)
            scrollRect.inertia = false;
        float nearestPos = float.MaxValue;
        for (int i = 0; i < skins.Length; i++){
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
            if (distance < nearestPos && turnOn){
                nearestPos = distance;
                selectedPanID = i;
                PlayerPrefs.SetInt("SelectedSkin", selectedPanID);
            }
            float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
            pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
            pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
            instPans[i].transform.localScale = pansScale[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if(scrollVelocity < 400 && !isScrolling)
            scrollRect.inertia = false;
        if(isScrolling || scrollVelocity > 400) 
            return;
            
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    IEnumerator WaitOneSec(){
        yield return new WaitForSeconds(1f);
        turnOn = true;
    }

    public void Scrolling(bool scroll){
        isScrolling = scroll;
        if(scroll) scrollRect.inertia = true;
    }
}