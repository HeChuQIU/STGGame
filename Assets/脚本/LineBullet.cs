using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STGBaselib;
using System;

public class LineBullet : Bullet
{
    public float faceAngle;

    public bool RemoveWhenInvisible = true;
    public override float FaceAngle => faceAngle;
    bool onClear = false;

    public override event EventHandler<MissEventArgs> Miss;

    public override Vector3 OriginVector { get; set; }

    public override Vector3 NowVector { get; protected set; }

    public override Vector3 NextVector => transform.position + new Vector3(Mathf.Cos(FaceAngle) * Speed, Mathf.Sin(FaceAngle) * Speed);

    public override bool IsRotatToFace { get; set; } = true;
    public float Speed { get; set; }
    public override Player player { get; set; }
    public Action onBecameInvisible { get; set; }

    public void SetFaceAngle(float angle) => faceAngle = angle;
    // Start is called before the first frame update
    void Start()
    {
        if (player != null) 
        {
            Miss += player.Miss;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = NextVector;
        if (IsRotatToFace)
        {
            transform.Rotate(-transform.rotation.eulerAngles);
            transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * FaceAngle - 90));
        }
        if (player != null) 
        {
            if (Box.Miss(this, player))
            {
                Miss?.Invoke(this, new MissEventArgs());
                Destroy(gameObject);
            }
        }
        if (onClear) 
        {
            Destroy(gameObject);
        }
    }

    public Vector2 SpeedToVector2()
    {
        return new Vector2(Speed * Mathf.Cos(FaceAngle), Speed * Mathf.Sin(FaceAngle));
    }

    public void SetSpeedWithVector2(Vector2 vector2)
    {
        SetFaceAngle(Bullet.GetAngle(vector2));
        Speed = vector2.magnitude;
    }
    private void OnBecameInvisible()
    {
        onBecameInvisible?.Invoke();
        if (RemoveWhenInvisible)
        {
            Destroy(gameObject);
        }
    }

    public override void Clear(object sender, ClearBulletEventArgs e)
    {
        onClear = true;
    }
}
