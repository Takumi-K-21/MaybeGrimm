using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SetImage : MonoBehaviour
{

    public Image image;
    private Sprite sprite;

    public static bool damageFlag = false;
    public static bool isGet = false;
    public static bool isCream = false;
    public static bool isFlag = false;

    private float GameTime = 0f;
    private bool isTime = false;

    private float GameTime2 = 0f;
    private bool isTime2 = false;

    void Update()
    {
        if (damageFlag && !isCream)
        {
            sprite = Resources.Load<Sprite>("icon_damage");
            image = this.GetComponent<Image>();
            image.sprite = sprite;
            isTime = true;
        }

        if (isGet && !isCream)
        {
            sprite = Resources.Load<Sprite>("icon_happy");
            image = this.GetComponent<Image>();
            image.sprite = sprite;
            isTime = true;
        }

        if (isCream && isFlag)
        {
            sprite = Resources.Load<Sprite>("IMG_0083");
            image = this.GetComponent<Image>();
            image.sprite = sprite;
            isTime2 = true;
            isFlag = false;
            GameTime2 = 0f;
        }

        if (isTime)
        {
            GameTime += Time.deltaTime;

            if(GameTime > 1)
            {
                GameTime = 0f;
                damageFlag = false;
                isGet = false;
                isTime = false;

                sprite = Resources.Load<Sprite>("icon_normal");
                image = this.GetComponent<Image>();
                image.sprite = sprite;

            }
        }

        if (isTime2)
        {
            GameTime2 += Time.deltaTime;

            if (GameTime2 >= 7.5f)
            {
                GameTime2 = 0f;
                damageFlag = false;
                isGet = false;
                isCream = false;
                isTime2 = false;

                sprite = Resources.Load<Sprite>("icon_normal");
                image = this.GetComponent<Image>();
                image.sprite = sprite;

            }
        }
    }
}