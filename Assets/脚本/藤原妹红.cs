using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 藤原妹红 : Boss
{
    public override List<SpellCard> SpellCards { get; set; } = new List<SpellCard>();
    public override int HealPoint { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public Player player;
    public override Game.GameDifficulty Difficulty { get; set; } = Game.GameDifficulty.Normal;
    public ulong count = 0;
    public SpriteRenderer spriteRenderer;
    List<Sprite> sprites = new List<Sprite>();
    LineBullet lineBullet;
    GameObject phoenix;
    // Start is called before the first frame update
    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        if (lineBullet == null) 
        {
            lineBullet = gameObject.AddComponent<LineBullet>();
            lineBullet.SetFaceAngle(0);
            lineBullet.IsRotatToFace = false;
        }
        for (int i = 1; i < 13; i++) 
        {
            Texture2D texture = (Texture2D)Resources.Load($"贴图/敌机/藤原妹红/藤原妹红 ({i})");
            sprites.Add(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f)));
        }
        if (phoenix == null) 
        {
            phoenix = Bullet.GetSpriteObjectFromFile("贴图/敌机/藤原妹红/boss24b", "phoenix");
            phoenix.transform.parent = gameObject.transform;
            phoenix.transform.localPosition = new Vector3(0, 0, 0.0001f);
            phoenix.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        SpellCards.Add(gameObject.AddComponent<藤原_滅罪寺院傷>());
        ((藤原_滅罪寺院傷)SpellCards[0]).player = player;
        ((藤原_滅罪寺院傷)SpellCards[0]).Difficulty = Difficulty;
        ((藤原_滅罪寺院傷)SpellCards[0]).ObjLinebullet = lineBullet;
    }

    public bool left, right;
    public int leftCount = 0, rightCount = 0;
    void Update()
    {
        spriteRenderer.sprite = sprites[(int)((count / 8) % 4)];
        if (lineBullet.Speed != 0f) 
        {
            if (Mathf.Cos(lineBullet.FaceAngle) <= 0)
            {
                left = true;
                right = false;
            }
            if (Mathf.Cos(lineBullet.FaceAngle) > 0)
            {
                left = false;
                right = true;
            }
        }
        else
        {
            left = false;
            right = false;
        }
        if (left && !right)
        {
            gameObject.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
            phoenix.transform.localPosition = new Vector3(0, 0, -0.0001f);
            if (leftCount <= 28)
            {
                rightCount = 0;
                spriteRenderer.sprite = sprites[((int)(leftCount / 8) % 4) + 3];
                leftCount++;
            }
            else
            {
                spriteRenderer.sprite = sprites[7];
            }
        }
        if (right && !left)
        {
            gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            phoenix.transform.localPosition = new Vector3(0, 0, 0.0001f);
            if (leftCount <= 28)
            {
                leftCount = 0;
                spriteRenderer.sprite = sprites[(int)(rightCount / 8) % 4 + 3];
                rightCount++;
            }
            else
            {
                spriteRenderer.sprite = sprites[7];
            }
        }

        count++;
    }   
}
