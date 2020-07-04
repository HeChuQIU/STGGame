using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TestSpellCard : MonoBehaviour
{
    ulong count = 0;
    float angle1 = 0f, angle2 = 0f;
    Vector3 Vector = new Vector3(0, 0);
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        if (Player == null) 
        {
            Player = GameObject.FindGameObjectWithTag("player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        //自机狙
        angle1 = Bullet.GetAngle(Player.transform.position);
        if (count % 10 < 4) 
        {
            for (float i = angle1 - Mathf.PI; i <= angle1 + Mathf.PI + 0.01f; i += 2 * Mathf.PI / 9f)  
            {
                GameObject bullet = new GameObject("bullet");
                SpriteRenderer sprite = bullet.AddComponent<SpriteRenderer>();
                LineBullet bulletBehaviour = bullet.AddComponent<LineBullet>();
                Texture2D texture = (Texture2D)Resources.Load($"贴图/子弹/bullet1_2_11");
                bullet.transform.localScale = new Vector3(2f, 2f, 2f);
                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                bulletBehaviour.SetFaceAngle(i);
                bulletBehaviour.Speed = 0.05f;
                bulletBehaviour.IsRotatToFace = true;
                bullet.SetActive(true);
            }
        }

        if (count % 100 <= 30)  
        {
            if (count % 200 < 100) 
            {
                angle2 -= 0.025f;
            }
            else
            {
                angle2 += 0.025f;
            }
            for (float i = 0; i > -2 * Mathf.PI; i -= (2 * Mathf.PI) / 10f)
            {
                GameObject bullet = new GameObject("bullet");
                SpriteRenderer sprite = bullet.AddComponent<SpriteRenderer>();
                LineBullet bulletBehaviour = bullet.AddComponent<LineBullet>();
                Texture2D texture = (Texture2D)Resources.Load($"贴图/子弹/bullet1_8_3");
                bullet.transform.localScale = new Vector3(2f, 2f, 2f);
                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                bulletBehaviour.OriginVector = Vector;
                bulletBehaviour.Speed = 0.02f;
                bulletBehaviour.SetFaceAngle(i + angle2);
                bulletBehaviour.IsRotatToFace = true;
                bullet.SetActive(true);
            }
        }
    }

}
 