using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoundType
{
    UI,
    World,
    Stinger
}

public class SoundPlayer : MonoBehaviour
{
    AudioSource _as;
    AudioManager _am;

    private void Awake()
    {
        _am = AudioManager.ie;
        _as = GetComponent<AudioSource>();
    }

    public void TryPlaySound(AudioClip sfx, SoundType soundType, float volumeScale)
    {
        float s = sfx.length;
        switch (soundType) {
            case SoundType.UI:
                if (!_am.RequestSoundClearanceUI(s)) return;
                _am.PlayGlobalOneShot(sfx, volumeScale);
                break;
            case SoundType.World:
                if (!_am.RequestSoundClearanceWorld(s)) return;
                PlaySpatial(sfx, volumeScale);
                break;
            case SoundType.Stinger:
                if (!_am.RequestSoundClearanceStinger(s)) return;
                _am.PlayGlobalOneShot(sfx, volumeScale);
                break;
            default:
                break;
        }
    }

    //For quick events
    public void PlayCurrentWorldClip()
    {
        float s = _as.clip.length;
        if (!_am.RequestSoundClearanceWorld(s)) return;

        _as.Play();
    }

    void PlaySpatial(AudioClip sfx, float volumeScale)
    {
        _as.loop = false;
        _as.clip = sfx;
        _as.Play();
    }
}