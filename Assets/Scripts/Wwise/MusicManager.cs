using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public MatchManager matchManager;
    public FightManager fightManager;
    private Player player1;
    private Player player2;
    private int highScore;

    private void Start()
    {
        player1 = fightManager.player1;
        player2 = fightManager.player2;
    }

    public void StartIntro()
    {
        CueManager.isCounting = true;

        //CF Schéma système musical
        if (player1.wins == 0 && player2.wins == 0)
        {
            AkSoundEngine.SetState("MusicToPlay", "ST_R0_Init");
        }
        else if (player1.wins == player2.wins)
        {
            if(player1.wins == 2)
            {
                AkSoundEngine.SetState("MusicToPlay", "ST_R2_Draw");
            }
            else if (matchManager.winnerID == 0)
            {
                AkSoundEngine.SetState("MusicToPlay", "ST_R1_LeadUS");
            }
            else if (matchManager.winnerID == 1)
            {
                AkSoundEngine.SetState("MusicToPlay", "ST_R1_LeadJP");
            }
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
            else if (matchManager.winnerID == 0 && highScore == 2)
            {
                AkSoundEngine.SetState("MusicToPlay", "ST_R2_LeadUS");
            }
            else if (matchManager.winnerID == 1 && highScore == 2)
            {
                AkSoundEngine.SetState("MusicToPlay", "ST_R2_LeadJP");
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
}
