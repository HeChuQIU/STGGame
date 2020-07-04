using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : Creature
{
    public abstract List<SpellCard> SpellCards { get; set; }
    public abstract Game.GameDifficulty Difficulty { get; set; }
}
