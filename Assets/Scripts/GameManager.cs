using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameRestarted;
    public static event Action<bool> OnGamePaused;
    public float Score
    {
        get { return score; }
        set
        {
            score = value;
            uiController.UpdateScore(value);
        }
    } 
    public static bool IsGamePaused
    {
        get { return isGamePaused; }
        set
        {
            isGamePaused = value;

            if (OnGamePaused != null)
                OnGamePaused(value);
        }
    }

    [SerializeField] private UIController uiController;
    [SerializeField] private Player player;
   

    public static int LiveInvadersCount;
    public Bullet BulletPrefab;

    private float score;
    private static bool isGamePaused = true;
    private List<IHittable> invaders = new List<IHittable>();
   

    //Called from Restart button
    public void RestartGame()
    {
        if (OnGameRestarted != null)
            OnGameRestarted();

        uiController.HideUIPanel();

        Score = 0;
        IsGamePaused = false;
        player.SetDefaultSettings();
    }


    void Awake ()
    {
        LiveInvadersCount = invaders.Count;

        player.OnDeadEvent += HandleOnDeadEvent;
        player.OnHitpointsChanged += Player_OnHitpointsChanged;
    }

    private void Player_OnHitpointsChanged(float obj)
    {
        uiController.UpdateLives(obj);
    }

    private void OnDestroy()
    {
        player.OnDeadEvent -= HandleOnDeadEvent;
        player.OnHitpointsChanged -= Player_OnHitpointsChanged;
    }

    public void SpawnInvaderMother ()
    {
        LiveInvadersCount++;
    }

    public void HandleOnDeadEvent(HittableType hittableType)
    {

        switch (hittableType)
        {
            case HittableType.SimpleInvader:
                Score += Constants.INVADER_BONUS_POINTS_SIMPLE;
                LiveInvadersCount--;
                break;
            case HittableType.MotherInvader:
                Score += Constants.INVADER_BONUS_POINTS_MOTHER;
                LiveInvadersCount--;
                break;
            case HittableType.ShootingInvader:
                Score += Constants.INVADER_BONUS_POINTS_SHOOTING;
                LiveInvadersCount--;
                break;
            case HittableType.Player:
                FinishGame (false);
                break;
            default:
                break;
        }

        if (LiveInvadersCount <= 0)
            FinishGame (true);
        
    }

    void FinishGame (bool isWin)
    {
        IsGamePaused = true;
        uiController.AfterGameScreenShow(isWin, Score);
    }
}
