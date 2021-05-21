using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform followTarget;

    private CinemachineVirtualCamera vcam;
    private GameManager gameManager;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        Invoke("FollowPlayer", 0.1f);
    }

    public void FollowPlayer()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();

        playerPrefab = gameManager.player;

        if (playerPrefab == null)
        {
            playerPrefab = GameObject.FindWithTag("Player");
        }
        followTarget = playerPrefab.transform;
        vcam.Follow = followTarget;
        vcam.LookAt = followTarget;
    }
}
