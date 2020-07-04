using UnityEngine;
using System;

public abstract class Bullet : Box
{
    private AudioSource audioSource;

    public abstract Vector3 OriginVector { get; set; }
    public abstract Vector3 NowVector { get; protected set; }
    public abstract Vector3 NextVector { get; }
    public abstract Player player { get; set; }
    new public abstract event EventHandler<MissEventArgs> Miss;

    private void Start()
    {
        if (player == null) 
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        Miss += player.Miss;
    }

    public virtual float FaceAngle
    {
        get
        {
            if (NextVector.x > NowVector.x)
            {
                return Mathf.Asin((NextVector.y - NowVector.y) / Vector3.Distance(NextVector, NowVector));
            }
            else
            {
                float angle = Mathf.PI - Mathf.Asin((NextVector.y - NowVector.y) / Vector3.Distance(NextVector, NowVector));
                if (angle > Mathf.PI) 
                {
                    angle -= 2 * Mathf.PI;
                }
                return angle;
            }
        }
    }
    public abstract bool IsRotatToFace { get; set; }

    public abstract void Clear(object sender, ClearBulletEventArgs e);

    public void AudioWAV(string name)
    {
        if (audioSource == null) 
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = Resources.Load<AudioClip>($"音效/{name}");
        audioSource.Play();
    }

    public static float GetAngle(Vector3 o,Vector3 vector)
    {
        float angle;
        vector -= o;
        Vector3 cross = Vector3.Cross(Vector3.right, vector);
        angle = Vector2.Angle(Vector3.right, vector) * Mathf.Deg2Rad;
        return cross.z > 0 ? angle : -angle;
    }

    public static float GetAngle(Vector3 vector)
    {
        float angle;
        Vector3 cross = Vector3.Cross(Vector3.right, vector);
        angle = Vector2.Angle(Vector3.right, vector) * Mathf.Deg2Rad;
        return cross.z > 0 ? angle : -angle;
    }

    public  static float ChangeAngleTo2PI(float angle)
    {
    begin:
        if (angle > 2 * Mathf.PI) 
        {
            angle -= 2 * Mathf.PI;
        }
        if (angle < 0) 
        {
            angle += 2 * Mathf.PI;
        }
        if (angle > 2 * Mathf.PI || angle < 0) 
        {
            goto begin;
        }
        return angle;
    }

    public static GameObject GetSpriteObjectFromFile(string resourcesPath, string objectName="bullet")
    {
        GameObject obj = new GameObject(objectName);
        SpriteRenderer sprite = obj.AddComponent<SpriteRenderer>();
        Texture2D texture = (Texture2D)Resources.Load(resourcesPath);
        sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        return obj;
    }

    public static GameObject GetSpriteObjectFromFile(BulletType bulletType, int color = 1)
    {
        GameObject bullet = new GameObject();
        bullet.transform.localScale = new Vector3(2f, 2f, 2f);
        SpriteRenderer sprite = bullet.AddComponent<SpriteRenderer>();
        Texture2D texture = GetBulletTexture(bulletType, color);
        sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        return bullet;
    }

    public static Texture2D GetBulletTexture(BulletType bulletType, int color)
    {
        string resourcesPath = "贴图/子弹/";
        switch (bulletType)
        {
            case BulletType.点弹:
                resourcesPath += "点弹";
                break;
            case BulletType.菌弹:
                resourcesPath += "菌弹";
                break;
            case BulletType.粒弹:
                resourcesPath += "粒弹";
                break;
            case BulletType.小玉:
                resourcesPath += "小玉";
                break;
            case BulletType.环玉:
                resourcesPath += "环玉";
                break;
            case BulletType.米弹:
                resourcesPath += "米弹";
                break;
            case BulletType.链弹:
                resourcesPath += "链弹";
                break;
            case BulletType.针弹:
                resourcesPath += "针弹";
                break;
            case BulletType.扎弹:
                resourcesPath += "扎弹";
                break;
            case BulletType.鳞弹:
                resourcesPath += "鳞弹";
                break;
            case BulletType.铳弹:
                resourcesPath += "铳弹";
                break;
            case BulletType.杆菌弹:
                resourcesPath += "杆菌弹";
                break;
            case BulletType.小星弹:
                resourcesPath += "小星弹";
                break;
            case BulletType.钱币:
                resourcesPath += "钱币";
                break;
            case BulletType.中玉:
                resourcesPath += "中玉";
                break;
            case BulletType.椭弹:
                resourcesPath += "椭弹";
                break;
            case BulletType.刀弹:
                resourcesPath += "刀弹";
                break;
            case BulletType.蝶弹:
                resourcesPath += "蝶弹";
                break;
            case BulletType.大星弹:
                resourcesPath += "大星弹";
                break;
            case BulletType.水光弹:
                resourcesPath += "水光弹";
                break;
            case BulletType.炎弹:
                resourcesPath += "炎弹";
                break;
            case BulletType.心弹:
                resourcesPath += "心弹";
                break;
            case BulletType.滴弹:
                resourcesPath += "滴弹";
                break;
            case BulletType.箭弹:
                resourcesPath += "箭弹";
                break;
            case BulletType.大玉:
                resourcesPath += "大玉";
                break;
            case BulletType.蔷薇:
                resourcesPath += "蔷薇";
                break;
            case BulletType.光玉:
                resourcesPath += "光玉";
                break;
            case BulletType.音符:
                resourcesPath += "音符";
                break;
            case BulletType.休止符:
                resourcesPath += "休止符";
                break;
            default:
                break;
        }
        resourcesPath += $" ({color})";
        Texture2D texture = (Texture2D)Resources.Load(resourcesPath);
        return texture;
    }

