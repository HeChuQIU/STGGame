using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class SpellCard : Game
{
    public float time;
    protected abstract Action update { get; set; }
    public abstract int HealPoint { get; }

}
