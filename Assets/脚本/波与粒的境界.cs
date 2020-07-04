using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STGBaselib;
using System;

public class 波与粒的境界 : SpellCard
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    public Player player;
    public float shoot_place;
    public bool isClockwiseFirst = true;
    private int count = 0;

    public override event EventHandler<ClearBulletEventArgs> Clear;
    void Start()
    {
        time = 30f;
        switch (isClockwiseFirst)
        {
            case true:
                plusAngle = -0.25f;
                mode = 0;
                break;
            case false:
                plusAngle = 0.25f;
                mode = 1;
                break;
        }
    }

    // Update is called once per frame
    float plusAngle;
    float angle = 0f;
    short mode;

    protected override Action update { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override int HealPoint => throw new NotImplementedException();

    void Update()
    {
        if (time <= 0f)
        {
            time = 0f;
            Destroy(this);
            return;
        }
        else
        {
            time -= Time.deltaTime;
        }

        if (count % 2 == 0) 
        {
            for (float i = 0; i < 2 * Mathf.PI; i += (Mathf.PI * 2 / shoot_place))
            {
                GameObject bullet = Bullet.GetSpriteObjectFromFile(Bullet.BulletType.鳞弹, 5);
                bullet.transform.position = new Vector2(10f * Mathf.Cos(i + (4.5f * Mathf.PI * Mathf.Sin(count / 100f))), 10f * Mathf.Sin(i + (4.5f * Mathf.PI * Mathf.Sin(count / 100f))));
                LineBullet linebullet = bullet.AddComponent<LineBullet>();
                Bullet.SetBulletBox(Bullet.BulletType.鳞弹, linebullet);
                linebullet.Speed = 0.05f;
                linebullet.SetFaceAngle(i + (4.5f * Mathf.PI * Mathf.Sin(count / 100f)) + Mathf.PI);
                linebullet.IsRotatToFace = true;
                linebullet.gameObject.SetActive(true);
                linebullet.player = player;
                Command command = bullet.AddComponent<Command>();
                command.update = () =>
                {
                    if (Vector2.Distance(bullet.transform.position, Vector2.zero) <= 0.1f) 
                    {
                        Destroy(bullet);
                    }
                };
                Clear += linebullet.Clear;
            }

        }
        switch (mode)
        {
            case 0:
                plusAngle += 0.0025f;
                break;
            case 1:
                plusAngle -= 0.0025f;
                break;
        }
        if (plusAngle >= 0.25f)
        {
            mode = 1;
        }
        if (plusAngle <= -0.25f)
        {
            mode = 0;
        }
        angle += plusAngle;
        count++;
    }

    private void OnGUI()
    {
        GUI.skin.label.normal.textColor = new Color(1f, 1f, 1f, 1f);
        GUI.skin.label.fontSize = 32;
        GUI.Label(new Rect((Screen.width - 120f) / 2f, 10, 120f, 40f), time.ToString("#00.00"));
        GUI.Label(new Rect(40f, 10f, 120f, 40f), $"Biu : {player.Biu}");
        GUI.Label(new Rect(Screen.width - 160f, Screen.height - 50f, 120f, 40f), Difficulty.ToString());
    }

    void OnDestroy()
    {
        Clear?.Invoke(this, new ClearBulletEventArgs());
    }
}
