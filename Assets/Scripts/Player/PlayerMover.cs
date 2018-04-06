using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class PlayerMover : MonoBehaviour
{
    private Transform playerTransform;
    private Camera mainCam;
    private Vector2 playerSize;
    

    private void Start()
    {
        Init();
    }
    
	
	void Update ()
    {
        if (GameManager.IsGamePaused) return;

        Vector3 tmpMousePosition = Vector3.zero;


        tmpMousePosition.x = Mathf.Clamp (Input.mousePosition.x, playerSize.x / 2, Screen.width - playerSize.x / 2);
        tmpMousePosition.y = playerSize.y;
        tmpMousePosition = mainCam.ScreenToWorldPoint(tmpMousePosition);

        //tmpMousePosition.y = 0;

        playerTransform.position = tmpMousePosition;
	}

    private void Init()
    {
        this.playerTransform = transform;
        this.playerSize = GetComponent<SpriteRenderer>().size;
        mainCam = Camera.main; //Unity does not cache Camera.main
    }
}
