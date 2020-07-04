using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class 百鬼夜行 : SpellCard
{
    public override int HealPoint => 2500;

    protected override Action update { get; set; }

    public override event EventHandler<ClearBulletEventArgs> Clear;

    public Player player;

    ulong count = 0;

    void Start()
    {
        time = 45f;
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

        switch (Difficulty)
        {
            default:
            case GameDifficulty.Easy:
            case GameDifficulty.Normal:
            case GameDifficulty.Hard:
            case GameDifficulty.Lunatic:
                if (count % 240 == 0) 
                {
                    GameObject bullet = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.大玉, 1);
                    bullet.transform.position = transform.position;
                    LineBullet lineBullet = bullet.AddComponent<LineBullet>();
                    Bullet.SetBulletBox(Bullet.BulletType.大玉, lineBullet);
                    lineBullet.SetFaceAngle(Bullet.GetAngle(lineBullet.transform.position, player.Position));
                    lineBullet.Speed = 0.05f;
                    lineBullet.player = player;
                    Clear += lineBullet.Clear;
                }
                switch (Random.Range(1, 3))
                {
                    case 1:
                        if (count % 150 == 0)
                        {
                            GameObject 小萃香 = new GameObject("小萃香");
                            小萃香.transform.position = new Vector3(-7f, Random.Range(0, 5f));
                            小伊吹萃香 小伊吹萃香 = 小萃香.AddComponent<小伊吹萃香>();
                            小伊吹萃香.Difficulty = Difficulty;
                            小伊吹萃香.player = player;
                            小伊吹萃香.百鬼夜行 = this;
                            Clear += 小伊吹萃香.clear;
                            Clear += 小伊吹萃香.Clear;
                            LineBullet lineBullet = 小伊吹萃香.gameObject.AddComponent<LineBullet>();
                            lineBullet.Type = Box.BoxType.Round;
                            lineBullet.R = 8f / 50f;
                            lineBullet.Speed = Random.Range(0.01f, 0.025f);
                            lineBullet.SetFaceAngle(Random.Range(-Mathf.PI / 4f, Mathf.PI / 4f));
                            lineBullet.IsRotatToFace = false;
                        }
                        break;
                    case 2:
                        if (count % 150 == 0)
                        {
                            GameObject 小萃香 = new GameObject("小萃香");
                            小萃香.transform.position = new Vector3(Random.Range(-7f, 7f), 6f);
                            小伊吹萃香 小伊吹萃香 = 小萃香.AddComponent<小伊吹萃香>();
                            小伊吹萃香.Difficulty = Difficulty;
                            小伊吹萃香.player = player;
                            小伊吹萃香.百鬼夜行 = this;
                            Clear += 小伊吹萃香.clear;
                            Clear += 小伊吹萃香.Clear;
                            LineBullet lineBullet = 小伊吹萃香.gameObject.AddComponent<LineBullet>();
                            lineBullet.Type = Box.BoxType.Round;
                            lineBullet.R = 8f / 50f;
                            lineBullet.Speed = Random.Range(0.01f, 0.025f);
                            lineBullet.SetFaceAngle(Random.Range(-Mathf.PI / 4f * 3, -Mathf.PI / 4f));
                            lineBullet.IsRotatToFace = false;
                        }
                        break;
                    case 3:
                        if (count % 150 == 0)
                        {
                            GameObject 小萃香 = new GameObject("小萃香");
                            小萃香.transform.position = new Vector3(7f, Random.Range(0, 5f));
                            小伊吹萃香 小伊吹萃香 = 小萃香.AddComponent<小伊吹萃香>();
                            小伊吹萃香.Difficulty = Difficulty;
                            小伊吹萃香.player = player;
                            小伊吹萃香.百鬼夜行 = this;
                            Clear += 小伊吹萃香.clear;
                            Clear += 小伊吹萃香.Clear; 
                            LineBullet lineBullet = 小伊吹萃香.gameObject.AddComponent<LineBullet>();
                            lineBullet.Type = Box.BoxType.Round;
                            lineBullet.R = 8f / 50f;
                            lineBullet.Speed = Random.Range(0.01f, 0.025f);
                            lineBullet.SetFaceAngle(Random.Range(-Mathf.PI / 4f * 3f, Mathf.PI / 4f * 3f));
                            lineBullet.IsRotatToFace = false;
                        }
                        break;
                }
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
            GUI.Label(new Rect(Screen.width - 160f, 10f, 120f, 40f), Difficulty.ToString());
        }
    }

    void OnDestroy()
    {
        Clear?.Invoke(this, new ClearBulletEventArgs());
    }

}
