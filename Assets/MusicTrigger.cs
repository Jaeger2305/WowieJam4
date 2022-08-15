using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public void GameWon()
    {
        MusicController.Instance.PlayStingerGameWon();
    }
    public void GameLost()
    {
        MusicController.Instance.PlayStingerGameLost();
    }
}
