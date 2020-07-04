using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 伊吹萃香 :Boss
{
    public override List<SpellCard> SpellCards { get; set; } = new List<SpellCard>();
    public override Game.GameDifficulty Difficulty { get; set; } = Game.GameDifficulty.Easy;
    public override int HealPoint { get; set; }

    public Player player;

    public SpriteRenderer spriteRenderer;
    ulong count = 0;

    void Start()
    {
        StartCoroutine(start());
    }

    IEnumerator start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        百鬼夜行 sc;
        sc = gameObject.AddComponent<百鬼夜行>();
        sc.enabled = false;
        sc.Difficulty = Difficulty;
        sc.player = player;
        SpellCards.Add(sc);

        foreach (SpellCard spellCard in SpellCards)
        {
            spellCard.enabled = true;
            HealPoint = spellCard.HealPoint;
            do
            {
                yield return new WaitForSeconds(0.5f);
            } while (!(HealPoint <= 0 || spellCard.time <= 0f));
            Destroy(spellCard);
        }
    }

    void Update()
    {
        if (count % 8 == 0)
        {
            Texture2D texture = (Texture2D)Resources.Load($"贴图/敌机/伊吹萃香/伊吹萃香 ({((count / 8) % 4) + 1})");
            spriteRenderer.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        count++;
    }
}