    public static void SetBulletBox(BulletType bulletType, Box box)
    {
        //此处设置原作子弹判定（像素），记得换算！！
        switch (bulletType)
        {
            case BulletType.点弹:
            case BulletType.菌弹:
            case BulletType.米弹:
            case BulletType.链弹:
            case BulletType.针弹:
            case BulletType.鳞弹:
            case BulletType.铳弹:
            case BulletType.杆菌弹:
            case BulletType.滴弹:
                box.Type = BoxType.Round;
                box.R = 2.4f;
                break;
            case BulletType.粒弹:
                box.Type = BoxType.Round;
                box.R = 2.0f;
                break;
            case BulletType.小玉:
            case BulletType.环玉:
            case BulletType.小星弹:
            case BulletType.钱币:
            case BulletType.炎弹:
            case BulletType.箭弹:
            case BulletType.音符:
                box.Type = BoxType.Round;
                box.R = 4.0f;
                break;
            case BulletType.扎弹:
                box.Type = BoxType.Round;
                box.R = 2.8f;
                break;
            case BulletType.中玉:
                box.Type = BoxType.Round;
                box.R = 8.5f;
                break;
            case BulletType.椭弹:
            case BulletType.蝶弹:
            case BulletType.大星弹:
                box.Type = BoxType.Round;
                box.R = 7.0f;
                break;
            case BulletType.刀弹:
            case BulletType.水光弹:
                box.Type = BoxType.Round;
                box.R = 6.0f;
                break;
            case BulletType.心弹:
                box.Type = BoxType.Round;
                box.R = 10.0f;
                break;
            case BulletType.大玉:
            case BulletType.蔷薇:
                box.Type = BoxType.Round;
                box.R = 14.0f;
                break;
            case BulletType.光玉:
                box.Type = BoxType.Round;
                box.R = 12.0f;
                break;             
            case BulletType.休止符:
                box.Type = BoxType.Round;
                box.R = 5.0f;
                break;
            default:
                box.Type = BoxType.None;
                break;
        }

        //换算
        box.R /= 50.0f;
    }

    /// <summary>
    /// 水平翻转
    /// </summary>
    public static Texture2D horizontalFlipPic(Texture2D texture2d)
    {
        int width = texture2d.width;//得到图片的宽度.   
        int height = texture2d.height;//得到图片的高度 

        Texture2D NewTexture2d = new Texture2D(width, height);//创建一张同等大小的空白图片 

        int i = 0;

        while (i < width)
        {
            NewTexture2d.SetPixels(i, 0, 1, height, texture2d.GetPixels(width - i - 1, 0, 1, height));
            i++;
        }
        NewTexture2d.Apply();

        return NewTexture2d;
    }

    public enum BulletType
    {
        点弹,
        菌弹,
        粒弹,
        小玉,
        环玉,
        米弹,
        链弹,
        针弹,
        扎弹,
        鳞弹,
        铳弹,
        杆菌弹,
        小星弹,
        钱币,
        中玉,
        椭弹,
        刀弹,
        蝶弹,
        大星弹,
        水光弹,
        炎弹,
        心弹,
        滴弹,
        箭弹,
        大玉,
        蔷薇,
        光玉,
        音符,
        休止符
    }
}

public class MissEventArgs : EventArgs { }
