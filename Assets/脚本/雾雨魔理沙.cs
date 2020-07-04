using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 雾雨魔理沙 : Boss
{
    public override List<SpellCard> SpellCards { get; set; }
    public override int HealPoint { get; set; } = 0;
    public override Game.GameDifficulty Difficulty { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    IEnumerable Start()
    {
        yield return new WaitForSeconds(1f);
        SpellCard sc1 = gameObject.AddComponent<百鬼夜行>();
        sc1.enabled = false;
        SpellCards.Add(sc1);
        for (int i = -1; i < SpellCards.Count;)
        {
            if (HealPoint <= 0) 
            {
                if (i != -1) 
                {
                    Destroy(SpellCards[i]);
                }
                i++;
                SpellCards[i].enabled = true;
                HealPoint = SpellCards[i].HealPoint;
            }
        }
    }

    void Update()
    {
        
    }
}
