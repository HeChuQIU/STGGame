using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class 藤原_滅罪寺院傷 : SpellCard
{
    public override int HealPoint => throw new NotImplementedException();

    protected override Action update { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Player player;

    public override event EventHandler<ClearBulletEventArgs> Clear;

    ulong count = 0;
    public LineBullet ObjLinebullet;

    // Start is called before the first frame update
    void Start()
    {
        time = 60f;
    }

    // Update is called once per frame
    void Update()
    {

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


        if (count % 180 == 0)
        {
            for (float i = 0; i < Mathf.PI + 0.001f; i += Mathf.PI)
            {
                GameObject 使魔 = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.中玉, 1);
                使魔.transform.position = transform.position;
                LineBullet lineBullet = 使魔.AddComponent<LineBullet>();
                Bullet.SetBulletBox(Bullet.BulletType.中玉, lineBullet);
                lineBullet.player = player;
                lineBullet.SetFaceAngle(i);
                lineBullet.Speed = 0.0213333333f;
                Clear += lineBullet.Clear;
                Command command = 使魔.AddComponent<Command>();
                command.update = () =>
                {
                    if (command.count % 15 == 0)
                    {
                        for (float j = -Mathf.PI / 2f; j < Mathf.PI / 2f + 0.001f; j += Mathf.PI)
                        {
                            GameObject bullet = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 7);
                            bullet.transform.position = 使魔.transform.position;
                            LineBullet lineBulleta = bullet.AddComponent<LineBullet>();
                            Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                            lineBulleta.player = player;
                            lineBulleta.SetFaceAngle(j);
                            lineBulleta.RemoveWhenInvisible = false;
                            lineBulleta.Speed = 0.07f;
                            Clear += lineBulleta.Clear;
                            lineBulleta.onBecameInvisible = () =>
                            {
                                
                                if (lineBulleta.Speed == 0.05f) 
                                {
                                    Destroy(lineBulleta.gameObject);
                                }
                                
                                lineBulleta.SetFaceAngle(lineBulleta.faceAngle + Mathf.PI);
                                lineBulleta.Speed = 0.05f;
                            };
                                                     
                        }
                    }
                };
            }
        }
        if (count % 180 == 60)
        {
            float angle = Random.Range(-Mathf.PI / 6f, Mathf.PI / 6f);
            if (transform.position.x < 0f)
            {
                ObjLinebullet.SetFaceAngle(angle);
            }
            if (transform.position.x >= 0f)
            {
                ObjLinebullet.SetFaceAngle(angle + Mathf.PI);
            }
            ObjLinebullet.Speed = Random.Range(0.01f, 0.02f);
        }
        if (count % 180 == 120)
        {
            ObjLinebullet.Speed = 0f;
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
            GUI.Label(new Rect(Screen.width - 160f, Screen.height - 50f, 120f, 40f), "Extra");
            //GUI.Label(new Rect(Screen.width - 320f, 10f, 300f, 40f), "物理[卫星运动]");
        }
    }
}
