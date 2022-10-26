using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private float _delta;

    private AudioSource _audioSource;
    private bool _isInHouse;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _currentVolume;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isInHouse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isInHouse = false;
        }
    }

    private void Update()
    {
        _currentVolume = _audioSource.volume;

        if (_isInHouse)
        {
            _audioSource.volume = Mathf.MoveTowards(_currentVolume, _maxVolume, _delta * Time.deltaTime);
        }
        else
        {
            _audioSource.volume = Mathf.MoveTowards(_currentVolume, _minVolume, _delta * Time.deltaTime);
        }
    }
}
