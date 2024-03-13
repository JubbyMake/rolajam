using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public sealed class AudioDisabler : MonoBehaviour
{
    private AudioSource _source;

    private void Awake() => _source = GetComponent<AudioSource>();

    private void OnEnable() => StartCoroutine(DoOnEnable());

    private IEnumerator DoOnEnable()
    {
        _source.Play();

        yield return new WaitForSecondsRealtime(0.1f);

        yield return new WaitWhile(() => _source.isPlaying);

        gameObject.SetActive(false);

        _source.volume = 1f;
        _source.pitch = 1f;
    }
}
