using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform rightBound;
    [SerializeField] SwimGameManager gameManager;
    [SerializeField] float followSpeed;

    AthleteFSM player;

    private void Awake()
    {
        player = gameManager.GetPlayer();
    }
    private void Update()
    {
        if(transform.position.x < rightBound.position.x)
        {
            if(player.transform.position.x > transform.position.x)
            {
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z), Time.deltaTime * followSpeed);
            }
        }
    }
}
