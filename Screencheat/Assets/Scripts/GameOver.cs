using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameOver inst;
    //[SerializeField] Canvas win;
    //[SerializeField] static Canvas lose;
    private void Awake()
    {
        inst = this;
    }
    void Start()
    {

    }
    void Update()
    {
        
    }
    public static void Win()
    {
        //win.gameObject.SetActive(true);
        //PhotonNetwork.Instantiate(Path.Combine("Prefabs", "GameOverWin"), Vector3.zero, Quaternion.identity);
    }
    public static void Lose()
    {
        //lose.gameObject.SetActive(true);
    }
    public void back()
    {
        Debug.Log("backkk");
        PhotonNetwork.LoadLevel(0);
        
    }
}
