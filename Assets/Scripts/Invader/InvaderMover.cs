using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderMover : MonoBehaviour
{
    public float Speed = Constants.INVADER_MOVESPEED;
    private Camera mainCam;

    void Start ()
    {
        GameManager.OnGameRestarted += GameManager_OnGameRestarted;
        GameManager.OnGamePaused += GameManager_OnGamePaused;
        mainCam = Camera.main;
    }

    private void OnDestroy()
    {
        GameManager.OnGameRestarted -= GameManager_OnGameRestarted;
        GameManager.OnGamePaused -= GameManager_OnGamePaused;
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
        Speed = Constants.INVADER_MOVESPEED;
        InvokeRepeating("Move", 1f, 0.25f);
    }

    void Move()
    {
        transform.position += Vector3.right * Speed;

        foreach (Transform invader in transform)
        {
            if (invader.GetComponent<IHittable>().HitPoints == 0) continue;

            var invScreenPos = mainCam.WorldToScreenPoint(invader.transform.position);
            var invWidth = invader.GetComponent<SpriteRenderer>().size.x;

            if (invScreenPos.x <= invWidth || invScreenPos.x >= Screen.width - invWidth)
            {
                Speed = -Speed * Constants.INVADER_SPEED_MULTIPLIER;
                transform.position += Vector3.down * 0.6f;
                return;
            }
        }
        if (GameManager.IsGamePaused)
            CancelInvoke();

    }
}
