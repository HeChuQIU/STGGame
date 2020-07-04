using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public virtual event EventHandler<ClearBulletEventArgs> Clear;
    public enum GameDifficulty
    {
        Easy,
        Normal,
        Hard,
        Lunatic
    }

    public GameDifficulty Difficulty { get; set; } = GameDifficulty.Normal;
      
    public static Player Player { get; }
}
public class ClearBulletEventArgs:EventArgs { }
