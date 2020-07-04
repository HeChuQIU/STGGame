using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public Player player;
    自机狙符卡 SC1;
    ulong count = 0;
    public Game.GameDifficulty gameDifficulty;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (count == 0) 
        {
            GameObject 灵梦 = new GameObject("博丽灵梦");
            灵梦.transform.position = new Vector3(0f, 2f);
            灵梦.transform.localScale = new Vector3(2f, 2f, 2f);
            博丽灵梦 博丽灵梦 = 灵梦.AddComponent<博丽灵梦>();
            博丽灵梦.Difficulty = gameDifficulty;
            博丽灵梦.player = player;
        }
        count++;
    }

    public void AudioWAV(string name)
    {
        audioSource.clip = Resources.Load<AudioClip>($"音效/{name}");
        audioSource.Play();
    }
}
