using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text text;
    Player player;
    public void SetUp(Player playerName)
    {
        player = playerName;
        text.text = playerName.NickName;
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {   
        //if player left room
        if(player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }
    public override void OnLeftRoom()
    {
        //if player left room
        Destroy(gameObject);
    }
}
