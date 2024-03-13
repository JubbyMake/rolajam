using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMHandler : MonoBehaviour
{
    [SerializeField] private AudioClip _bgmReal;
    [SerializeField] private AudioClip _bgmFake;

    private bool _isPlaying;

    public static BGMHandler Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(Instance);

        Instance = this;

        var temp = GetComponent<AudioSource>();

        var num = Random.Range(0, 500);

        Debug.Log(num);

        if(num == 69)
            temp.clip = _bgmReal;
        else
            temp.clip = _bgmFake;

        temp.loop = true;
        temp.Play();

        _isPlaying = true;
    }

    public void StopNow()
    {
        var temp = GetComponent<AudioSource>();

        temp.Stop();

        _isPlaying = false;
    }

    public void StartBGM()
    {
        if(!_isPlaying)
            Awake();
    }
}
