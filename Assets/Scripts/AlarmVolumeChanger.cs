using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmVolumeChanger : MonoBehaviour
{
    [SerializeField] private float _volumeChangeDuration = 10f;

    private AudioSource _audioSource;
    private float _delay = 1f;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ChangeVolume(bool isTurnUp)
    {
        if (isTurnUp)
            StartCoroutine(ChangeVolumeTo(1f));
        else
            StartCoroutine(ChangeVolumeTo(0));
    }

    private IEnumerator ChangeVolumeTo(float targetVolume)
    {
        var wait = new WaitForSeconds(_delay);
        float currentVolume = _audioSource.volume;
        float stepTime = _delay / _volumeChangeDuration;

        if (targetVolume > 0 && _audioSource.isPlaying == false)        
            _audioSource.Play();

        while (currentVolume != targetVolume)
        {
            currentVolume = Mathf.MoveTowards(currentVolume, targetVolume, stepTime);
            _audioSource.volume = currentVolume;
            yield return wait;
        }

        if (targetVolume == 0)
            _audioSource.Stop();
    }
}