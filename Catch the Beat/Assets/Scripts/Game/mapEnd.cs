using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;



public class mapEnd : MonoBehaviour {
    Image[] panel;
	Color32[] colors;
	CanvasRenderer[] results;
	TextMeshProUGUI[] text;
	SpriteRenderer[] blueAdditionalSprites;
	public SpriteRenderer[] fruits;
	Tweener tw;
	void Start () {
        GameObject.Find("sounds").GetComponent<sounds>().MapEnd();
        AudioLoad.audioSource.time = MapsLoad.PreviewTime / 1000.0f;
        if (!MapsLoad.AD) GameObject.Find("mapScript").GetComponent<records>().setRecord(MenuLoad.map, finalStatistics.finalScore, finalStatistics.accuracy);

        fruits = GameObject.Find("FSprites").GetComponentsInChildren<SpriteRenderer> ();

		colors = new Color32[4];
		colors[0] = new Color32(158, 47, 255, 255);
		colors[1] = new Color32(255, 76, 185, 255);
		colors[2] = new Color32(36, 166, 101, 255);
		colors[3] = new Color32(46, 132, 164, 255);

		panel = GetComponentsInChildren<Image>();


		setFruit ();
		setFruitPosition ();

		blueAdditionalSprites = GetComponentsInChildren<SpriteRenderer>();

		text = GetComponentsInChildren<TextMeshProUGUI>();

		results = GetComponentsInChildren<CanvasRenderer> ();

       
        Vector2 max = new Vector2(Screen.width, Screen.height);
        max.x = max.y;
        panel[1].transform.localScale = max/450f;
        max.y = Screen.height/ 600f;
        panel[0].transform.localScale = max;
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        max.y /=1.6f;
        max.x *=(-1.01f);
        panel[1].transform.position = max;
        max = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        panel[0].transform.position = max;
        Vector3 p = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        p.z = 0;
        panel[2].transform.position = p;
        p = new Vector3(Screen.width, Screen.height,1);
        p.y = Screen.height / 600f;
        p.x = p.y;
        panel[2].transform.localScale = p;


		p = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
////////////////////////score
		results [3].transform.position = new Vector3(p.x+0.7f, max.y / 2.4f,0);
		text[0].transform.localScale = new Vector2(panel[1].transform.localScale.x ,panel[1].transform.localScale.x);
		text [0].text = Convert.ToString(finalStatistics.finalScore);
/////////////////////////////combo
		p = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		text[1].transform.localScale = new Vector2(panel[1].transform.localScale.x /1.3f ,panel[1].transform.localScale.x/1.3f);
		text[1].text = Convert.ToString(finalStatistics.maxCombo);

		if (text[1].text.Length == 3) {
			results [4].transform.position = new Vector3 (p.x - 1.023f, p.y + 1.7f, 0);
		}
		if (text[1].text.Length == 1) {
			results [4].transform.position = new Vector3 (p.x - 1.45f, p.y + 1.7f, 0);
		}
		if (text[1].text.Length == 2) {
			results [4].transform.position = new Vector3 (p.x - 1.15f, p.y + 1.7f, 0);
		}
		if (text[1].text.Length == 4) {
			results [4].transform.position = new Vector3 (p.x - 0.92f, p.y + 1.7f, 0);
		}

/////////////////////////////

//////////accuracy
		text[2].transform.localScale = new Vector2(panel[1].transform.localScale.x /1.3f ,panel[1].transform.localScale.x/1.3f);
		text[2].text = Convert.ToString(Math.Round(finalStatistics.accuracy,2));
		switch(BigRankLetter.accuracyColor) {

		case BigRankLetter.Colors.gold:
			text [2].CrossFadeColor (Color.yellow, 4.5f, false, false);
			break;
		case BigRankLetter.Colors.green:
			text [2].CrossFadeColor (Color.green, 4.5f, false, false);
			break;
		case BigRankLetter.Colors.blue:
			text [2].CrossFadeColor (Color.blue, 4.5f, false, false);
			break;
		case BigRankLetter.Colors.purple:
			text [2].CrossFadeColor (Color.magenta, 4.5f, false, false);
			break;
		case BigRankLetter.Colors.red:
			text [2].CrossFadeColor (Color.red, 4.5f, false, false);
			break;

		}

		if (text[2].text.Length == 5) {
			results [5].transform.position = new Vector3 (p.x + 3.85f, p.y + 1.7f, 0);
		}
		if (text[2].text.Length == 4) {
			results [5].transform.position = new Vector3 (p.x + 3.72f, p.y + 1.7f, 0);
		}
		if (text[2].text.Length == 2) {
			results [5].transform.position = new Vector3 (p.x + 3.4f, p.y + 1.7f, 0);
		}
		if (text[2].text.Length == 1) {
			results [5].transform.position = new Vector3 (p.x + 3.23f, p.y + 1.7f, 0);
		}
		if (text[2].text.Length == 3) {
			results [5].transform.position = new Vector3 (p.x + 3.62f, p.y + 1.7f, 0);
		}
		if (text[2].text.Length == 6) {
			results [5].transform.position = new Vector3 (p.x + 4.0f, p.y + 1.7f, 0);
		}

////////////////big fruit
	
		p = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		text[3].transform.localScale = new Vector2(panel[1].transform.localScale.x /1.3f ,panel[1].transform.localScale.x/1.3f);
		text[3].text = Convert.ToString(finalStatistics.big_fruits_counter);

		if (text[3].text.Length == 2) {
			results[7].transform.position = new Vector3 (fruits[0].transform.position.x - 0.9f, fruits[0].transform.position.y-0.27f, 0);
		}
		if (text[3].text.Length == 1) {
			results[7].transform.position = new Vector3 (fruits[0].transform.position.x - 1.1f, fruits[0].transform.position.y-0.27f, 0);
		}
		if (text[3].text.Length == 3) {
			results[7].transform.position = new Vector3 (fruits[0].transform.position.x - 0.375f, fruits[0].transform.position.y-0.27f, 0);
		}
		if (text[3].text.Length == 4) {
			results[7].transform.position = new Vector3 (fruits[0].transform.position.x + 0.25f, fruits[0].transform.position.y-0.27f, 0);
		}
////////////////medium fruit
		text[4].transform.localScale = new Vector2(panel[1].transform.localScale.x /1.3f ,panel[1].transform.localScale.x/1.3f);
		text[4].text = Convert.ToString(finalStatistics.medium_fruits_counter);

		if (text[4].text.Length == 2) {
			results[8].transform.position = new Vector3 (fruits[2].transform.position.x - 0.9f, fruits[2].transform.position.y-0.27f, 0);
		}
		if (text[4].text.Length == 1) {
			results[8].transform.position = new Vector3 (fruits[2].transform.position.x - 1.1f, fruits[2].transform.position.y-0.27f, 0);
		}
		if (text[4].text.Length == 3) {
			results[8].transform.position = new Vector3 (fruits[2].transform.position.x - 0.375f, fruits[2].transform.position.y-0.27f, 0);
		}
////////////////small fruit
		text[5].transform.localScale = new Vector2(panel[1].transform.localScale.x /1.3f ,panel[1].transform.localScale.x/1.3f);
		text[5].text = Convert.ToString(finalStatistics.small_fruits_counter);

		if (text[5].text.Length == 2) {
			results[9].transform.position = new Vector3 (fruits[4].transform.position.x - 0.9f, fruits[4].transform.position.y-0.27f, 0);
		}
		if (text[5].text.Length == 1) {
			results[9].transform.position = new Vector3 (fruits[4].transform.position.x - 1.1f, fruits[4].transform.position.y-0.27f, 0);
		}
		if (text[5].text.Length == 3) {
			results[9].transform.position = new Vector3 (fruits[4].transform.position.x - 0.375f, fruits[4].transform.position.y-0.27f, 0);
		}


///////////////missed fruits

		text[6].transform.localScale = new Vector2(panel[1].transform.localScale.x /1.3f ,panel[1].transform.localScale.x/1.3f);
		text[6].text = Convert.ToString(finalStatistics.missed_fruits);

		if (text[6].text.Length == 2) {
			results[10].transform.position = new Vector3 (fruits[6].transform.position.x - 0.9f, fruits[6].transform.position.y-0.27f, 0);
		}
		if (text[6].text.Length == 1) {
			results[10].transform.position = new Vector3 (fruits[6].transform.position.x - 1.1f, fruits[6].transform.position.y-0.27f, 0);
		}
		if (text[6].text.Length == 3) {
			results[10].transform.position = new Vector3 (fruits[6].transform.position.x - 0.375f, fruits[6].transform.position.y-0.27f, 0);
		}

///////////  

		setPercentAndXSpritesPositionAndScale();

		setButtonPositionAndScale ();


		setSongNamePositionAndScale ();

		StartCoroutine (Animation ());


		bgLoad();


    }

