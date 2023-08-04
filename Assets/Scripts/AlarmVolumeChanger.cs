using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AlarmVolumeChanger : MonoBehaviour
{
    [SerializeField] private float _volumeChangeDuration = 10f;

    private AudioSource _audioSource;
    private float _delay = 1f;
    private float _maxVolume = 1f;
    private float _minVolume = 0;
    private Coroutine _volumeChanger;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ChangeVolume(bool isTurnUp)
    {
        if (isTurnUp)
        {
            StopVolumeChange();
            StartVolumeChange(_maxVolume);
        }
        //StartCoroutine(ChangeVolumeTo(_maxVolume));
        else
        {
            StopVolumeChange();
            StartVolumeChange(_minVolume);
        }
            //StartCoroutine(ChangeVolumeTo(_minVolume));
    }

    private void StartVolumeChange(float targetVolume)
    {
        _volumeChanger = StartCoroutine(ChangeVolumeTo(targetVolume));
    }

    private void StopVolumeChange()
    {
        if (_volumeChanger != null)
            StopCoroutine(_volumeChanger);
    }

    private IEnumerator ChangeVolumeTo(float targetVolume)
    {
        var wait = new WaitForSeconds(_delay);
        float currentVolume = _audioSource.volume;
        float stepTime = _delay / _volumeChangeDuration;

        if (targetVolume > _minVolume && _audioSource.isPlaying == false)        
            _audioSource.Play();

        while (currentVolume != targetVolume)
        {
            currentVolume = Mathf.MoveTowards(currentVolume, targetVolume, stepTime);
            _audioSource.volume = currentVolume;
            yield return wait;
        }

        if (targetVolume == _minVolume)
            _audioSource.Stop();
    }
}