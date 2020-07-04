using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class SquareFunctionBullet : Bullet
{ 
    public float A { get; set; } = 0;
    public float H { get; set; } = 0;
    public float K { get; set; } = 0;
    public float Speed { get; set; } = 0;
    public float X { get; set; } = 0;
    public float Y { get; set; } = 0;

    public Vector3 LastVector;

    public override event EventHandler<MissEventArgs> Miss;

    /// <summary>
    /// 旋转角度
    /// </summary>
    public float Rad { get; set; }
    public override Vector3 OriginVector { get; set; }

    public override Vector3 NextVector => Rotating(new Vector3(X + Speed, A * (X + Speed - H) * (X + Speed - H) + K), Rad);

    public override bool IsRotatToFace { get; set; }
    
    public override Vector3 NowVector {
        get => Rotating(new Vector3(X, A * (X - H) * (X - H) + K), Rad);
        protected set { }         
    }

    public override Player player { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    // Start is called before the first frame update
    void Start()
    {
        Y = A * (X - H) * (X - H) + K;
    }

    // Update is called once per frame
    void Update()
    {       
        X += Speed;
        Y = A * (X - H) * (X - H) + K;
        gameObject.transform.position = GetNowVector3();
        if (IsRotatToFace)
        {
            transform.rotation = new Quaternion() { eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * FaceAngle - 90) };
        }
    }
    public Vector3 GetNowVector3() 
    {
        return Rotating(new Vector3(X, Y), Rad);
    }
    public static float Angle(Vector3 from,Vector3 to)
    {
        return Asin((to.y - from.y) / Vector3.Distance(to, from) * Rad2Deg);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    public static Vector3  Rotating(Vector3 vector,float rad)
    {
        return new Vector3(((float)Cos(rad)) * vector.x - ((float)Sin(rad)) * vector.y, ((float)Cos(rad)) * vector.y + ((float)Sin(rad)) * vector.x);
    }

    public override void Clear(object sender, ClearBulletEventArgs e)
    {
        Destroy(gameObject);
    }
}
