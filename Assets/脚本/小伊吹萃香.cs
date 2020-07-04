using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class 小伊吹萃香 : Creature
{
    public override int HealPoint { get; set; }

    public Game.GameDifficulty Difficulty { get; set; } = Game.GameDifficulty.Easy;

    public SpriteRenderer spriteRenderer;
    ulong count = 0;

    public Player player;

    public 百鬼夜行 百鬼夜行;

    public EventHandler<ClearBulletEventArgs> Clear;
    // Start is called before the first frame update
    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Texture2D texture = (Texture2D)Resources.Load($"贴图/敌机/伊吹萃香/伊吹萃香 ({((count / 8) % 4) + 1})");
        spriteRenderer.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        switch (Difficulty)
        {
            default:
            case Game.GameDifficulty.Easy:
                if (count % 120 <= 40 && count % 20 == 0 && Vector2.Distance(transform.position, player.transform.position) >= 2f) 
                {
                    GameObject bullet = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.米弹, 1);
                    bullet.transform.position = transform.position;
                    LineBullet lineBullet = bullet.AddComponent<LineBullet>();
                    Bullet.SetBulletBox(Bullet.BulletType.米弹, lineBullet);
                    lineBullet.SetFaceAngle(Bullet.GetAngle(transform.position, player.transform.position));
                    lineBullet.Speed = 0.05f;
                    lineBullet.player = player;
                    Clear += lineBullet.Clear;
                }
                break;
            case Game.GameDifficulty.Normal:
                if (count % 180 == 0 && Vector2.Distance(transform.position, player.transform.position) >= 2f) 
                {
                    for (float i = Bullet.GetAngle(transform.position, player.transform.position) - Mathf.PI / 12f; i < Bullet.GetAngle(transform.position, player.transform.position) + Mathf.PI / 12f + 0.001f; i += Mathf.PI / 12f) 
                    {
                        for (float s = 0.04f; s <= 0.06f; s+=0.02f)
                        {
                            GameObject bullet = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.米弹, 1);
                            bullet.transform.position = transform.position;
                            LineBullet lineBullet = bullet.AddComponent<LineBullet>();
                            Bullet.SetBulletBox(Bullet.BulletType.米弹, lineBullet);
                            lineBullet.SetFaceAngle(i);
                            lineBullet.Speed = s;
                            lineBullet.player = player;
                            Clear += lineBullet.Clear;
                        }
                    }
                }
                break;
            case Game.GameDifficulty.Hard:
                if (count % 180 == 0 && Vector2.Distance(transform.position, player.transform.position) >= 3f)
                {
                    for (float i = Bullet.GetAngle(transform.position, player.transform.position) - Mathf.PI / 6f; i < Bullet.GetAngle(transform.position, player.transform.position) + Mathf.PI / 6f + 0.001f; i += Mathf.PI / 12f)
                    {
                        GameObject bullet1 = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.米弹, 1);
                        bullet1.transform.position = transform.position;
                        LineBullet lineBullet1 = bullet1.AddComponent<LineBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.米弹, lineBullet1);
                        lineBullet1.SetFaceAngle(i);
                        lineBullet1.Speed = 0.03f;
                        lineBullet1.player = player;
                        Clear += lineBullet1.Clear;

                        GameObject bullet2 = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.米弹, 1);
                        bullet2.transform.position = transform.position;
                        LineBullet lineBullet2 = bullet2.AddComponent<LineBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.米弹, lineBullet2);
                        lineBullet2.SetFaceAngle(i);
                        lineBullet2.Speed = 0.02f;
                        lineBullet2.player = player;
                        Clear += lineBullet2.Clear;
                        Command command2 = bullet2.AddComponent<Command>();
                        command2.update = () =>
                        {
                            if (command2.count == 90) 
                            {
                                lineBullet2.Speed = 0.05f;
                            }
                        };
                    }
                }
                if (count % 240 == 0) 
                {
                    for (float i = 0.02f; i < 0.06f; i+=0.01f)
                    {
                        GameObject bullet = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.米弹, 1);
                        bullet.transform.position = transform.position;
                        LineBullet lineBullet = bullet.AddComponent<LineBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.米弹, lineBullet);
                        lineBullet.SetFaceAngle(-(Mathf.PI / 2));
                        lineBullet.Speed = i;
                        lineBullet.player = player;
                        Clear += lineBullet.Clear;
                    }
                }
                break;
            case Game.GameDifficulty.Lunatic:
                if ((count % 180 <= 60
                    && count % 20 == 0)
                    && Vector2.Distance(transform.position, player.transform.position) >= 4f)
                {
                    for (float i = Bullet.GetAngle(transform.position, player.transform.position); i <= Bullet.GetAngle(transform.position, player.transform.position) + Mathf.PI * 2f + 0.001f; i += Mathf.PI / 12f) 
                    {
                        GameObject bullet = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.米弹, 1);
                        bullet.transform.position = transform.position;
                        LineBullet lineBullet = bullet.AddComponent<LineBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.米弹, lineBullet);
                        lineBullet.SetFaceAngle(i);
                        lineBullet.Speed = 0.005f;
                        lineBullet.player = player;
                        Clear += lineBullet.Clear;
                        Command command = bullet.AddComponent<Command>();
                        command.Ints.Add((int)count % 180);
                        command.update = () =>
                        {
                            if (command.count == 180) 
                            {
                                lineBullet.Speed = command.Ints[0] / 20 * 0.03f;
                            }
                            if (command.count == 200) 
                            {
                                lineBullet.Speed = 0.03f;
                            }
                        };
                    }
                }
                if (count % 180 == 0)
                {
                    for (float i = 0.02f; i < 0.06f; i += 0.01f)
                    {
                        GameObject bullet = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.米弹, 1);
                        bullet.transform.position = transform.position;
                        LineBullet lineBullet = bullet.AddComponent<LineBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.米弹, lineBullet);
                        lineBullet.SetFaceAngle(-(Mathf.PI / 2));
                        lineBullet.Speed = i;
                        lineBullet.player = player;
                        Clear += lineBullet.Clear;
                    }
                }
                break;
        }
        if (onBeDestroy)
        {
            Destroy(gameObject);
        }
        count++;
    }

    bool onBeDestroy = false;

    public void clear(object sender,ClearBulletEventArgs e)
    {
        onBeDestroy = true;
        Clear?.Invoke(this, new ClearBulletEventArgs());
    }
}
