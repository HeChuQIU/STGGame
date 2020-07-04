using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class Box : MonoBehaviour
{
    public enum BoxType
    {
        None,
        Round,
        Square
    }
    public BoxType Type { get; set; } = BoxType.None;
    public Vector3 Position { get => transform.position; }
    public float R { get; set; } = 0f;
    public float Width { get; set; } = 0f;
    public float Height { get; set; } = 0f;

    void Update()
    {
        
    }

    public static bool Miss(Box box1, Box box2)
    {
        if (box1.Type == BoxType.None || box2.Type == BoxType.None) 
        {
            return false;
        }
        if (box1.Type == BoxType.Round && box2.Type == BoxType.Round) 
        {
            if (Vector2.Distance(box1.Position, box2.Position) < Mathf.Sqrt(((box1.R) * (box1.R)) + ((box2.R) * (box2.R))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (box1.Type == BoxType.Round && box2.Type == BoxType.Square) 
        {
            if (Vector2.Distance(box1.Position, box2.Position) < (Sin(Bullet.GetAngle((Vector2) box2.Position -(Vector2) box1.Position)) * box1.R) + (box2.Height / 2)
                || Vector2.Distance(box1.Position, box2.Position) < (Cos(Bullet.GetAngle((Vector2)box2.Position - (Vector2)box1.Position)) * box1.R) + (box2.Width / 2)) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (box1.Type == BoxType.Square && box2.Type == BoxType.Square) 
        {
            if (Vector2.Distance(box1.Position,box2.Position)<(box1.Height/2)+(box2.Height/2)
                || Vector2.Distance(box1.Position, box2.Position) < (box1.Width / 2) + (box2.Width / 2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (box1.Type == BoxType.Square && box2.Type == BoxType.Round) 
        {
            if (Vector2.Distance(box1.Position, box2.Position) < (Sin(Bullet.GetAngle((Vector2)box1.Position - (Vector2)box2.Position)) * box2.R) + (box1.Height / 2)
                || Vector2.Distance(box2.Position, box1.Position) < (Cos(Bullet.GetAngle((Vector2)box1.Position - (Vector2)box2.Position)) * box2.R) + (box1.Width / 2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
