using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class 梦想封印 : SpellCard
{
    public override int HealPoint => 2500;
    public override event EventHandler<ClearBulletEventArgs> Clear;
    protected override Action update { get; set; }
    public Player player;
    ulong count = 0;
    public LineBullet ObjLinebullet;

    // Start is called before the first frame update
    void Start()
    {
        time = 33f;
        ObjLinebullet.RemoveWhenInvisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 6.5f)
        {
            transform.position = new Vector2(transform.position.x, -transform.position.y);
        }
        if (transform.position.y < -6.5f)
        {
            transform.position = new Vector2(transform.position.x, -transform.position.y);
        }
        if (transform.position.x > 4.5f)
        {
            transform.position = new Vector2(-transform.position.x, transform.position.y);
        }
        if (transform.position.x < -4.5f)
        {
            transform.position = new Vector2(-transform.position.x, transform.position.y);
        }

        if (time <= 0f)
        {
            time = 0f;
            Clear?.Invoke(this, new ClearBulletEventArgs());
            return;
        }
        else
        {
            time -= Time.deltaTime;
        }

        if (count % 600 == 50) 
        {
            ObjLinebullet.SetFaceAngle(Random.Range(0, 2 * Mathf.PI));
            ObjLinebullet.Speed = 0.05f;
        }
        count++;
    }

    void OnGUI()
    {
        if (time > 0f)
        {
            GUI.skin.label.normal.textColor = new Color(1f, 1f, 1f, 1f);
            GUI.skin.label.fontSize = 32;
            GUI.Label(new Rect((Screen.width - 120f) / 2f, 10, 120f, 40f), time.ToString("#00.00"));
            GUI.Label(new Rect(40f, 10f, 120f, 40f), $"Biu : {player.Biu}");
            GUI.Label(new Rect(Screen.width - 160f, Screen.height - 50f, 120f, 40f), Difficulty.ToString());
            //GUI.Label(new Rect(Screen.width - 320f, 10f, 300f, 40f), "物理[卫星运动]");
        }
    }
}
