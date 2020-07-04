using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 博丽灵梦 : Boss
{
    public Player player;
    public override List<SpellCard> SpellCards { get; set; } = new List<SpellCard>();
    public override Game.GameDifficulty Difficulty { get; set; } = Game.GameDifficulty.Normal;
    public override int HealPoint { get; set; }
    public ulong count = 0;
    public SpriteRenderer spriteRenderer;
    List<Sprite> sprites = new List<Sprite>();
    LineBullet lineBullet;
    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        lineBullet = gameObject.AddComponent<LineBullet>();
        lineBullet.SetFaceAngle(0);
        lineBullet.IsRotatToFace = false;

        for (int i = 1; i < 13; i++)
        {
            Texture2D texture = (Texture2D)Resources.Load($"贴图/敌机/博丽灵梦/博丽灵梦 ({i})");
            sprites.Add(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f)));
        }
        SpellCards.Add(gameObject.AddComponent<梦想封印>());    
        ((梦想封印)SpellCards[0]).player = player;
        ((梦想封印)SpellCards[0]).Difficulty = Difficulty;
        ((梦想封印)SpellCards[0]).ObjLinebullet = lineBullet;
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
