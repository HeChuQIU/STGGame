using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.Mathf;

public class 魔理沙一非 : SpellCard
{
    ulong count = 0;
    public SpriteRenderer spriteRenderer;
    public Player player;
    public override event EventHandler<ClearBulletEventArgs> Clear;
    public AudioSource audioSource;
    public GameControl game;
    protected override Action update { get; set; }

    public override int HealPoint => 2500;

    // Start is called before the first frame update
    void Start()
    {
        time = 45.0f;
        Difficulty = Difficulty;
        if (spriteRenderer == null) 
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        switch (Difficulty)
        {
            default:
            case GameDifficulty.Easy:
                update = Easy;
                break;
            case GameDifficulty.Normal:
                update = Normal;
                break;
            case GameDifficulty.Hard:
                update = Hard;
                break;
            case GameDifficulty.Lunatic:
                update = Lunatic;
                break;
        }
    }

    float a1 = 0f, a2 = 0f, a3 = 0f;
    /// <summary>
    /// 是否逆时针转
    /// </summary>
    bool b1 = false;
    void Update()
    {
        count++;
        if (count % 8 == 0)
        {
            Texture2D texture = (Texture2D)Resources.Load($"贴图/敌机/魔理沙/魔理沙 ({((count / 8) % 4) + 1})");
            spriteRenderer.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        if (time <= 0f)
        {
            time = 0f;
            Clear(this, new ClearBulletEventArgs());
            return;
        }
        else
        {
            time -= Time.deltaTime;
        }
        if (time <= 10f && !called)
        {
            called = true;
            StartCoroutine(timeOutClipPlay());
        }
        update();
    }
    bool called = false;

    void OnGUI()
    {
        if (time > 0f)
        {
            GUI.skin.label.normal.textColor = new Color(1f, 1f, 1f, 1f);
            GUI.skin.label.fontSize = 32;
            GUI.Label(new Rect((Screen.width - 120f) / 2f, 10, 120f, 40f), time.ToString("#00.00"));
            GUI.Label(new Rect(40f, 10, 120f, 40f), $"Biu : {player.Biu}");
        }
    }
    void AudioWAV(string name)
    {
        audioSource.clip = Resources.Load<AudioClip>($"音效/{name}");
        audioSource.Play();
    }

    IEnumerator timeOutClipPlay()
    {
        for (int i = 0; i < 11; i++)
        {
            if (time <= 3f)
            {
                game.AudioWAV("se_timeout2");
            }
            else
            {
                game.AudioWAV("se_timeout");
            }
            if (time <= 0f) 
            {
                if (player.Biu == 0) 
                {
                    game.AudioWAV("se_enep01");
                }
                else
                {
                    game.AudioWAV("se_fault");
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void Easy()
    {
        if (count >= 100)
        {
            if (count % 30 <= 20 && count % 5 == 0 && ((Bullet.GetAngle(transform.position, player.transform.position) < PI
                && Bullet.GetAngle(transform.position, player.transform.position) > 0f)
                || (count - 100) % 300 <= 150))
            {
                if ((count - 100) % 600 >= 300)
                {
                    a3 += 0.2f;
                    for (float i = 0f; i < 2 * PI; i += PI / 5)
                    {
                        GameObject bullet1 = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.小星弹, 15);
                        bullet1.transform.position = transform.position;
                        Rotat rotat1 = bullet1.AddComponent<Rotat>();
                        rotat1.Speed = UnityEngine.Random.Range(-2.5f, 2.5f);
                        rotat1.Enabled = true;
                        CircularBullet circularBullet1 = bullet1.AddComponent<CircularBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.小星弹, circularBullet1);
                        circularBullet1.IsRotatToFace = false;
                        circularBullet1.Angle = i + a3;
                        circularBullet1.W = 0.025f;
                        circularBullet1.RSpeed = 0.01f;
                        circularBullet1.player = player;
                        Clear += circularBullet1.Clear;
                        Command command1 = bullet1.AddComponent<Command>();
                        command1.update = () =>
                        {
                            if (command1.count == 150)
                            {
                                circularBullet1.RSpeed = 0f;
                            }
                            if (command1.count == 200)
                            {
                                LineBullet lineBullet1 = bullet1.AddComponent<LineBullet>();
                                Bullet.SetBulletBox(Bullet.BulletType.小星弹, lineBullet1);
                                lineBullet1.player = player;
                                lineBullet1.SetFaceAngle(circularBullet1.FaceAngle + PI / 2);
                                lineBullet1.Speed = 0.04f;
                                Clear += lineBullet1.Clear;
                                Clear -= circularBullet1.Clear;
                                Destroy(circularBullet1);
                                if (count % 2 == 0)
                                {
                                AudioWAV("se_kira00");
                                }
                            }
                        };


                        GameObject bullet2 = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.小星弹, 15);
                        bullet2.transform.position = transform.position;
                        Rotat rotat2 = bullet2.AddComponent<Rotat>();
                        rotat2.Speed = UnityEngine.Random.Range(-2.5f, 2.5f);
                        rotat2.Enabled = true;
                        CircularBullet circularBullet2 = bullet2.AddComponent<CircularBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.小星弹, circularBullet1);
                        circularBullet2.IsRotatToFace = false;
                        circularBullet2.Angle = i + a3;
                        circularBullet2.W = -0.025f;
                        circularBullet2.RSpeed = 0.01f;
                        circularBullet2.player = player;
                        Clear += circularBullet2.Clear;
                        Command command2 = bullet2.AddComponent<Command>();
                        command2.update = () =>
                        {
                            if (command2.count == 150)
                            {
                                circularBullet2.RSpeed = 0f;
                            }
                            if (command2.count == 250)
                            {
                                LineBullet lineBullet2 = bullet2.AddComponent<LineBullet>();
                                Bullet.SetBulletBox(Bullet.BulletType.小星弹, lineBullet2);
                                lineBullet2.player = player;
                                lineBullet2.SetFaceAngle(circularBullet1.FaceAngle + PI / 2);
                                lineBullet2.Speed = 0.04f;
                                Clear += lineBullet2.Clear;
                                Clear -= circularBullet2.Clear;
                                Destroy(circularBullet2);
                                if (count % 2 == 0) 
                                {
                                    AudioWAV("se_kira00");
                                }
                            }
                        };
                    }
                    if (count % 2 == 0)
                    {
                    AudioWAV("se_tan00");
                    }
                }
                else
                {
                    a3 += 0.2f;
                    for (float i = 0f; i < 2 * PI; i += PI / 5)
                    {
                        GameObject bullet1 = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.小星弹, 15);
                        bullet1.transform.position = transform.position;
                        Rotat rotat1 = bullet1.AddComponent<Rotat>();
                        rotat1.Speed = UnityEngine.Random.Range(-2.5f, 2.5f);
                        rotat1.Enabled = true;
                        CircularBullet circularBullet1 = bullet1.AddComponent<CircularBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.小星弹, circularBullet1);
                        circularBullet1.IsRotatToFace = false;
                        circularBullet1.Angle = i + a3;
                        circularBullet1.W = -0.025f;
                        circularBullet1.RSpeed = 0.01f;
                        circularBullet1.player = player;
                        Clear += circularBullet1.Clear;
                        Command command1 = bullet1.AddComponent<Command>();
                        command1.update = () =>
                        {
                            if (command1.count == 150)
                            {
                                circularBullet1.RSpeed = 0f;
                            }
                            if (command1.count == 250)
                            {
                                LineBullet lineBullet1 = bullet1.AddComponent<LineBullet>();
                                Bullet.SetBulletBox(Bullet.BulletType.小星弹, lineBullet1);
                                lineBullet1.player = player;
                                lineBullet1.SetFaceAngle(circularBullet1.FaceAngle + PI / 4);
                                lineBullet1.Speed = 0.04f;
                                Clear += lineBullet1.Clear;
                                Clear -= circularBullet1.Clear;
                                Destroy(circularBullet1);
                                if (count % 2 == 0)
                                {
                                AudioWAV("se_kira00");
                                }
                            }
                        };


                        GameObject bullet2 = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.小星弹, 15);
                        bullet2.transform.position = transform.position;
                        Rotat rotat2 = bullet2.AddComponent<Rotat>();
                        rotat2.Speed = UnityEngine.Random.Range(-2.5f, 2.5f);
                        rotat2.Enabled = true;
                        CircularBullet circularBullet2 = bullet2.AddComponent<CircularBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.小星弹, circularBullet1);
                        circularBullet2.IsRotatToFace = false;
                        circularBullet2.Angle = i + a3;
                        circularBullet2.W = 0.025f;
                        circularBullet2.RSpeed = 0.01f;
                        circularBullet2.player = player;
                        Clear += circularBullet2.Clear;
                        Command command2 = bullet2.AddComponent<Command>();
                        command2.update = () =>
                        {
                            if (command2.count == 150)
                            {
                                circularBullet2.RSpeed = 0f;
                            }
                            if (command2.count == 200)
                            {
                                LineBullet lineBullet2 = bullet2.AddComponent<LineBullet>();
                                Bullet.SetBulletBox(Bullet.BulletType.小星弹, lineBullet2);
                                lineBullet2.player = player;
                                lineBullet2.SetFaceAngle(circularBullet1.FaceAngle + PI / 4);
                                lineBullet2.Speed = 0.04f;
                                Clear += lineBullet2.Clear;
                                Clear -= circularBullet2.Clear;
                                Destroy(circularBullet2);
                                if (count % 2 == 0)
                                {
                                AudioWAV("se_kira00");
                                }
                            }
                        };
                    }
                    if (count % 2 == 0)
                    {
                    AudioWAV("se_tan00");
                    }
                }
            }
        }
    }

    void Normal()
    {
        if (count % 60 >= 10 && count % 10 == 0 && ((Bullet.GetAngle(transform.position, player.transform.position) < PI
                && Bullet.GetAngle(transform.position, player.transform.position) > 0f)
                || count % 300 <= 150))
        {
            a3 += 0.2f;
            for (float i = 0f; i < 2 * PI; i += PI / 5)
            {
                GameObject bullet1 = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.小星弹, 15);
                bullet1.transform.position = transform.position;
                Rotat rotat1 = bullet1.AddComponent<Rotat>();
                rotat1.Speed = UnityEngine.Random.Range(-2.5f, 2.5f);
                rotat1.Enabled = true;
                CircularBullet circularBullet1 = bullet1.AddComponent<CircularBullet>();
                Bullet.SetBulletBox(Bullet.BulletType.小星弹, circularBullet1);
                circularBullet1.IsRotatToFace = false;
                circularBullet1.Angle = i + a3;
                circularBullet1.W = -0.025f;
                circularBullet1.RSpeed = 0.01f;
                circularBullet1.player = player;
                Clear += circularBullet1.Clear;
                Command command1 = bullet1.AddComponent<Command>();
                command1.update = () =>
                {
                    if (command1.count == 150)
                    {
                        circularBullet1.RSpeed = 0f;
                    }
                    if (command1.count == 250)
                    {
                        LineBullet lineBullet1 = bullet1.AddComponent<LineBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.小星弹, lineBullet1);
                        lineBullet1.player = player;
                        lineBullet1.SetFaceAngle(circularBullet1.FaceAngle + PI / 2);
                        lineBullet1.Speed = 0.04f;
                        Clear += lineBullet1.Clear;
                        Clear -= circularBullet1.Clear;
                        Destroy(circularBullet1);

                        GameObject bullet1a = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.小星弹, UnityEngine.Random.Range(1, 16));
                        bullet1a.transform.position = bullet1.transform.position;
                        Rotat rotat1a = bullet1a.AddComponent<Rotat>();
                        rotat1a.Speed = UnityEngine.Random.Range(-2.5f, 2.5f);
                        rotat1a.Enabled = true;
                        LineBullet lineBullet1a = bullet1a.AddComponent<LineBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.小星弹, lineBullet1a);
                        lineBullet1a.Speed = 0.02f;
                        lineBullet1a.SetFaceAngle(circularBullet1.FaceAngle);
                        lineBullet1a.player = player;
                        Clear += lineBullet1a.Clear;
                        AudioWAV("se_kira00");
                    }
                };


                GameObject bullet2 = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.小星弹, 15);
                bullet2.transform.position = transform.position;
                Rotat rotat2 = bullet2.AddComponent<Rotat>();
                rotat2.Speed = UnityEngine.Random.Range(-2.5f, 2.5f);
                rotat2.Enabled = true;
                CircularBullet circularBullet2 = bullet2.AddComponent<CircularBullet>();
                Bullet.SetBulletBox(Bullet.BulletType.小星弹, circularBullet1);
                circularBullet2.IsRotatToFace = false;
                circularBullet2.Angle = i + a3;
                circularBullet2.W = 0.025f;
                circularBullet2.RSpeed = 0.01f;
                circularBullet2.player = player;
                Clear += circularBullet2.Clear;
                Command command2 = bullet2.AddComponent<Command>();
                command2.update = () =>
                {
                    if (command2.count == 150)
                    {
                        circularBullet2.RSpeed = 0f;
                    }
                    if (command2.count == 200)
                    {
                        LineBullet lineBullet2 = bullet2.AddComponent<LineBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.小星弹, lineBullet2);
                        lineBullet2.player = player;
                        lineBullet2.SetFaceAngle(circularBullet1.FaceAngle + PI / 4);
                        lineBullet2.Speed = 0.04f;
                        Clear += lineBullet2.Clear;
                        Clear -= circularBullet2.Clear;
                        Destroy(circularBullet2);

                        GameObject bullet2a = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.小星弹, UnityEngine.Random.Range(1, 16));
                        bullet2a.transform.position = bullet2.transform.position;
                        Rotat rotat2a = bullet2a.AddComponent<Rotat>();
                        rotat2a.Speed = UnityEngine.Random.Range(-2.5f, 2.5f);
                        rotat2a.Enabled = true;
                        LineBullet lineBullet2a = bullet2a.AddComponent<LineBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.小星弹, lineBullet2a);
                        lineBullet2a.Speed = 0.01f;
                        lineBullet2a.SetFaceAngle(circularBullet1.FaceAngle);
                        lineBullet2a.player = player;
                        Clear += lineBullet2a.Clear;
                        AudioWAV("se_kira00");
                    }
                };
            }
            if (count % 2 == 0)
            {
                AudioWAV("se_tan00");
            }
        }
    }

    void Hard()
    {
        if (count % 30 < 10 && count % 10 == 0 && ((Bullet.GetAngle(transform.position, player.transform.position) < PI
&& Bullet.GetAngle(transform.position, player.transform.position) > 0f)
|| count % 600 > 200)) 
        {
            a3 += 0.2f;
            for (float i = 0f; i < 2 * PI; i += PI / 5)
            {
                GameObject bullet1 = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.小星弹, 15);
                bullet1.transform.position = transform.position;
                Rotat rotat1 = bullet1.AddComponent<Rotat>();
                rotat1.Speed = UnityEngine.Random.Range(-2.5f, 2.5f);
                rotat1.Enabled = true;
                CircularBullet circularBullet1 = bullet1.AddComponent<CircularBullet>();
                Bullet.SetBulletBox(Bullet.BulletType.小星弹, circularBullet1);
                circularBullet1.IsRotatToFace = false;
                circularBullet1.Angle = i + a3;
                circularBullet1.W = -0.025f;
                circularBullet1.RSpeed = 0.01f;
                circularBullet1.player = player;
                Clear += circularBullet1.Clear;
                Command command1 = bullet1.AddComponent<Command>();
                command1.update = () =>
                {
                    if (command1.count == 150)
                    {
                        circularBullet1.RSpeed = 0f;
                    }
                    if (command1.count == 250)
                    {
                        LineBullet lineBullet1 = bullet1.AddComponent<LineBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.小星弹, lineBullet1);
                        lineBullet1.player = player;
                        lineBullet1.SetFaceAngle(circularBullet1.FaceAngle + PI / 2);
                        lineBullet1.Speed = 0.04f;
                        Clear += lineBullet1.Clear;
                        Clear -= circularBullet1.Clear;
                        Destroy(circularBullet1);
                        for (float j = 0.02f; j <= 0.06f; j += 0.02f)
                        {
                            GameObject bullet1a = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.小星弹, UnityEngine.Random.Range(1, 16));
                            bullet1a.transform.position = bullet1.transform.position;
                            Rotat rotat1a = bullet1a.AddComponent<Rotat>();
                            rotat1a.Speed = UnityEngine.Random.Range(-2.5f, 2.5f);
                            rotat1a.Enabled = true;
                            LineBullet lineBullet1a = bullet1a.AddComponent<LineBullet>();
                            Bullet.SetBulletBox(Bullet.BulletType.小星弹, lineBullet1a);
                            lineBullet1a.Speed = j;
                            lineBullet1a.SetFaceAngle(circularBullet1.FaceAngle);
                            lineBullet1a.player = player;
                            Clear += lineBullet1a.Clear;
                        }
                        AudioWAV("se_kira00");
                    }
                };


                GameObject bullet2 = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.小星弹, 15);
                bullet2.transform.position = transform.position;
                Rotat rotat2 = bullet2.AddComponent<Rotat>();
                rotat2.Speed = UnityEngine.Random.Range(-2.5f, 2.5f);
                rotat2.Enabled = true;
                CircularBullet circularBullet2 = bullet2.AddComponent<CircularBullet>();
                Bullet.SetBulletBox(Bullet.BulletType.小星弹, circularBullet1);
                circularBullet2.IsRotatToFace = false;
                circularBullet2.Angle = i + a3;
                circularBullet2.W = 0.025f;
                circularBullet2.RSpeed = 0.01f;
                circularBullet2.player = player;
                Clear += circularBullet2.Clear;
                Command command2 = bullet2.AddComponent<Command>();
                command2.update = () =>
                {
                    if (command2.count == 150)
                    {
                        circularBullet2.RSpeed = 0f;
                    }
                    if (command2.count == 200)
                    {
                        LineBullet lineBullet2 = bullet2.AddComponent<LineBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.小星弹, lineBullet2);
                        lineBullet2.player = player;
                        lineBullet2.SetFaceAngle(circularBullet1.FaceAngle + PI / 4);
                        lineBullet2.Speed = 0.04f;
                        Clear += lineBullet2.Clear;
                        Clear -= circularBullet2.Clear;
                        Destroy(circularBullet2);
                        for (float j = 0.02f; j <= 0.06f; j += 0.02f)
                        {
                            GameObject bullet2a = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.小星弹, UnityEngine.Random.Range(1, 16));
                            bullet2a.transform.position = bullet2.transform.position;
                            Rotat rotat2a = bullet2a.AddComponent<Rotat>();
                            rotat2a.Speed = UnityEngine.Random.Range(-2.5f, 2.5f);
                            rotat2a.Enabled = true;
                            LineBullet lineBullet2a = bullet2a.AddComponent<LineBullet>();
                            Bullet.SetBulletBox(Bullet.BulletType.小星弹, lineBullet2a);
                            lineBullet2a.Speed = j;
                            lineBullet2a.SetFaceAngle(circularBullet1.FaceAngle);
                            lineBullet2a.player = player;
                            Clear += lineBullet2a.Clear;
                        }
                        AudioWAV("se_kira00");
                    }
                };
            }
            if (count % 2 == 0)
            {
                AudioWAV("se_tan00");
            }
        }
    }

    void Lunatic()
    {
        if (count % 75 >= 60 && count % 5 == 0 && ((Bullet.GetAngle(transform.position, player.transform.position) < PI
&& Bullet.GetAngle(transform.position, player.transform.position) > 0f)
|| count % 1000 < 700))
        {
            a3 += 0.2f;
            for (float i = 0f; i < 2 * PI; i += PI / 5)
            {
                GameObject bullet1 = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.小星弹, 15);
                bullet1.transform.position = transform.position;
                Rotat rotat1 = bullet1.AddComponent<Rotat>();
                rotat1.Speed = UnityEngine.Random.Range(-2.5f, 2.5f);
                rotat1.Enabled = true;
                CircularBullet circularBullet1 = bullet1.AddComponent<CircularBullet>();
                Bullet.SetBulletBox(Bullet.BulletType.小星弹, circularBullet1);
                circularBullet1.IsRotatToFace = false;
                circularBullet1.Angle = i + a3;
                circularBullet1.W = -0.025f;
                circularBullet1.RSpeed = 0.01f;
                circularBullet1.player = player;
                Clear += circularBullet1.Clear;
                Command command1 = bullet1.AddComponent<Command>();
                command1.update = () =>
                {
                    if (command1.count == 150)
                    {
                        circularBullet1.RSpeed = 0f;
                    }
                    if (command1.count == 250)
                    {
                        LineBullet lineBullet1 = bullet1.AddComponent<LineBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.小星弹, lineBullet1);
                        lineBullet1.player = player;
                        lineBullet1.SetFaceAngle(circularBullet1.FaceAngle + PI / 2);
                        lineBullet1.Speed = 0.04f;
                        Clear += lineBullet1.Clear;
                        Clear -= circularBullet1.Clear;
                        Destroy(circularBullet1);
                        for (float j = 0.02f; j <= 0.08f; j += 0.01f)
                        {
                            GameObject bullet1a = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.小星弹, UnityEngine.Random.Range(1, 16));
                            bullet1a.transform.position = bullet1.transform.position;
                            Rotat rotat1a = bullet1a.AddComponent<Rotat>();
                            rotat1a.Speed = UnityEngine.Random.Range(-2.5f, 2.5f);
                            rotat1a.Enabled = true;
                            LineBullet lineBullet1a = bullet1a.AddComponent<LineBullet>();
                            Bullet.SetBulletBox(Bullet.BulletType.小星弹, lineBullet1a);
                            lineBullet1a.Speed = j;
                            lineBullet1a.SetFaceAngle(circularBullet1.FaceAngle);
                            lineBullet1a.player = player;
                            Clear += lineBullet1a.Clear;
                        }
                        AudioWAV("se_kira00");
                    }
                };


                GameObject bullet2 = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.小星弹, 15);
                bullet2.transform.position = transform.position;
                Rotat rotat2 = bullet2.AddComponent<Rotat>();
                rotat2.Speed = UnityEngine.Random.Range(-2.5f, 2.5f);
                rotat2.Enabled = true;
                CircularBullet circularBullet2 = bullet2.AddComponent<CircularBullet>();
                Bullet.SetBulletBox(Bullet.BulletType.小星弹, circularBullet1);
                circularBullet2.IsRotatToFace = false;
                circularBullet2.Angle = i + a3;
                circularBullet2.W = 0.025f;
                circularBullet2.RSpeed = 0.01f;
                circularBullet2.player = player;
                Clear += circularBullet2.Clear;
                Command command2 = bullet2.AddComponent<Command>();
                command2.update = () =>
                {
                    if (command2.count == 150)
                    {
                        circularBullet2.RSpeed = 0f;
                    }
                    if (command2.count == 200)
                    {
                        LineBullet lineBullet2 = bullet2.AddComponent<LineBullet>();
                        Bullet.SetBulletBox(Bullet.BulletType.小星弹, lineBullet2);
                        lineBullet2.player = player;
                        lineBullet2.SetFaceAngle(circularBullet1.FaceAngle + PI / 4);
                        lineBullet2.Speed = 0.04f;
                        Clear += lineBullet2.Clear;
                        Clear -= circularBullet2.Clear;
                        Destroy(circularBullet2);
                        for (float j = 0.02f; j <= 0.08f; j += 0.01f)
                        {
                            GameObject bullet2a = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.小星弹, UnityEngine.Random.Range(1, 16));
                            bullet2a.transform.position = bullet2.transform.position;
                            Rotat rotat2a = bullet2a.AddComponent<Rotat>();
                            rotat2a.Speed = UnityEngine.Random.Range(-2.5f, 2.5f);
                            rotat2a.Enabled = true;
                            LineBullet lineBullet2a = bullet2a.AddComponent<LineBullet>();
                            Bullet.SetBulletBox(Bullet.BulletType.小星弹, lineBullet2a);
                            lineBullet2a.Speed = j;
                            lineBullet2a.SetFaceAngle(circularBullet1.FaceAngle);
                            lineBullet2a.player = player;
                            Clear += lineBullet2a.Clear;
                        }
                        AudioWAV("se_kira00");
                    }
                };
            }
            if (count % 2 == 0)
            {
                AudioWAV("se_tan00");
            }
        }
    }
}
