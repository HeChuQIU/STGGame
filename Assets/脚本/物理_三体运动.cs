using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class 物理_三体运动 : SpellCard
{
    ulong count = 0;
    const float m = 10f;
    public Player player;

    List<LineBullet> lineBullets = new List<LineBullet>();
    protected override Action update { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override int HealPoint => 4500;

    public override event EventHandler<ClearBulletEventArgs> Clear;

    private void Start()
    {
        Difficulty = Game.GameDifficulty.Normal;
        time = 33f;

        for (int i = 0; i < 3; i++)
        {
            //Random.Range(-5f, 5f)
            GameObject bullet = new GameObject($"{i}");
            bullet.transform.localScale = new Vector3(2f, 2f, 2f);
            SpriteRenderer sprite = bullet.AddComponent<SpriteRenderer>();
            Texture2D texture = Bullet.GetBulletTexture(Bullet.BulletType.中玉, 7);
            sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            bullet.transform.position = new Vector3(3f * Mathf.Cos((2 * Mathf.PI / 3f) * (i + 1)), 3f * Mathf.Sin((2 * Mathf.PI / 3f) * (i + 1)));
            LineBullet lineBullet = bullet.AddComponent<LineBullet>();
            lineBullet.SetFaceAngle(2 * Mathf.PI / 3f * (i + 1) + Mathf.PI / 2);
            lineBullet.Speed = 0.05f + Random.Range(-0.02f, 0.02f);
            lineBullet.RemoveWhenInvisible = false;
            lineBullet.IsRotatToFace = false;
            Bullet.SetBulletBox(Bullet.BulletType.中玉, lineBullet);
            lineBullet.player = player;
            Clear += lineBullet.Clear;
            Command command1 = bullet.AddComponent<Command>();
            command1.update = 重力模拟;
            lineBullets.Add(lineBullet);
            

            void 重力模拟()
            {
                float G = 0.001f;
                float angle = Bullet.GetAngle(lineBullet.transform.position);
                Vector2 speed = lineBullet.SpeedToVector2();
                if (lineBullet.transform.position != lineBullets[0].transform.position) 
                {
                    Vector2 a1 = new Vector2(G * m / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, lineBullets[0].transform.position), 2) * Mathf.Cos(angle + Mathf.PI), G * m / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, lineBullets[0].transform.position), 2) * Mathf.Sin(angle + Mathf.PI));
                    speed += a1;
                }
                if (lineBullet.transform.position != lineBullets[1].transform.position)
                {
                    Vector2 a2 = new Vector2(G * m / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, lineBullets[1].transform.position), 2) * Mathf.Cos(angle + Mathf.PI), G * m / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, lineBullets[1].transform.position), 2) * Mathf.Sin(angle + Mathf.PI));
                    speed += a2;
                }
                if (lineBullet.transform.position != lineBullets[2].transform.position)
                {
                    Vector2 a3 = new Vector2(G * m / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, lineBullets[2].transform.position), 2) * Mathf.Cos(angle + Mathf.PI), G * m / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, lineBullets[2].transform.position), 2) * Mathf.Sin(angle + Mathf.PI));
                    speed += a3;
                }

                lineBullet.SetSpeedWithVector2(speed);
            }
        }
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
                break;
            case Game.GameDifficulty.Normal:

                break;
            case Game.GameDifficulty.Hard:
                break;
            case Game.GameDifficulty.Lunatic:
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
            GUI.Label(new Rect(Screen.width - 320f, 10f, 300f, 40f), "物理[三体运动]");
        }
    }
}
