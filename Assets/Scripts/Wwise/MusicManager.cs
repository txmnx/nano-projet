using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AK.Wwise.Event MusicEvent;
    public GameObject Start_Music;
    public MatchManager matchManager;
    public FightManager fightManager;
    private Player player1;
    private Player player2;
    private int highScore;
    private int roundNbr;
    private string musicCueName;

    private void Start()
    {
        player1 = fightManager.player1;
        player2 = fightManager.player2;
        MusicEvent.Post(gameObject, (uint)AkCallbackType.AK_MusicSyncAll, MusicCallbackFunction, (uint)AkCallbackType.AK_MusicSyncUserCue);
    }

    public void StartIntro()
    {

        CueManager.isCounting = true;

        roundNbr = player1.wins + player2.wins;

        //Switch pour l'annonce du round
        switch (roundNbr)
        {
            case (0):
                AkSoundEngine.SetSwitch("Round", "Round1", Start_Music);
                break;
            case (1):
                AkSoundEngine.SetSwitch("Round", "Round2", Start_Music);
                break;
            case (2):
                AkSoundEngine.SetSwitch("Round", "FinalRound", Start_Music);
                break;
            case (3):
                AkSoundEngine.SetSwitch("Round", "Round4", Start_Music);
                break;
            case (4):
                AkSoundEngine.SetSwitch("Round", "FinalRound", Start_Music);
                break;
        }

        //CF Schéma système musical
        if (player1.wins == 0 && player2.wins == 0)
        {
            AkSoundEngine.SetState("MusicToPlay", "ST_R0_Init");
        }
        else if (player1.wins == 1 && player2.wins == 1)
        {
            AkSoundEngine.SetState("MusicToPlay", "ST_R2_Draw");
        }
        else
        {
            if (player1.wins> player2.wins)
            {
                highScore = player1.wins;
            }
            else
            {
                highScore = player2.wins;
            }

            if (matchManager.winnerID == 0 && highScore == 1)
            {
                AkSoundEngine.SetState("MusicToPlay", "ST_R1_LeadUS");
            }
            else if (matchManager.winnerID == 1 && highScore == 1)
            {
                AkSoundEngine.SetState("MusicToPlay", "ST_R1_LeadJP");
            }
        }
    }

    public void RoundWinUS()
    {
        CueManager.isCounting = true;
        AkSoundEngine.SetState("MusicToPlay", "ST_RoundWinUS");
        AkSoundEngine.SetState("RoundWinner", "US");
    }

    public void RoundWinJP()
    {
        CueManager.isCounting = true;
        AkSoundEngine.SetState("MusicToPlay", "ST_RoundWinJP");
        AkSoundEngine.SetState("RoundWinner", "JP");
    }

    public void WinUS()
    {
        CueManager.isCounting = true;
        AkSoundEngine.SetState("MusicToPlay", "ST_WinUS");
    }

    public void WinJP()
    {
        CueManager.isCounting = true;
        AkSoundEngine.SetState("MusicToPlay", "ST_WinJP");
    }

    public void MusicCallbackFunction(object in_cookie, AkCallbackType in_type, object in_info)
    {
        AkMusicSyncCallbackInfo musicInfo = in_info as AkMusicSyncCallbackInfo;
        musicCueName = musicInfo.userCueName;
        Debug.Log(musicCueName);
    }
}
