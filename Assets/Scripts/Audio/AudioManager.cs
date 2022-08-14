using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    //Lazy singleton
    public static AudioManager ie;

    [SerializeField] AudioSource _globalAudioSource;
    [SerializeField] float _volumeScale;
    [SerializeField] int _maxSimultaneousUISounds;
    [SerializeField] int _maxSimultaneousStingers;
    [SerializeField] int _maxSimultaneousWorldSounds;

    int _activeUISounds;
    int _activeStingers;
    int _activeWorldSounds;

    private void Awake()
    {
        ie = this;
    }

    public void PlayGlobalOneShot(AudioClip clip)
    {
        _globalAudioSource.PlayOneShot(clip, _volumeScale);
    }

    public bool RequestSoundClearanceUI(float soundClipDuration)
    {
        if (_activeUISounds < _maxSimultaneousUISounds) {
            _activeUISounds++;
            StartCoroutine(ClearUIClip(soundClipDuration));
            return true;
        }
        else {
            return false;
        }
    }

    public bool RequestSoundClearanceWorld(float soundClipDuration)
    {
        if (_activeWorldSounds < _maxSimultaneousWorldSounds) {
            _activeWorldSounds++;
            StartCoroutine(ClearWorldClip(soundClipDuration));
            return true;
        }
        else {
            return false;
        }
    }

    public bool RequestSoundClearanceStinger(float soundClipDuration)
    {
        if (_activeStingers < _maxSimultaneousStingers) {
            _activeStingers++;
            StartCoroutine(ClearStinger(soundClipDuration));
            return true;
        }
        else {
            return false;
        }
    }

    IEnumerator ClearWorldClip(float s)
    {
        yield return new WaitForSeconds(s);

        _activeWorldSounds--;
    }
    IEnumerator ClearUIClip(float s)
    {
        yield return new WaitForSeconds(s);

        _activeUISounds--;
    }

    IEnumerator ClearStinger(float s)
    {
        yield return new WaitForSeconds(s);

        _activeStingers--;
    }
}
