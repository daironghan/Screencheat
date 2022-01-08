using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class ScoreBoardItem : MonoBehaviour
{
    public TMP_Text playerText;
    public TMP_Text scoreText;
    public void Initialize(Player player)
    {
        playerText.text = player.NickName;
    }

}
