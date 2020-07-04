using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotat : MonoBehaviour
{
    /// <summary>
    /// 是否旋转
    /// </summary>
    public bool Enabled = false;
    public float Speed = 0f;
    public float Angle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(0, 0, Angle));
    }

    // Update is called once per frame
    void Update()
    {
        if (Enabled)
        {
            transform.Rotate(new Vector3(0, 0, Speed));
        }
    }
}
