using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderController : MonoBehaviour
{
    public List <Invader> Invaders;
    private float speedMultiplier;
    [SerializeField] GameManager gameManager;

	void Start ()
    {
        GameManager.OnGameRestarted += GameManager_OnGameRestarted;
        var tmpInvaders = GetComponentsInChildren<Invader>();

        foreach (var inv in tmpInvaders)
        {
            inv.OnDeadEvent += Inv_OnDeadEvent;
            Invaders.Add(inv);
        }
        GameManager.LiveInvadersCount = Invaders.Count;
	}

    private void GameManager_OnGameRestarted()
    {
        foreach (var inv in Invaders)
        {
            inv.SetDefaultSettings();
        }
        GameManager.LiveInvadersCount = Invaders.Count;
    }

    private void Inv_OnDeadEvent(HittableType obj)
    {
        gameManager.HandleOnDeadEvent(obj);
    }


    
}