	public void setSongNamePositionAndScale() {



		Vector3 p = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));

		text [7].text = MenuSubItem.song_name;
		text[7].transform.localScale = new Vector2(panel[1].transform.localScale.x /1.3f ,panel[1].transform.localScale.x/1.3f);
		results[11].transform.position = new Vector3 (p.x + 2.0f, p.y-1.0f, 0);



	}
	public void setFruit() {
		
		string fruit_path = "";

	
		for(int i=0; i<fruits.Length; i+=2)
        {
            if (i == 3 || i == 5) continue;
			switch (UnityEngine.Random.Range (1, 5)) {
			case 1:
				fruit_path = "apple";
				break;
			case 2:
				fruit_path = "grape";
				break;
			case 3:
				fruit_path = "orange";
				break;
			case 4:
				fruit_path = "pear";
				break;
			}

			fruits[i].sprite = Resources.Load<Sprite> ("EndGameMaterials/" + fruit_path);
            fruits[i].color = colors[UnityEngine.Random.Range(0, 4)];
        }




	}
	public void setFruitPosition() {
		
		Vector3 p = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		fruits[0].transform.position = new Vector3 (p.x + 0.7f, p.y + 6.1f, 0);
		fruits[0].transform.localScale = new Vector2 (panel [1].transform.localScale.x/1.1f , panel [1].transform.localScale.x/1.1f);
        fruits[1].transform.position = new Vector3(p.x + 0.7f, p.y + 6.1f, 0);
        fruits[1].transform.localScale = new Vector2(panel[1].transform.localScale.x / 2.2f, panel[1].transform.localScale.x / 2.2f);

        fruits[2].transform.position = new Vector3 (p.x + 0.7f, p.y + 4.79f, 0);
		fruits[2].transform.localScale = new Vector2 (panel [1].transform.localScale.x/1.9f , panel [1].transform.localScale.x/1.9f);
        //fruits[3].transform.position = new Vector3(p.x + 0.7f, p.y + 4.79f, 0);
        //fruits[3].transform.localScale = new Vector2(panel[1].transform.localScale.x / 1.9f, panel[1].transform.localScale.x / 1.9f);

        fruits[4].transform.position = new Vector3 (p.x + 0.7f, p.y + 3.7f, 0);
		fruits[4].transform.localScale = new Vector2 (panel [1].transform.localScale.x/3.65f , panel [1].transform.localScale.x/3.65f);
        //fruits[5].transform.position = new Vector3(p.x + 0.7f, p.y + 3.7f, 0);
        //fruits[5].transform.localScale = new Vector2(panel[1].transform.localScale.x / 3.65f, panel[1].transform.localScale.x / 3.65f);

        fruits[6].transform.position = new Vector3 (p.x + 5.7f, p.y + 6.1f, 0);
		fruits[6].transform.localScale = new Vector2 (panel [1].transform.localScale.x/1.3f , panel [1].transform.localScale.x/1.3f);
        fruits[7].transform.position = new Vector3(p.x + 5.7f, p.y + 6.1f, 0);
        fruits[7].transform.localScale = new Vector2(panel[1].transform.localScale.x / 2.6f, panel[1].transform.localScale.x / 2.6f);
    }
    void bgLoad()
    {
        string s = MapsLoad.currentMap;
        SpriteRenderer image = GameObject.Find("BG").GetComponent<SpriteRenderer>();
        String path = Application.persistentDataPath + '/' + MenuLoad.folder + '/' + s;
        WWW www = new WWW("file://" + path);
        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        www.LoadImageIntoTexture(tex);
        image.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        var width = image.sprite.bounds.size.x;
        var height = image.sprite.bounds.size.y;
        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        image.transform.localScale = new Vector2((float)worldScreenWidth / width, (float)worldScreenHeight / height);
        image.color = new Color(1, 1, 1, 0.5f);
    }

	void setButtonPositionAndScale() {
		
		Vector3 p = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
	
		panel [3].transform.position = new Vector2 (p.x - 3.1f,p.y + 2.0f);
		GameObject.Find ("retryButton").GetComponent<RectTransform> ().sizeDelta =  new Vector2 (panel [1].transform.localScale.x*130f , panel [1].transform.localScale.x *43f);

	
	}

	public void setPercentAndXSpritesPositionAndScale() {
		
		blueAdditionalSprites[0].transform.position = new Vector2(results[5].transform.position.x + 2.7f,results[5].transform.position.y+0.28f); //combo
		blueAdditionalSprites[0].transform.localScale = new Vector2(panel[1].transform.localScale.x * 57f  ,panel[1].transform.localScale.x * 57f);


		blueAdditionalSprites[1].transform.position = new Vector2(results[4].transform.position.x + 2.7f ,results[4].transform.position.y+0.28f); //percent
		blueAdditionalSprites[1].transform.localScale = new Vector2(panel[1].transform.localScale.x * 59f  ,panel[1].transform.localScale.x * 59f);


		blueAdditionalSprites[2].transform.position = new Vector2(results[7].transform.position.x + 2.7f ,results[7].transform.position.y+0.28f); //big fruit
		blueAdditionalSprites[2].transform.localScale = new Vector2(panel[1].transform.localScale.x * 59f  ,panel[1].transform.localScale.x * 59f);

		blueAdditionalSprites[3].transform.position = new Vector2(results[8].transform.position.x + 2.7f ,results[8].transform.position.y+0.28f); //medium fruit
		blueAdditionalSprites[3].transform.localScale = new Vector2(panel[1].transform.localScale.x * 59f  ,panel[1].transform.localScale.x * 59f);

		blueAdditionalSprites[4].transform.position = new Vector2(results[9].transform.position.x + 2.7f ,results[9].transform.position.y+0.28f); //small fruit
		blueAdditionalSprites[4].transform.localScale = new Vector2(panel[1].transform.localScale.x * 59f  ,panel[1].transform.localScale.x * 59f);

		blueAdditionalSprites[5].transform.position = new Vector2(results[10].transform.position.x + 2.7f ,results[10].transform.position.y+0.28f); //small fruit
		blueAdditionalSprites[5].transform.localScale = new Vector2(panel[1].transform.localScale.x * 59f  ,panel[1].transform.localScale.x * 59f);



	}

	IEnumerator Animation() {
		
		Vector3 for_scale_score = text [0].transform.localScale;
		Vector3 for_scale = text[1].transform.localScale;
		Color for_alpha = text [1].color;

		for(int i = 0; i < text.Length-1; i++ ){

			text[i].transform.localScale = text[i].transform.localScale * 13.0f;
			text[i].color = new Color(text[i].color.r,text[i].color.g,text[i].color.b,0);
		
		}

		foreach (SpriteRenderer spr in fruits) {
		
			tw = spr.transform.DOPunchScale (new Vector3 (-0.8f, -0.8f, -0.8f), 1.2f, 3, 1f).SetEase(Ease.OutExpo);
		
		}



		yield return tw.WaitForCompletion (); 

		text [0].transform.DOScale (for_scale_score,1f);
		text[0].DOFade(1f,5f).SetEase(Ease.OutQuart);

		for(int i = 1; i < text.Length-1; i++ ){

			text[i].transform.DOScale(for_scale,1f);
			tw = text[i].DOFade(1f,5f).SetEase(Ease.OutQuart);

		}
		yield return tw.WaitForCompletion ();
	
		GameObject.Find ("retryButton").GetComponent<RectTransform> ().transform.DORotate (new Vector3 (0,0,720.0f), 3f,RotateMode.FastBeyond360);
	
	}

	public void retryButton_On_Click() {

		health.restart ();
        AudioLoad.fromBegin = true;
        AudioLoad.audioSource.time = 0;
        GameObject.Find("mapScript").GetComponent<MapsLoad>().fileParse();
        GameObject.Find("mapScript").GetComponent<MapsLoad>().bitLoad();
        GameObject.Find("mapScript").GetComponent<MapsLoad>().settings();
        GameObject.Find("sounds").GetComponent<sounds>().MenuHit();
        SceneManager.LoadScene ("scene");

    }
    void Update () {
		
	}
}
