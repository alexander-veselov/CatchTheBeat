using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Score_Numbers_Instance : MonoBehaviour
{

	public uint fruit_counter = 0;
	public uint current_anim_counter = 0;

	string string_for_convertation;
	string string_for_convertation_curr;
	string s_for_position;
	Vector3 transf;

	Tweener myTweener;

	Score_Numbers anim;
	Score_Numbers main;

	Sprite[] sprites;

	SpriteRenderer[] nums_to_disp;
	SpriteRenderer[] back_nums;
	Vector3 pos;

	SpriteRenderer playerSprite;
	// Use this for initialization
	void Awake()
	{

		sprites = new Sprite[10];

		int counter = 0;

		while (counter != 10)
		{

			sprites[counter] = Resources.Load<Sprite>("Numbers_2/" + Convert.ToString(counter));

			counter++;
		}

	}
	void Start()
	{
		s_for_position = "0";
		transf = transform.localScale;

		pos = GameObject.Find("Player").transform.position;
		pos.y = 0f;

		anim = Instantiate(Resources.Load<Score_Numbers>("Prefabs/num_pref"), pos, transform.rotation);

		main = Instantiate(Resources.Load<Score_Numbers>("Prefabs/num_pref"), pos, transform.rotation);

		nums_to_disp = anim.GetComponentsInChildren<SpriteRenderer>();
		back_nums = main.GetComponentsInChildren<SpriteRenderer>();

		playerSprite = GameObject.Find ("Player").GetComponentInChildren<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update()
	{
		if (GameObject.Find ("Player") != null) {
			pos = GameObject.Find ("Player").transform.position;
			pos.y = 0f;

//		if (fruit_counter > 0)
//		{
			if (s_for_position.Length == 1) {
				pos.x -= playerSprite.size.x / 3.0f;

			}
			if (s_for_position.Length == 2) {
				pos.x -= playerSprite.size.x / 2.0f;

			}
			if (s_for_position.Length == 3) {
				
				pos.x -= playerSprite.size.x / 1.51f;
			}

//		}

			anim.transform.position = pos;
			main.transform.position = pos;


			if (nums_to_disp != null) {

				for (int i = 0; i < nums_to_disp.Length; i++) {

					pos.x += 1f;

					nums_to_disp [i].transform.position = pos;

					back_nums [i].transform.position = pos;
				}

			}
		}
	}



	public void Boom()
	{

		doInterruptAnimation (ref nums_to_disp,ref back_nums);

		StartCoroutine (Animation(string_for_convertation,nums_to_disp,back_nums));


		foreach (SpriteRenderer spr in back_nums)
		{

			spr.DOFade(0, 10f);

		}


	}


	IEnumerator Animation(string string_for_convertation, SpriteRenderer[] nums_to_disp, SpriteRenderer[] back_nums) {

		int i = 0;


		current_anim_counter = fruit_counter - 1;

		string_for_convertation = Convert.ToString(fruit_counter);

		string_for_convertation_curr = Convert.ToString (current_anim_counter);

		s_for_position = string_for_convertation;

		for (i = 0; i < string_for_convertation_curr.Length; i++) {


			back_nums [i].sprite = sprites [Convert.ToInt32 (Convert.ToString (string_for_convertation_curr [i]))];


			if (string_for_convertation.Length > string_for_convertation_curr.Length) {

				for (int g = 0; g < string_for_convertation.Length; g++) {

					nums_to_disp [g].sprite = sprites [Convert.ToInt32 (Convert.ToString (string_for_convertation [g]))];
					nums_to_disp [g].transform.DOScale (transform.localScale + new Vector3 (1.7f, 1.7f, 1.7f), 0.3f).SetEase (Ease.OutFlash);
					nums_to_disp [g].DOFade (0, 0.6f).SetEase (Ease.OutFlash);

				}

			} else {

				nums_to_disp [i].sprite = sprites [Convert.ToInt32 (Convert.ToString (string_for_convertation [i]))];
				nums_to_disp [i].transform.DOScale (transform.localScale + new Vector3 (1.7f, 1.7f, 1.7f), 0.3f).SetEase (Ease.OutFlash);
				nums_to_disp [i].DOFade (0, 0.6f).SetEase (Ease.OutFlash);

			}


			myTweener = back_nums [i].transform.DOPunchScale (new Vector3 (0.8f, 0.8f, 0.8f), 0.7f, 1, 0.5f);

		}

		yield return myTweener.WaitForCompletion ();

		for (int ko = 0; ko < string_for_convertation.Length; ko++) {

			back_nums [ko].sprite = sprites [Convert.ToInt32 (Convert.ToString (string_for_convertation [ko]))];
		}


	}


	public void docleanCombo()
	{
		s_for_position = "0";
		doInterruptAnimation (ref nums_to_disp,ref back_nums);

		fruit_counter = 0;

		nums_to_disp[0].sprite = sprites[0];
		back_nums[0].sprite = sprites[0];


		nums_to_disp[0].transform.DOScale(transform.localScale + new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutFlash);

		nums_to_disp[0].DOFade(0, 0.6f).SetEase(Ease.OutFlash);

		back_nums[0].transform.DOPunchScale(new Vector3(0.8f, 0.8f, 0.8f), 0.7f, 1, 0.5f);

		for (int i = 1; i < nums_to_disp.Length; i++) {

			nums_to_disp[i].sprite = null;
			back_nums[i].sprite = null;


		}

		back_nums [0].DOFade (0,10f);

	}

	public void doInterruptAnimation(ref SpriteRenderer[] nums_to_disp,ref SpriteRenderer[] back_nums) {


		for (int i = 0; i < nums_to_disp.Length; i++)
		{

			nums_to_disp[i].DOKill();
			nums_to_disp[i].transform.localScale = transf;
			nums_to_disp[i].color = new Color(nums_to_disp[i].color.r, nums_to_disp[i].color.g, nums_to_disp[i].color.b, 1);

			back_nums[i].transform.DOKill();

			back_nums[i].transform.localScale = transf;

			back_nums[i].DOKill();

			back_nums[i].color = new Color(back_nums[i].color.r, back_nums[i].color.g, back_nums[i].color.b, 1);


		}


	}
}