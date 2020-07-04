using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 物理_卫星运动 : SpellCard
{
    ulong count = 0;
    const float m1 = 10f, m2 = 0.5f;
    public Player player;

    protected override Action update { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override int HealPoint => 4500;

    public override event EventHandler<ClearBulletEventArgs> Clear;

    private void Start()
    {
        time = 43f;
    }

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

        switch (Difficulty)
        {
            case Game.GameDifficulty.Easy:
                if (count % 1000 == 0)
                {
                    for (float i = 0; i < 2 * Mathf.PI; i += 2 * Mathf.PI / 5)
                    {
                        GameObject bullet = new GameObject();
                        bullet.transform.position = transform.position;
                        bullet.transform.localScale = new Vector3(2f, 2f, 2f);
                        SpriteRenderer sprite = bullet.AddComponent<SpriteRenderer>();
                        Texture2D texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 7);
                        sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                        LineBullet lineBullet = bullet.AddComponent<LineBullet>();
                        lineBullet.SetFaceAngle(i);
                        lineBullet.Speed = 0.09f;
                        lineBullet.RemoveWhenInvisible = false;
                        Bullet.SetBulletBox(Bullet.BulletType.中玉, lineBullet);
                        lineBullet.player = player;
                        Clear += lineBullet.Clear;
                        Command command1 = bullet.AddComponent<Command>();
                        command1.update = 重力模拟;
                        Command command2 = bullet.AddComponent<Command>();

                        command2.start = () =>
                        {
                            command2.Ints.Add(0);
                            command2.Ints.Add(0);
                        };

                        command2.update = () =>
                        {
                            float angle = Bullet.GetAngle(transform.position, lineBullet.transform.position);
                            Vector2 speed = lineBullet.SpeedToVector2();

                            if (Vector2.Distance(lineBullet.transform.position, transform.position) >= 1f && command2.Ints[0] == 0)
                            {
                                command2.Ints[0] = 1;

                                float al = 0.095f;
                                Vector2 a = new Vector2(al * Mathf.Cos(angle + Mathf.PI / 2f), al * Mathf.Sin(angle + Mathf.PI / 2));
                                lineBullet.SetSpeedWithVector2(a);
                            }
                            if (command2.count <= 300)
                            {
                                if (command2.count % 20 >= 15)
                                {
                                    GameObject bulleta = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 7);
                                    bulleta.transform.position = lineBullet.transform.position;
                                    LineBullet lineBulleta = bulleta.AddComponent<LineBullet>();
                                    lineBulleta.SetFaceAngle(lineBullet.FaceAngle - Mathf.PI / 2f);
                                    lineBulleta.Speed = 0f;
                                    Clear += lineBulleta.Clear;
                                    Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                                    lineBulleta.player = player;
                                    Command commanda = bulleta.AddComponent<Command>();
                                    commanda.update = () =>
                                    {
                                        if (commanda.count == 120)
                                        {
                                            lineBulleta.Speed = 0.05f;
                                        }
                                    };
                                }
                            }
                            else if (command2.count <= 625)
                            {
                                if (command2.count % 10 >= 6 && command2.count % 2 == 0)
                                {
                                    GameObject bulleta = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 4);
                                    bulleta.transform.position = lineBullet.transform.position;
                                    LineBullet lineBulleta = bulleta.AddComponent<LineBullet>();
                                    lineBulleta.SetFaceAngle(lineBullet.FaceAngle - Mathf.PI / 2f);
                                    lineBulleta.Speed = 0f;
                                    Clear += lineBulleta.Clear;
                                    Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                                    lineBulleta.player = player;
                                    Command commanda = bulleta.AddComponent<Command>();
                                    commanda.update = () =>
                                    {
                                        if (commanda.count == 120)
                                        {
                                            lineBulleta.Speed = 0.05f;
                                        }
                                    };
                                }
                            }
                            else if (command2.count <= 1000)
                            {
                                if (command2.count % 30 >= 10 && command2.count % 3 == 0)
                                {
                                    GameObject bulleta = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 1);
                                    bulleta.transform.position = lineBullet.transform.position;
                                    LineBullet lineBulleta = bulleta.AddComponent<LineBullet>();
                                    lineBulleta.SetFaceAngle(lineBullet.FaceAngle + Mathf.PI / 2f);
                                    lineBulleta.Speed = 0f;
                                    Clear += lineBulleta.Clear;
                                    Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                                    lineBulleta.player = player;
                                    Command commanda = bulleta.AddComponent<Command>();
                                    commanda.update = () =>
                                    {
                                        if (commanda.count == 120)
                                        {
                                            lineBulleta.Speed = 0.05f;
                                        }
                                    };
                                }
                            }
                            else if (command2.count > 1000)
                            {
                                if (command2.count % 30 >= 25)
                                {
                                    GameObject bulleta = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 6);
                                    bulleta.transform.position = lineBullet.transform.position;
                                    LineBullet lineBulleta = bulleta.AddComponent<LineBullet>();
                                    lineBulleta.SetFaceAngle(lineBullet.FaceAngle + Mathf.PI / 2f);
                                    lineBulleta.Speed = 0f;
                                    Clear += lineBulleta.Clear;
                                    Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                                    lineBulleta.player = player;
                                    Command commanda = bulleta.AddComponent<Command>();
                                    commanda.update = () =>
                                    {
                                        if (commanda.count == 120)
                                        {
                                            lineBulleta.Speed = 0.05f;
                                        }
                                    };
                                }
                            }
                            if (command2.count == 300)
                            {
                                lineBullet.Speed += 0.0175f;
                                texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 4);
                                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            }

                            if (command2.count == 625)
                            {
                                lineBullet.Speed += 0.0175f;
                                texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 1);
                                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            }
                            if (command2.count == 1000)
                            {
                                lineBullet.Speed -= 0.015f;
                                texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 6);
                                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            }
                            if (command2.count == 1500)
                            {
                                lineBullet.Speed -= 0.02f;
                                texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 6);
                                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            }
                        };

                        void 重力模拟()
                        {
                            if (Vector2.Distance(transform.position, lineBullet.transform.position) > 0.8f)
                            {
                                float G = 0.001f;
                                float angle = Bullet.GetAngle(transform.position, lineBullet.transform.position);
                                Vector2 speed = lineBullet.SpeedToVector2();
                                Vector2 a = new Vector2(G * m1 / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, transform.position), 2) * Mathf.Cos(angle + Mathf.PI), G * m1 / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, transform.position), 2) * Mathf.Sin(angle + Mathf.PI));
                                speed += a;
                                lineBullet.SetSpeedWithVector2(speed);
                            }
                            if (command1.count > 60 && Vector2.Distance(transform.position, lineBullet.transform.position) < 0.8f)
                            {
                                Destroy(lineBullet.gameObject);
                            }
                        }
                    }
                }
                break;
            case Game.GameDifficulty.Normal:
                if (count % 1000 == 0) 
                {
                    for (float i = 0; i < 2 * Mathf.PI; i += 2 * Mathf.PI / 5) 
                    {
                        GameObject bullet = new GameObject();
                        bullet.transform.position = transform.position;
                        bullet.transform.localScale = new Vector3(2f, 2f, 2f);
                        SpriteRenderer sprite = bullet.AddComponent<SpriteRenderer>();
                        Texture2D texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 7);
                        sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                        LineBullet lineBullet = bullet.AddComponent<LineBullet>();
                        lineBullet.SetFaceAngle(i);
                        lineBullet.Speed = 0.09f;
                        lineBullet.RemoveWhenInvisible = false;
                        Bullet.SetBulletBox(Bullet.BulletType.中玉, lineBullet);
                        lineBullet.player = player;
                        Clear += lineBullet.Clear;
                        Command command1 = bullet.AddComponent<Command>();
                        command1.update = 重力模拟;
                        Command command2 = bullet.AddComponent<Command>();

                        command2.start = () =>
                        {
                            command2.Ints.Add(0);
                            command2.Ints.Add(0);
                        };

                        command2.update = () =>
                        {
                            float angle = Bullet.GetAngle(transform.position, lineBullet.transform.position);
                            Vector2 speed = lineBullet.SpeedToVector2();

                            if (Vector2.Distance(lineBullet.transform.position, transform.position) >= 1f && command2.Ints[0] == 0)    
                            {
                                command2.Ints[0] = 1;

                                float al = 0.095f;
                                Vector2 a = new Vector2(al * Mathf.Cos(angle + Mathf.PI / 2f), al * Mathf.Sin(angle + Mathf.PI / 2));
                                lineBullet.SetSpeedWithVector2(a);
                            }
                            if (command2.count <= 300)
                            {
                                if (command2.count % 20 >= 15) 
                                {
                                    GameObject bulleta = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 7);
                                    bulleta.transform.position = lineBullet.transform.position;
                                    LineBullet lineBulleta = bulleta.AddComponent<LineBullet>();
                                    lineBulleta.SetFaceAngle(lineBullet.FaceAngle - Mathf.PI / 2f);
                                    lineBulleta.Speed = 0f;
                                    Clear += lineBulleta.Clear;
                                    Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                                    lineBulleta.player = player;
                                    Command commanda = bulleta.AddComponent<Command>();
                                    commanda.update = () =>
                                    {
                                        if (commanda.count == 120)
                                        {
                                            lineBulleta.Speed = 0.05f;
                                        }
                                    };
                                }
                            }
                            else if (command2.count <= 625)
                            {
                                if (command2.count % 10 >= 4 && command2.count % 2 == 0)  
                                {
                                    GameObject bulleta = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 4);
                                    bulleta.transform.position = lineBullet.transform.position;
                                    LineBullet lineBulleta = bulleta.AddComponent<LineBullet>();
                                    lineBulleta.SetFaceAngle(lineBullet.FaceAngle - Mathf.PI / 2f);
                                    lineBulleta.Speed = 0f;
                                    Clear += lineBulleta.Clear;
                                    Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                                    lineBulleta.player = player;
                                    Command commanda = bulleta.AddComponent<Command>();
                                    commanda.update = () =>
                                    {
                                        if (commanda.count == 120)
                                        {
                                            lineBulleta.Speed = 0.05f;
                                        }
                                    };
                                }
                            }
                            else if (command2.count <= 1000)
                            {
                                if (command2.count % 20 >= 10 && command2.count % 3 == 0) 
                                {
                                    GameObject bulleta = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 1);
                                    bulleta.transform.position = lineBullet.transform.position;
                                    LineBullet lineBulleta = bulleta.AddComponent<LineBullet>();
                                    lineBulleta.SetFaceAngle(lineBullet.FaceAngle + Mathf.PI / 2f);
                                    lineBulleta.Speed = 0f;
                                    Clear += lineBulleta.Clear;
                                    Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                                    lineBulleta.player = player;
                                    Command commanda = bulleta.AddComponent<Command>();
                                    commanda.update = () =>
                                    {
                                        if (commanda.count == 120)
                                        {
                                            lineBulleta.Speed = 0.05f;
                                        }
                                    };
                                }
                            }
                            else if (command2.count > 1000)
                            {
                                if (command2.count % 20 >= 15)
                                {
                                    GameObject bulleta = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 6);
                                    bulleta.transform.position = lineBullet.transform.position;
                                    LineBullet lineBulleta = bulleta.AddComponent<LineBullet>();
                                    lineBulleta.SetFaceAngle(lineBullet.FaceAngle + Mathf.PI / 2f);
                                    lineBulleta.Speed = 0f;
                                    Clear += lineBulleta.Clear;
                                    Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                                    lineBulleta.player = player;
                                    Command commanda = bulleta.AddComponent<Command>();
                                    commanda.update = () =>
                                    {
                                        if (commanda.count == 120)
                                        {
                                            lineBulleta.Speed = 0.05f;
                                        }
                                    };
                                }
                            }
                            if (command2.count == 300) 
                            {
                                lineBullet.Speed += 0.0175f;
                                texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 4);
                                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            }

                            if (command2.count == 625)  
                            {
                                lineBullet.Speed += 0.0175f;
                                texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 1);
                                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            }
                            if (command2.count == 1000) 
                            {
                                lineBullet.Speed -= 0.015f;
                                texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 6);
                                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            }
                            if (command2.count == 1500)
                            {
                                lineBullet.Speed -= 0.02f;
                                texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 6);
                                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            }
                        };
                        
                        void 重力模拟()
                        {
                            if (Vector2.Distance(transform.position, lineBullet.transform.position) > 0.8f)
                            {
                                float G = 0.001f;
                                float angle = Bullet.GetAngle(transform.position, lineBullet.transform.position);
                                Vector2 speed = lineBullet.SpeedToVector2();
                                Vector2 a = new Vector2(G * m1 / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, transform.position), 2) * Mathf.Cos(angle + Mathf.PI), G * m1 / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, transform.position), 2) * Mathf.Sin(angle + Mathf.PI));
                                speed += a;
                                lineBullet.SetSpeedWithVector2(speed);
                            }
                            if (command1.count > 60 && Vector2.Distance(transform.position, lineBullet.transform.position) < 0.8f) 
                            {
                                Destroy(lineBullet.gameObject);
                            }
                        }
                    }
                }  
                break;
            case Game.GameDifficulty.Hard:
                if (count % 750 == 0)
                {
                    for (float i = 0; i < 2 * Mathf.PI; i += 2 * Mathf.PI / 5)
                    {
                        GameObject bullet = new GameObject();
                        bullet.transform.position = transform.position;
                        bullet.transform.localScale = new Vector3(2f, 2f, 2f);
                        SpriteRenderer sprite = bullet.AddComponent<SpriteRenderer>();
                        Texture2D texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 7);
                        sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                        LineBullet lineBullet = bullet.AddComponent<LineBullet>();
                        lineBullet.SetFaceAngle(i);
                        lineBullet.Speed = 0.09f;
                        lineBullet.RemoveWhenInvisible = false;
                        Bullet.SetBulletBox(Bullet.BulletType.中玉, lineBullet);
                        lineBullet.player = player;
                        Clear += lineBullet.Clear;
                        Command command1 = bullet.AddComponent<Command>();
                        command1.update = 重力模拟;
                        Command command2 = bullet.AddComponent<Command>();

                        command2.start = () =>
                        {
                            command2.Ints.Add(0);
                            command2.Ints.Add(0);
                        };

                        command2.update = () =>
                        {
                            float angle = Bullet.GetAngle(transform.position, lineBullet.transform.position);
                            Vector2 speed = lineBullet.SpeedToVector2();

                            if (Vector2.Distance(lineBullet.transform.position, transform.position) >= 1f && command2.Ints[0] == 0)
                            {
                                command2.Ints[0] = 1;

                                float al = 0.095f;
                                Vector2 a = new Vector2(al * Mathf.Cos(angle + Mathf.PI / 2f), al * Mathf.Sin(angle + Mathf.PI / 2));
                                /*
                                speed += a;
                                lineBullet.SetFaceAngle(Bullet.GetAngle(speed));
                                lineBullet.Speed = speed.magnitude;
                                */
                                lineBullet.SetSpeedWithVector2(a);
                            }
                            if (command2.count <= 300)
                            {
                                if (command2.count % 10 >= 6)
                                {
                                    GameObject bulleta = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 7);
                                    bulleta.transform.position = lineBullet.transform.position;
                                    LineBullet lineBulleta = bulleta.AddComponent<LineBullet>();
                                    lineBulleta.SetFaceAngle(lineBullet.FaceAngle - Mathf.PI / 2f);
                                    lineBulleta.Speed = 0f;
                                    Clear += lineBulleta.Clear;
                                    Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                                    lineBulleta.player = player;
                                    Command commanda = bulleta.AddComponent<Command>();
                                    commanda.update = () =>
                                    {
                                        if (commanda.count == 120)
                                        {
                                            lineBulleta.Speed = 0.05f;
                                        }
                                    };
                                }
                            }
                            else if (command2.count <= 625)
                            {
                                if (command2.count % 5 >= 2 && command2.count % 2 == 0)
                                {
                                    GameObject bulleta = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 4);
                                    bulleta.transform.position = lineBullet.transform.position;
                                    LineBullet lineBulleta = bulleta.AddComponent<LineBullet>();
                                    lineBulleta.SetFaceAngle(lineBullet.FaceAngle - Mathf.PI / 2f);
                                    lineBulleta.Speed = 0f;
                                    Clear += lineBulleta.Clear;
                                    Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                                    lineBulleta.player = player;
                                    Command commanda = bulleta.AddComponent<Command>();
                                    commanda.update = () =>
                                    {
                                        if (commanda.count == 120)
                                        {
                                            lineBulleta.Speed = 0.05f;
                                        }
                                    };
                                }
                            }
                            else if (command2.count <= 1000)
                            {
                                if (command2.count % 10 >= 5 && command2.count % 2 == 0)
                                {
                                    GameObject bulleta = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 1);
                                    bulleta.transform.position = lineBullet.transform.position;
                                    LineBullet lineBulleta = bulleta.AddComponent<LineBullet>();
                                    lineBulleta.SetFaceAngle(lineBullet.FaceAngle + Mathf.PI / 2f);
                                    lineBulleta.Speed = 0f;
                                    Clear += lineBulleta.Clear;
                                    Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                                    lineBulleta.player = player;
                                    Command commanda = bulleta.AddComponent<Command>();
                                    commanda.update = () =>
                                    {
                                        if (commanda.count == 120)
                                        {
                                            lineBulleta.Speed = 0.05f;
                                        }
                                    };
                                }
                            }
                            else if (command2.count > 1000)
                            {
                                if (command2.count % 10 >= 8)  
                                {
                                    GameObject bulleta = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 6);
                                    bulleta.transform.position = lineBullet.transform.position;
                                    LineBullet lineBulleta = bulleta.AddComponent<LineBullet>();
                                    lineBulleta.SetFaceAngle(lineBullet.FaceAngle + Mathf.PI / 2f);
                                    lineBulleta.Speed = 0f;
                                    Clear += lineBulleta.Clear;
                                    Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                                    lineBulleta.player = player;
                                    Command commanda = bulleta.AddComponent<Command>();
                                    commanda.update = () =>
                                    {
                                        if (commanda.count == 120)
                                        {
                                            lineBulleta.Speed = 0.05f;
                                        }
                                    };
                                }
                            }
                            if (command2.count == 300)
                            {
                                lineBullet.Speed += 0.0175f;
                                texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 4);
                                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            }

                            if (command2.count == 675)
                            {
                                lineBullet.Speed += 0.0175f;
                                texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 1);
                                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            }
                            if (command2.count == 1000)
                            {
                                lineBullet.Speed -= 0.015f;
                                texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 6);
                                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            }
                            if (command2.count == 1500)
                            {
                                lineBullet.Speed -= 0.02f;
                                texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 6);
                                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            }
                        };

                        void 重力模拟()
                        {
                            if (Vector2.Distance(transform.position, lineBullet.transform.position) > 0.8f)
                            {
                                float G = 0.001f;
                                float angle = Bullet.GetAngle(transform.position, lineBullet.transform.position);
                                Vector2 speed = lineBullet.SpeedToVector2();
                                Vector2 a = new Vector2(G * m1 / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, transform.position), 2) * Mathf.Cos(angle + Mathf.PI), G * m1 / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, transform.position), 2) * Mathf.Sin(angle + Mathf.PI));
                                speed += a;
                                lineBullet.SetSpeedWithVector2(speed);
                            }
                            if (command1.count > 60 && Vector2.Distance(transform.position, lineBullet.transform.position) < 0.8f)
                            {
                                Destroy(lineBullet.gameObject);
                            }
                        }
                    }
                }
                        break;
            case Game.GameDifficulty.Lunatic:
                if (count % 1250 == 0|| count % 1250 == 250)
                {
                    for (float i = 0; i < 2 * Mathf.PI; i += 2 * Mathf.PI / 5)
                    {
                        GameObject bullet = new GameObject();
                        bullet.transform.position = transform.position;
                        bullet.transform.localScale = new Vector3(2f, 2f, 2f);
                        SpriteRenderer sprite = bullet.AddComponent<SpriteRenderer>();
                        Texture2D texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 7);
                        sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                        LineBullet lineBullet = bullet.AddComponent<LineBullet>();
                        lineBullet.SetFaceAngle(i);
                        lineBullet.Speed = 0.09f;
                        lineBullet.RemoveWhenInvisible = false;
                        Bullet.SetBulletBox(Bullet.BulletType.中玉, lineBullet);
                        lineBullet.player = player;
                        Clear += lineBullet.Clear;
                        Command command1 = bullet.AddComponent<Command>();
                        command1.update = 重力模拟;
                        Command command2 = bullet.AddComponent<Command>();

                        command2.start = () =>
                        {
                            command2.Ints.Add(0);
                            command2.Ints.Add(0);
                        };

                        command2.update = () =>
                        {
                            float angle = Bullet.GetAngle(transform.position, lineBullet.transform.position);
                            Vector2 speed = lineBullet.SpeedToVector2();

                            if (Vector2.Distance(lineBullet.transform.position, transform.position) >= 1f && command2.Ints[0] == 0)
                            {
                                command2.Ints[0] = 1;

                                float al = 0.095f;
                                Vector2 a = new Vector2(al * Mathf.Cos(angle + Mathf.PI / 2f), al * Mathf.Sin(angle + Mathf.PI / 2));
                                /*
                                speed += a;
                                lineBullet.SetFaceAngle(Bullet.GetAngle(speed));
                                lineBullet.Speed = speed.magnitude;
                                */
                                lineBullet.SetSpeedWithVector2(a);
                            }
                            if (command2.count <= 300)
                            {
                                if (command2.count % 10 >= 6)
                                {
                                    GameObject bulleta = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 7);
                                    bulleta.transform.position = lineBullet.transform.position;
                                    LineBullet lineBulleta = bulleta.AddComponent<LineBullet>();
                                    lineBulleta.SetFaceAngle(lineBullet.FaceAngle - Mathf.PI / 2f);
                                    lineBulleta.Speed = 0f;
                                    Clear += lineBulleta.Clear;
                                    Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                                    lineBulleta.player = player;
                                    Command commanda = bulleta.AddComponent<Command>();
                                    commanda.update = () =>
                                    {
                                        if (commanda.count == 120)
                                        {
                                            lineBulleta.Speed = 0.05f;
                                        }
                                    };
                                }
                            }
                            else if (command2.count <= 625)
                            {
                                if (command2.count % 5 >= 2 && command2.count % 2 == 0)
                                {
                                    GameObject bulleta = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 4);
                                    bulleta.transform.position = lineBullet.transform.position;
                                    LineBullet lineBulleta = bulleta.AddComponent<LineBullet>();
                                    lineBulleta.SetFaceAngle(lineBullet.FaceAngle - Mathf.PI / 2f);
                                    lineBulleta.Speed = 0f;
                                    Clear += lineBulleta.Clear;
                                    Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                                    lineBulleta.player = player;
                                    Command commanda = bulleta.AddComponent<Command>();
                                    commanda.update = () =>
                                    {
                                        if (commanda.count == 120)
                                        {
                                            lineBulleta.Speed = 0.05f;
                                        }
                                    };
                                }
                            }
                            else if (command2.count <= 1000)
                            {
                                if (command2.count % 10 >= 5 && command2.count % 2 == 0)
                                {
                                    GameObject bulleta = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 1);
                                    bulleta.transform.position = lineBullet.transform.position;
                                    LineBullet lineBulleta = bulleta.AddComponent<LineBullet>();
                                    lineBulleta.SetFaceAngle(lineBullet.FaceAngle + Mathf.PI / 2f);
                                    lineBulleta.Speed = 0f;
                                    Clear += lineBulleta.Clear;
                                    Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                                    lineBulleta.player = player;
                                    Command commanda = bulleta.AddComponent<Command>();
                                    commanda.update = () =>
                                    {
                                        if (commanda.count == 120)
                                        {
                                            lineBulleta.Speed = 0.05f;
                                        }
                                    };
                                }
                            }
                            else if (command2.count > 1000)
                            {
                                if (command2.count % 10 >= 8)
                                {
                                    GameObject bulleta = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.扎弹, 6);
                                    bulleta.transform.position = lineBullet.transform.position;
                                    LineBullet lineBulleta = bulleta.AddComponent<LineBullet>();
                                    lineBulleta.SetFaceAngle(lineBullet.FaceAngle + Mathf.PI / 2f);
                                    lineBulleta.Speed = 0f;
                                    Clear += lineBulleta.Clear;
                                    Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBulleta);
                                    lineBulleta.player = player;
                                    Command commanda = bulleta.AddComponent<Command>();
                                    commanda.update = () =>
                                    {
                                        if (commanda.count == 120)
                                        {
                                            lineBulleta.Speed = 0.05f;
                                        }
                                    };
                                }
                            }
                            if (command2.count == 300)
                            {
                                lineBullet.Speed += 0.0175f;
                                texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 4);
                                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            }

                            if (command2.count == 675)
                            {
                                lineBullet.Speed += 0.0175f;
                                texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 1);
                                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            }
                            if (command2.count == 1000)
                            {
                                lineBullet.Speed -= 0.015f;
                                texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 6);
                                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            }
                            if (command2.count == 1500)
                            {
                                lineBullet.Speed -= 0.02f;
                                texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 6);
                                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                            }
                        };

                        void 重力模拟()
                        {
                            if (Vector2.Distance(transform.position, lineBullet.transform.position) > 0.8f)
                            {
                                float G = 0.001f;
                                float angle = Bullet.GetAngle(transform.position, lineBullet.transform.position);
                                Vector2 speed = lineBullet.SpeedToVector2();
                                Vector2 a = new Vector2(G * m1 / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, transform.position), 2) * Mathf.Cos(angle + Mathf.PI), G * m1 / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, transform.position), 2) * Mathf.Sin(angle + Mathf.PI));
                                speed += a;
                                lineBullet.SetSpeedWithVector2(speed);
                            }
                            if (command1.count > 60 && Vector2.Distance(transform.position, lineBullet.transform.position) < 0.8f)
                            {
                                Destroy(lineBullet.gameObject);
                            }
                        }
                    }
                }
                break;
            default:
                break;
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
