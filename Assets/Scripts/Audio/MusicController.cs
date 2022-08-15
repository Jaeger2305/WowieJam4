using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MusicGroup 
{
    public AudioClip Intro;
    public AudioClip MainLoop;
    public AudioClip AdditionLoop;

}

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;

    [Header("Tracklist")]
    [SerializeField] AudioClip _titleIntro;
    [SerializeField] AudioClip _titleLoop;
    [SerializeField] AudioClip _gameTrack1;
    [SerializeField] AudioClip _gameTrack1Add;
    [SerializeField] AudioClip _combatTrack1;
    [SerializeField] AudioClip _combatTrack1Add;
    [SerializeField] AudioClip _combatTrack2;
    [SerializeField] AudioClip _combatTrack2Add;
    [Header("Stingers")]
    [SerializeField] AudioClip _stingerVictory;
    [SerializeField] AudioClip _loopVictory;
    [SerializeField] AudioClip _stingerDefeat;
    [Header("Track Settings")]
    [SerializeField] float _gamePlayTransitionDuration;
    [SerializeField] float _combatWaveTransitionDuration;
    [Header("Controller Settings")]
    [SerializeField][Range(0.01f,1f)] float _musicVolumeMult = 1f;
    [SerializeField] AudioSource _musicSourcePrimary;
    [SerializeField] AudioSource _musicSourceSecondary;
    [SerializeField] AudioSource _musicSourceOneShots;
    [SerializeField] AnimationCurve _volFadeInCurve;
    [SerializeField] AnimationCurve _volFadeOutCurve;
    [SerializeField] float _defaultFadeDuration;
    [SerializeField] float _defaultCrossfadeDuration;
    [SerializeField] float _gameEndFadeDuration;

    float _timeFadeStart;
    float _timePrimaryStart;
    float _timeSecondaryStart;
    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }
    private void Start()
    {
        //Start Title Music
        StartCoroutine(OneShotIntroLoop(_musicSourcePrimary, _titleIntro, _titleLoop, _titleIntro.length*0.6f));
    }

    #region Music Triggers
    public void GameStartTriggerMusic()
    {
        StartCoroutine(FadeIntoSynchronous(_gamePlayTransitionDuration, _gameTrack1, _gameTrack1Add, true, _defaultFadeDuration));
    }

    public void CombatWaveStart()
    {
        StartCoroutine(FadeIntoSynchronous(_combatWaveTransitionDuration, _combatTrack1, _combatTrack1Add, true, _defaultFadeDuration));
    }
    public void TriggerSecondary()
    {
        StartCoroutine(FadeVolume(true, _musicSourceSecondary, Time.time, _defaultFadeDuration));
    }
    public void FinalWaveStart()
    {
        StartCoroutine(FadeIntoSynchronous(_combatWaveTransitionDuration, _combatTrack2, _combatTrack2Add, false, _defaultFadeDuration));
    }
    #endregion
    #region Stingers
    public void PlayStingerGameWon()
    {
        StartCoroutine(FadeVolume(false, _musicSourcePrimary, Time.time, _gameEndFadeDuration));
        StartCoroutine(FadeVolume(false, _musicSourceSecondary, Time.time, _gameEndFadeDuration));
        //SelfDestructStinger(_stingerVictory);
        StartCoroutine(OneShotIntroLoop(_musicSourcePrimary, _stingerVictory, _loopVictory, _defaultCrossfadeDuration));
        
    }
    public void PlayStingerGameLost()
    {
        StartCoroutine(FadeVolume(false, _musicSourcePrimary, Time.time, _gameEndFadeDuration));
        StartCoroutine(FadeVolume(false, _musicSourceSecondary, Time.time, _gameEndFadeDuration));
        //SelfDestructStinger(_stingerDefeat);
        _musicSourceOneShots.PlayOneShot(_stingerDefeat, _musicVolumeMult);
    }
    #endregion
    #region General Music Control
    public void MusicPlayPrimary(bool fadeIn, bool loop, AudioClip track = null)
    {
        track = track == null ? _musicSourcePrimary.clip : track;
        _musicSourcePrimary.clip = track;
        _musicSourcePrimary.loop = loop;
        _musicSourcePrimary.Play();
        _timePrimaryStart = Time.time;
        if (!fadeIn) return;
        _timeFadeStart = Time.time;
        FadeVolume(true, _musicSourcePrimary, _timeFadeStart, _defaultFadeDuration);
    }

    public void MusicPlaySecondary(bool fadeIn, bool loop, AudioClip track = null)
    {
        track = track == null ? _musicSourcePrimary.clip : track;
        _musicSourceSecondary.clip = track;
        _musicSourceSecondary.loop = loop;
        _musicSourceSecondary.Play();
        _timeSecondaryStart = Time.time;
        if (!fadeIn) return;
        _timeFadeStart = Time.time;
        FadeVolume(true, _musicSourceSecondary, _timeFadeStart, _defaultFadeDuration);
    }

    IEnumerator QueueTrack(AudioSource source, AudioClip nextTrack, float playAfterSeconds, bool crossfade, bool loop)
    {
        float s = playAfterSeconds;
        if (crossfade) s = s - _defaultCrossfadeDuration;

        yield return new WaitForSeconds(s);

        source.loop = loop;

        if (!crossfade) {
            source.clip = nextTrack;
            source.Play();
            yield break;
        }
        StartCoroutine(Crossfade(source, nextTrack, Time.time));
    }

    IEnumerator FadeVolume(bool fadeIn, AudioSource source, float startTime, float duration)
    {
        print("music start fade volume");
        while (Time.time < startTime + duration) {
            float t = (Time.time - startTime) / duration;   
            source.volume = fadeIn ? _volFadeInCurve.Evaluate(t) : _volFadeOutCurve.Evaluate(t);
            source.volume *= _musicVolumeMult;
            yield return null;
        }
        source.volume = fadeIn ? _volFadeInCurve.Evaluate(1f) : _volFadeOutCurve.Evaluate(1f);
        print("music end fade volume");
    }

    IEnumerator Crossfade(AudioSource source, AudioClip nextTrack, float startTime)
    {
        print("music: start crossfade");
        
        while (Time.time < startTime + _defaultCrossfadeDuration) {
            float t = (Time.time - startTime) / _defaultCrossfadeDuration;
            source.volume = _volFadeOutCurve.Evaluate(t) * _musicVolumeMult;
            yield return null;
        }

        //Fade out complete
        print("music: end crossfade");
        source.clip = nextTrack;
        source.Play();
        StartCoroutine(FadeVolume(true, source, Time.time, _defaultFadeDuration));
    }

    
    IEnumerator OneShotIntroLoop(AudioSource source, AudioClip oneShot, AudioClip nextTrack, float fadeDuration)
    {
        _musicSourceOneShots.PlayOneShot(oneShot, _musicVolumeMult);

        yield return new WaitForSeconds(oneShot.length - fadeDuration);

        source.clip = nextTrack;
        source.loop = true;
        source.Play();
        StartCoroutine(FadeVolume(true, source, Time.time, fadeDuration));
    }

    IEnumerator FadeIntoSynchronous(float initialDelay, AudioClip primary, AudioClip secondary, bool muteSecondary, float totalFadeDuration)
    {
        yield return new WaitForSeconds(initialDelay);

        float syncTime = Time.time;

        StartCoroutine(FadeVolume(false, _musicSourcePrimary, syncTime, totalFadeDuration * 0.5f));
        StartCoroutine(FadeVolume(false, _musicSourceSecondary, syncTime, totalFadeDuration * 0.5f));

        yield return new WaitForSeconds(totalFadeDuration * 0.5f);

        _musicSourcePrimary.clip = primary;
        _musicSourceSecondary.clip = secondary;
        _musicSourcePrimary.Play();
        _musicSourceSecondary.Play();
        StartCoroutine(FadeVolume(true, _musicSourcePrimary, Time.time, totalFadeDuration * 0.5f));
    }

    IEnumerator SelfDestructStinger(AudioClip stinger)
    {
        //I wonder if we can even do this
        float s = stinger.length;
        _musicSourceOneShots.PlayOneShot(stinger);

        yield return new WaitForSeconds(s);

        Destroy(gameObject);    //There can be only one...
    }

    #endregion
}
