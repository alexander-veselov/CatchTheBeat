using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Score_Numbers_Instance : MonoBehaviour
{

    public uint fruit_counter = 0;

    string string_for_convertation;
    Vector3 transf;


    Score_Numbers anim;
    Score_Numbers main;

    Sprite[] sprites;

    SpriteRenderer[] nums_to_disp;
    SpriteRenderer[] back_nums;
    Vector3 pos;
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

        transf = transform.localScale;

        pos = GameObject.Find("Player").transform.position;
        pos.y = 0f;

        anim = Instantiate(Resources.Load<Score_Numbers>("Prefabs/num_pref"), pos, transform.rotation);

        main = Instantiate(Resources.Load<Score_Numbers>("Prefabs/num_pref"), pos, transform.rotation);

        nums_to_disp = anim.GetComponentsInChildren<SpriteRenderer>();
        back_nums = main.GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        pos = GameObject.Find("Player").transform.position;
        pos.y = 0f;

        if (string_for_convertation != null)
        {
            if (string_for_convertation.Length == 1)
            {
                pos.x -= 1.0f;

            }
            if (string_for_convertation.Length == 2)
            {
                pos.x -= 1.57f;

            }
            if (string_for_convertation.Length == 3)
            {
                pos.x -= 2.0f;

            }
        }

        anim.transform.position = pos;
        main.transform.position = pos;


        if (nums_to_disp != null)
        {

            for (int i = 0; i < nums_to_disp.Length; i++)
            {

                pos.x += 1f;

                nums_to_disp[i].transform.position = pos;

                back_nums[i].transform.position = pos;
            }

        }
    }



    public void Boom()
    {

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



        if (fruit_counter > 0)
        {

            int i = 0;

            string_for_convertation = Convert.ToString(fruit_counter);



            while (i != string_for_convertation.Length)
            {


                nums_to_disp[i].sprite = sprites[Convert.ToInt32(Convert.ToString(string_for_convertation[i]))];
                back_nums[i].sprite = sprites[Convert.ToInt32(Convert.ToString(string_for_convertation[i]))];


                nums_to_disp[i].transform.DOScale(transform.localScale + new Vector3(1.9f, 1.9f, 1.9f), 0.3f).SetEase(Ease.OutFlash);

                nums_to_disp[i].DOFade(0, 0.6f).SetEase(Ease.OutFlash);

                back_nums[i].transform.DOPunchScale(new Vector3(1, 1, 1), 0.7f, 1, 0.5f);

                i++;
            }

            foreach (SpriteRenderer spr in back_nums)
            {

                spr.DOFade(0, 10f);

            }


        }


    }

    public void docleanCombo()
    {


        fruit_counter = 0;
        //Instantiate(Resources.Load<Score_Numbers>("Prefabs/num_pref"), pos, transform.rotation);

        //Instantiate(Resources.Load<Score_Numbers>("Prefabs/num_pref"), pos, transform.rotation);
        for (int i = 0; i < nums_to_disp.Length; i++)
        {

            nums_to_disp[i].sprite = null;

            back_nums[i].sprite = null;

        }

    }
}