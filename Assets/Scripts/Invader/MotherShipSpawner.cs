using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipSpawner : MonoBehaviour
{
    [SerializeField] Invader motherShipPrefab;
    [SerializeField] GameManager gameManager;
    [SerializeField] List<Transform> spawnPoints;

    Vector2 RandomTopPosition
    {
        get
        {
            var rand = Random.Range(0, spawnPoints.Count);
            
            return spawnPoints[rand].position;
        }
    }

	void Start ()
    {
        GameManager.OnGamePaused += GameManager_OnGamePaused;
    }
	
	void TrySpawnMothership ()
    {
        if (GameManager.IsGamePaused)
            GameManager.OnGamePaused += GameManager_OnGamePaused;

        var chanceToSpawn = Random.Range(0f, 10f);

        if (chanceToSpawn <= 4f && !GameManager.IsGamePaused)
        {
            SpawnMother();
        }		
	}

    private void GameManager_OnGamePaused(bool obj)
    {
        if (obj)
            InvokeRepeating("TrySpawnMothership", 4f, 4f);
        else
            CancelInvoke();
    }

    void SpawnMother ()
    {
        gameManager.SpawnInvaderMother();
        var yourMother = Instantiate(motherShipPrefab);
        yourMother.transform.position = RandomTopPosition;
        yourMother.hittableType = HittableType.MotherInvader;
        yourMother.OnDeadEvent += YourMother_OnDeadEvent;

    }

    private void YourMother_OnDeadEvent(HittableType obj)
    {
        gameManager.HandleOnDeadEvent(obj);
    }
}
