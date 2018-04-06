using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherMover : MonoBehaviour
{
    public float Speed = 0.2f;
    private Camera mainCam;

    void Start ()
    {
        GameManager.OnGameRestarted += GameManager_OnGameRestarted;
        GameManager.OnGamePaused += GameManager_OnGamePaused;
        mainCam = Camera.main;

    }

    private void GameManager_OnGamePaused(bool obj)
    {
        if (obj)
            CancelInvoke();
        else
            InvokeRepeating("Move", 1f, 0.25f);
    }

    private void GameManager_OnGameRestarted()
    {
        InvokeRepeating("Move", 1f, 0.25f);
    }

    void Move ()
    {
        transform.position += Vector3.right * Speed;

        var invScreenPos = mainCam.WorldToScreenPoint(transform.position);
        var invWidth = GetComponent<SpriteRenderer>().size.x;

        if (invScreenPos.x <= 0 || invScreenPos.x >= Screen.width)
        {
            Speed = -Speed;
            transform.position += Vector3.down * 0.6f;
            return;
        }
    }
}
