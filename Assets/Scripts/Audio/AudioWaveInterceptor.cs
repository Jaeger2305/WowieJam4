using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWaveInterceptor : MonoBehaviour
{
    [SerializeField] WaveRunner _wave;
    public void NewWave()
    {
        switch (_wave.GetCurrentWaveData().WaveID) {
            case 0:
                break;
            case 1:
                StartWave1();
                break;
            case 2:
                MusicController.Instance.TriggerSecondary();
                break;
            case 3:
                StartWave3();
                break;
            default:
                break;
        }
    }
    public void StartWave1()
    {
        MusicController.Instance.CombatWaveStart();
    }
    public void StartWave3()
    {
        MusicController.Instance.FinalWaveStart();
    }
}
