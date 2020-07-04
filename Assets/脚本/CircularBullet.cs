using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class CircularBullet : Bullet
{
    public override Vector3 OriginVector { get; set; } = Vector3.zero;
    public override Vector3 NowVector { get; protected set; }
    public override Vector3 NextVector => (new Vector3(Cos(Angle + W), Sin(Angle + W)) * RailR) + OriginVector;
    public override bool IsRotatToFace { get; set; } = true;
    /// <summary>
    /// 半径
    /// </summary>
    public float RailR { get; set; } = 0f;
    /// <summary>
    /// 半径向外移动速度
    /// </summary>
    public float RSpeed { get; set; } = 0f;
    /// <summary>
    /// 当前旋转到的角度
    /// </summary>
    public float Angle { get; set; } = 0f;
    /// <summary>
    /// 角速度
    /// </summary>
    public float W { get; set; } = 0.01f;
    public override Player player { get; set; }

    public override event EventHandler<MissEventArgs> Miss;

    // Start is called before the first frame update
    void Start()
    {
        OriginVector = transform.position;
        NowVector = OriginVector + (new Vector3(Cos(Angle + W), Sin(Angle + W)) * RailR);
        if (player != null)
        {
            Miss += player.Miss;
        }
        NowVector = OriginVector;
    }

    // Update is called once per frame
    bool onDestroy = false;
    protected void Update()
    {
        NowVector = NextVector;
        transform.position = NowVector;
        RailR += RSpeed;
        Angle += W;
        if (IsRotatToFace)
        {
            transform.Rotate(-transform.rotation.eulerAngles);
            transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * FaceAngle - 90));
        }
        if (transform.position.x > 10 || transform.position.x < -10 || transform.position.y > 10 || transform.position.y < -10) 
        {
            Destroy(gameObject);
        }
        if (player != null)
        {
            if (Box.Miss(this, player))
            {
                Miss?.Invoke(this, new MissEventArgs());
                Destroy(gameObject);
            }
        }
        if (onDestroy)
        {
            Destroy(gameObject);
        }
    }

    public static float AngleTo360(float angle)
    {
        begin:
        if (angle > 360f) 
        {
            angle -= 360f;
        }
        if (angle < 0f) 
        {
            angle += 360f;
        }
        if (angle > 360f || angle < 0f) 
        {
            goto begin;
        }
        return angle;
    }

    public override void Clear(object sender, ClearBulletEventArgs e)
    {
        onDestroy = true;
    }
}
