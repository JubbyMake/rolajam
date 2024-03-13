using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AudioPool : MonoBehaviour
{
    [SerializeField] private GameObject _audioSourcePrefab;

    private List<AudioSource> _sources = new();

    public static AudioPool Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(Instance);

        Instance = this;

        for(int i = 0; i < 10; i++)
        {
            var temp = Instantiate(_audioSourcePrefab, transform)
                .GetComponent<AudioSource>();

            temp.gameObject.SetActive(false);
            _sources.Add(temp);
        }
    }

    public static AudioSource GetSource()
    {
        foreach(var temp in Instance._sources)
        {
            if(temp.isActiveAndEnabled)
                continue;

            return temp;
        }

        var tempNew = Instantiate(Instance._audioSourcePrefab, Instance.transform)
            .GetComponent<AudioSource>();

        Instance._sources.Add(tempNew);

        return tempNew;
    }
}
