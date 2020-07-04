using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 东风谷早苗 : Boss
{
    public Player player;
    public override Game.GameDifficulty Difficulty { get; set; } = Game.GameDifficulty.Normal;
    ulong count = 0;
    public SpriteRenderer spriteRenderer;

    public override List<SpellCard> SpellCards { get; set; } = new List<SpellCard>();
    public override int HealPoint { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        /*
        SpellCards.Add(gameObject.AddComponent<物理_卫星运动>());
        ((物理_卫星运动)SpellCards[0]).player = player;
        */
        SpellCards.Add(gameObject.AddComponent<物理_卫星运动>());
        ((物理_卫星运动)SpellCards[0]).player = player;
        ((物理_卫星运动)SpellCards[0]).Difficulty = Difficulty;
    }

    void Update()
    {
        if (count % 8 == 0)
        {
            Texture2D texture = (Texture2D)Resources.Load($"贴图/敌机/东风谷早苗/东风谷早苗 ({((count / 8) % 4) + 1})");
            spriteRenderer.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }

        count++;
    }
}
