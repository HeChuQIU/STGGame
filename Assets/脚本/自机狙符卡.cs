using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class 自机狙符卡 : SpellCard
{
    ulong count = 0;
    public Player player;
    float angle1 = 0f, angle2 = 0f;

    protected override Action update { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override int HealPoint => throw new NotImplementedException();

    public override event EventHandler<ClearBulletEventArgs> Clear;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count % 500 == 120)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 vector = new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(2f, 5f));
                GameObject bullet = Bullet.GetSpriteObjectFromFile($"贴图/子弹/bullet2_6_5");
                Command command = bullet.AddComponent<Command>();
                bullet.transform.position = vector;
                bullet.transform.localScale = new Vector3(2f, 2f, 2f);
                void update()
                {
                    angle1 = Bullet.GetAngle(player.transform.position - command.transform.position);
                    if (command.count >= 150 && command.count % 10 == 0)
                    {
                        for (float a = angle1 - (PI / 18); a <= angle1 + (PI / 18) + 0.01f; a += (PI / 18)) 
                        {
                            GameObject bullet1 = Bullet.GetSpriteObjectFromFile($"贴图/子弹/bullet1_2_{UnityEngine.Random.Range(1, 16)}");
                            LineBullet lineBullet = bullet1.AddComponent<LineBullet>();
                            Box box = bullet1.AddComponent<Box>();
                            lineBullet.Type = Box.BoxType.Round;
                            lineBullet.R = 0.12f;
                            bullet1.transform.position = vector;
                            bullet1.transform.localScale = new Vector3(2f, 2f, 2f);
                            lineBullet.player = player;
                            lineBullet.Miss += player.Miss;
                            lineBullet.SetFaceAngle(a);
                            lineBullet.Speed = 0.075f;
                            lineBullet.OriginVector = command.transform.position;
                        }
                    }
                    if (command.count == 500)
                    {
                        Destroy(command.gameObject);
                    }
                }
                command.update = update;
            }
        }

        if (count % 500 >= 380 && count % 12 == 0)
        {
            if (count % 1000 < 500)
            {
                angle2 += 0.025f;
            }
            else
            {
                angle2 -= 0.025f;
            }
            for (float i = 0; i < 2 * PI; i += (2 * PI) / 18)
            {
                GameObject bullet = Bullet.GetSpriteObjectFromFile($"贴图/子弹/bullet1_4_3");
                LineBullet lineBullet = bullet.AddComponent<LineBullet>();
                Clear += lineBullet.Clear;
                lineBullet.Type = Box.BoxType.Round;
                lineBullet.R = 0.20f;
                lineBullet.Miss += player.Miss;
                lineBullet.player = player;
                bullet.transform.localScale = new Vector3(2f, 2f, 2f);
                lineBullet.SetFaceAngle(i + angle2);
                lineBullet.Speed = 0.025f;               
            }
        }

        count++;
    }
}
