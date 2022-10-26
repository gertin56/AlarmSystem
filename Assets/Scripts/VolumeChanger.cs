using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private float _delta;

    private AudioSource _audioSource;
    private bool _isIncreases = false;
    private Coroutine _increaseVolumeCorutine;
    private Coroutine _decreaseVolumeCorutine;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _currentVolume;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void StartIncreaseVolume()
    {
        if (_isIncreases == false && _decreaseVolumeCorutine != null)
        {
            StopCoroutine(_decreaseVolumeCorutine);
        }
        
        _increaseVolumeCorutine = StartCoroutine(IncreaseVolume());
    }

    public void StartDecreaseVolume()
    {
        if (_isIncreases == true && _increaseVolumeCorutine != null)
        {
            StopCoroutine(_increaseVolumeCorutine);
        }
        
        _decreaseVolumeCorutine = StartCoroutine(DecreaseVolume());
    }

    private IEnumerator IncreaseVolume()
    {
        _isIncreases = true;

        while (_currentVolume < _maxVolume)
        {
            _currentVolume = _audioSource.volume;
            _audioSource.volume = Mathf.MoveTowards(_currentVolume, _maxVolume, _delta * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator DecreaseVolume()
    {
        _isIncreases = false;

        while (_currentVolume > _minVolume)
        {
            _currentVolume = _audioSource.volume;
            _audioSource.volume = Mathf.MoveTowards(_currentVolume, _minVolume, _delta * Time.deltaTime);
            yield return null;
        }
    }
}
