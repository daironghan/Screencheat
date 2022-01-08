using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    //detect when switch scene and instantiate player manager
    private void Awake()
    {
        if(Instance)//check if another room manager exists
        {
            Destroy(gameObject); 
            return;
        }
        DontDestroyOnLoad(gameObject);//only one
        Instance = this;
    }
    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 1)//game scene, instantiate 
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "player"), new Vector3(-0.8661098f, 3.475175f, 18.20544f), Quaternion.identity);//Spawn point
        }
    }
    void Start()
    {

    }


    void Update()
    {
        
    }
}
