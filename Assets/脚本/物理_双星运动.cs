using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 物理_双星运动 : SpellCard
{
    public override int HealPoint => throw new NotImplementedException();

    protected override Action update { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override event EventHandler<ClearBulletEventArgs> Clear;

    public Player player;
    ulong count = 0;

    CircularBullet circularBullet1, circularBullet2;
    // Start is called before the first frame update
    void Start()
    {
        time = 25f;

        GameObject star1 = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.中玉, 7),
    star2 = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.中玉, 7);
        circularBullet1 = star1.AddComponent<CircularBullet>();
        circularBullet1.W = 0.05f;
        circularBullet1.RailR = 3f;
        circularBullet1.Angle = Mathf.PI / 2f;
        Clear += circularBullet1.Clear;
        Bullet.SetBulletBox(Bullet.BulletType.中玉, circularBullet1);
        circularBullet1.player = player;

        circularBullet2 = star2.AddComponent<CircularBullet>();
        circularBullet2.W = 0.05f;
        circularBullet2.RailR = 1.5f;
        circularBullet2.Angle = -Mathf.PI / 2f;
        Clear += circularBullet2.Clear;
        Bullet.SetBulletBox(Bullet.BulletType.中玉, circularBullet2);
        circularBullet2.player = player;
    }

    const float m1 = 2f, m2 = 1f;
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

        if (count % 5 == 0) 
        {
            for (float i = 0; i < 2 * Mathf.PI; i += 2 * Mathf.PI / 5)
            {
                GameObject bullet = new GameObject();
                bullet.transform.localScale = new Vector3(2f, 2f, 2f);
                SpriteRenderer sprite = bullet.AddComponent<SpriteRenderer>();
                Texture2D texture = Bullet.GetBulletTexture(Bullet.BulletType.扎弹, 12);
                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                bullet.transform.position = new Vector2(0f, 0f);
                LineBullet lineBullet = bullet.AddComponent<LineBullet>();
                lineBullet.SetFaceAngle(i);
                lineBullet.Speed = 0.05f;
                lineBullet.RemoveWhenInvisible = false;
                Bullet.SetBulletBox(Bullet.BulletType.扎弹, lineBullet);
                lineBullet.player = player;
                Clear += lineBullet.Clear;
                Command command1 = bullet.AddComponent<Command>();
                command1.update = 重力模拟;

                void 重力模拟()
                {

                    float G = 0.001f;
                    float angle = Bullet.GetAngle(lineBullet.transform.position);
                    Vector2 speed = lineBullet.SpeedToVector2();
                    Vector2 a1 = new Vector2(G * m1 / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, circularBullet1.transform.position), 2) * Mathf.Cos(angle + Mathf.PI), G * m1 / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, circularBullet1.transform.position), 2) * Mathf.Sin(angle + Mathf.PI));
                    speed += a1;

                    Vector2 a2 = new Vector2(G * m2 / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, circularBullet2.transform.position), 2) * Mathf.Cos(angle + Mathf.PI), G * m1 / Mathf.Pow(Vector2.Distance(lineBullet.transform.position, circularBullet2.transform.position), 2) * Mathf.Sin(angle + Mathf.PI));
                    speed += a2;

                    lineBullet.SetSpeedWithVector2(speed);

                    if (lineBullet.Speed > 0.1f) 
                    {
                        lineBullet.Speed = 0.1f;
                    }

                    if (Vector2.Distance(circularBullet1.transform.position, lineBullet.transform.position) < 8.5f / 50f || Vector2.Distance(circularBullet2.transform.position, lineBullet.transform.position) < 8.5f / 50f) 
                    {
                        Destroy(lineBullet.gameObject);
                    }
                }
            }
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
            GUI.Label(new Rect(Screen.width - 320f, 10f, 300f, 40f), "物理[双星运动]");
        }
    }

}
