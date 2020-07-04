using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Command : MonoBehaviour
{
    public ulong count = 0;
    public Action update,start;
    private List<int> ints = new List<int>();
    public List<int> Ints 
    {
        get 
        {
            if (ints == null) 
            {
                ints = new List<int>();
            }
            return ints;
        } set => ints = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        start?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        update?.Invoke();
        count++;
    }
}
