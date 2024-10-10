using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource bgmSource;//播放bgm的音频

    private void Awake()
    {
        Instance = this;
    }

    //初始化
    public void Init()
    {
       bgmSource =  gameObject.AddComponent<AudioSource>();
    }

    //播放bgm
    public void PlayBGM(string name,bool isloop = true)
    {
        //加载声音剪辑
        AudioClip clip = Resources.Load<AudioClip>("Sounds/BGM/" + name);

        bgmSource.clip = clip;//设置音频

        bgmSource.loop = isloop;

        bgmSource.Play();


    }

    //播放音效
    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + name);

        AudioSource.PlayClipAtPoint(clip, transform.position);//播放
    }
}
