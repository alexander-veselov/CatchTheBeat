using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BigRankLetter : MonoBehaviour {

	SpriteRenderer sprite;

	Vector3 p;

	public  enum Colors {

	    gold,
		green,
		blue,
		purple,
		red,

	}

	public static Colors accuracyColor;
	// Use this for initialization
	void Awake () {

		sprite = GetComponent<SpriteRenderer> ();

		p = Camera.main.ViewportToWorldPoint (new Vector2(1, 1));

//		sprite.sprite = Resources.Load<Sprite> ("EndGameMaterials/B");
		if (finalStatistics.accuracy == 100.0f) {

			sprite.sprite = Resources.Load<Sprite> ("EndGameMaterials/SS");
			accuracyColor = Colors.gold;


		} else {
			if (finalStatistics.accuracy >= 95.0f) {

				sprite.sprite = Resources.Load<Sprite> ("EndGameMaterials/S");
				accuracyColor = Colors.gold;

			} else {
				if (finalStatistics.accuracy >= 90.0f) {

					sprite.sprite = Resources.Load<Sprite> ("EndGameMaterials/A");
					accuracyColor = Colors.green;

				} else {
				
					if (finalStatistics.accuracy >= 80.0f) {

						sprite.sprite = Resources.Load<Sprite> ("EndGameMaterials/B");
						accuracyColor = Colors.blue;

					} else {

						if (finalStatistics.accuracy >= 70.0f) {
					
							sprite.sprite = Resources.Load<Sprite> ("EndGameMaterials/C");
							accuracyColor = Colors.purple;
						} else {
						
						
							if (finalStatistics.accuracy >= 60.0f) {
								sprite.sprite = Resources.Load<Sprite> ("EndGameMaterials/D");
								accuracyColor = Colors.red;
							
							}
						
						}
					
					}

				
				}
			}
		}


		sprite.transform.position = new Vector3 (p.x / 1.5f, p.y / 9f, -3f);
		sprite.transform.localScale = new Vector2 (p.x / 18f, p.x  / 18f);

		sprite.transform.DOShakePosition (40f,0.15f,9,90,false,false);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
