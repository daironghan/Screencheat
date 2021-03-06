using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    void Awake()
    {
        PV = GetComponent<PhotonView>();   
    }
    void Start()
    {
        if(PV.IsMine)//photon view is owned by local player
        {
            CreateController();
        }
    }
    void Update()
    {
        
    }
    void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","player"), Vector3.zero, Quaternion.identity);
    }
}
