using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STGBaselib;

public class Player : Box
{
    // Start is called before the first frame update
    static float speed = 2.5f / 50f;
    bool slow = false;
    public GameObject Point;
    ulong count = 0;
    List<Sprite> sprites = new List<Sprite>();
    SpriteRenderer sprite;
    bool left, right;
    int leftCount = 0,rightCount = 0;
    public AudioSource audioSource;
    public int Biu = 0;
    void Start()
    {
        Type = Box.BoxType.Round;
        R = 0.1f;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        for (int i = 1; i < 25; i++)
        {
            Texture2D texture = (Texture2D)Resources.Load($"贴图/自机/雾雨魔理沙/雾雨魔理沙 ({i})");
            sprites.Add(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f)));
        }
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector = transform.position ;
        if (Input.GetKey(KeyCode.Z) && count % 3 == 0)  
        {
            for (float i = -0.25f; i <= 0.25f; i += 0.5f)
            {
                GameObject bullet = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 3);
                bullet.transform.position = gameObject.transform.position + new Vector3(i, -0.25f, 0);
                LineBullet lineBullet = bullet.AddComponent<LineBullet>();
                lineBullet.OriginVector = gameObject.transform.position + new Vector3(i, -0.25f, 0);
                lineBullet.SetFaceAngle(Mathf.PI / 2f);
                lineBullet.Speed = 0.5f;
                lineBullet.transform.localScale = new Vector3(2, 2, 2);
            }
        }
        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y <= 4.5f) 
        {
            vector.y += speed;
        }
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y >= -4.5f) 
        {
            vector.y -= speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= -6.5f) 
        {
            vector.x -= speed;
            left = true;
            right = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) 
        {
            left = false;
        }
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= 6.5f)
        {
            vector.x += speed;
            right = true;
            left = false;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) 
        {
            right = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            slow = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            slow = false;
        }
        if (slow) 
        {
            speed = 1.5f/50f;
            Point.SetActive(true);
        }
        else
        {
            speed = 2.5f/50f;
            Point.SetActive(false);
        }
        transform.position = vector;
        sprite.sprite = sprites[(int)((count / 8) % 8)];
        if (left && !right) 
        {
            rightCount = 0;
            if (leftCount < 24) 
            {
                sprite.sprite = sprites[((int)(leftCount / 6) % 4) + 8];
            }
            else
            {
                sprite.sprite = sprites[((int)(leftCount - 4) / 8) % 4 + 12];
            }
            leftCount++;
        }
        if (right && !left) 
        {
            leftCount = 0;
            if (rightCount < 24)
            {
                sprite.sprite = sprites[(int)(rightCount / 6) % 8 + 16];
            }
            else
            {
                sprite.sprite = sprites[(int)((rightCount - 4) / 8) % 4 + 20];
            }
            rightCount++;
        }
        count++;
    }

    public void Miss(object sender,MissEventArgs e)
    {
        AudioWAV("se_pldead00");
        Biu += 1;
        Debug.Log("Biu~");
    }

    public void AudioWAV(string name)
    {
        audioSource.clip = Resources.Load<AudioClip>($"音效/{name}");
        audioSource.Play();
    }
}
