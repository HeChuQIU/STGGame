using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class 完全随机 : SpellCard 
{
    ulong count = 0;
    public override event EventHandler<ClearBulletEventArgs> Clear;
    AudioSource audioSource;
    AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        time = 31f;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioClip = Resources.Load<AudioClip>("音效/se_ch00");
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (count % 500 == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 vector = new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(0f, 5f));
                GameObject bullet = new GameObject("bullet");
                SpriteRenderer sprite = bullet.AddComponent<SpriteRenderer>();
                Command command = bullet.AddComponent<Command>();
                Texture2D texture = (Texture2D)Resources.Load($"贴图/子弹/bullet2_6_5");
                bullet.transform.position = vector;
                bullet.transform.localScale = new Vector3(2f, 2f, 2f);
                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                command.update = update;
                void update()
                {
                    if (command.count >= 150 && command.count % 5 == 0)  
                    {
                        if (command.count % 10 == 0) 
                        {
                            AudioSource audioSource1;

                            //音效
                            audioSource1 = command.gameObject.AddComponent<AudioSource>();
                            audioSource1.clip = Resources.Load<AudioClip>("音效/se_tan00");
                            audioSource1.Play();
                            //
                        }
                        GameObject bullet1 = new GameObject("bullet");
                        SpriteRenderer sprite1 = bullet1.AddComponent<SpriteRenderer>();
                        Texture2D texture1 = (Texture2D)Resources.Load($"贴图/子弹/bullet1_2_{UnityEngine.Random.Range(1, 16)}");
                        LineBullet lineBullet = bullet1.AddComponent<LineBullet>();
                        Command command1 = bullet1.AddComponent<Command>();

                        command1.update = u;
                        bullet1.transform.position = vector;
                        bullet1.transform.localScale = new Vector3(2f, 2f, 2f);
                        sprite1.sprite = Sprite.Create(texture1, new Rect(0, 0, texture1.width, texture1.height), new Vector2(0.5f, 0.5f));
                        lineBullet.SetFaceAngle(UnityEngine.Random.Range(0f, 2 * Mathf.PI));
                        lineBullet.Speed = UnityEngine.Random.Range(0.01f, 0.075f);
                        lineBullet.OriginVector = command.transform.position;
                        Clear += lineBullet.Clear;
                        void u()
                        {
                            if (command1.count >= 30 && lineBullet.Speed > 0.005f && command1.count < 750)  
                            {
                                lineBullet.Speed -= 0.0005f;
                            }
                            if (command1.count >= 750) 
                            {
                                lineBullet.Speed += 0.00025f;
                            }
                        }
                    }
                    if (command.count == 250) 
                    {
                        Destroy(command.gameObject);
                    }
                }
            }
        }
        time -= UnityEngine.Time.deltaTime;
        if (time <= 10f && !called) 
        {
            called = true;
            StartCoroutine(timeOutClipPlay());            
        }
        if (time <= 0f)  
        {
            audioClip = Resources.Load<AudioClip>("音效/se_fault");
            audioSource.clip = audioClip;
            audioSource.Play();
            Destroy(this);
        }
        count++;
    }
    bool called = false;

    protected override Action update { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override int HealPoint => throw new NotImplementedException();

    IEnumerator timeOutClipPlay()
    {
        audioClip = Resources.Load<AudioClip>("音效/se_timeout");
        audioSource.clip = audioClip;
        for (int i = 0; i < 10; i++)
        {
            if (time <= 3f) 
            {
                audioClip = Resources.Load<AudioClip>("音效/se_timeout2");
                audioSource.clip = audioClip;
            }
            audioSource.Play();
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnGUI()
    {
        GUI.skin.label.normal.textColor = new Color(1f, 1f, 1f, 1f);
        GUI.skin.label.fontSize = 32;
        GUI.Label(new Rect((Screen.width - 120f) / 2f, 10, 120f, 40f), time.ToString("#00.00"));
    }

    private void OnDestroy()
    {
        Clear?.Invoke(this, new ClearBulletEventArgs());
    }

}
