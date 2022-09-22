using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Enemy
    {
        public List<Transform> enemyPositions;
    }
    [SerializeField] private List<Enemy> enemySpawn;
    int curSpawn;
    int indexPos=0;
    int indexSpawn = 0;
    [SerializeField] private GameObject EnemyPrefab;

    public static EnemySpawner Instance;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        curSpawn = enemySpawn[indexSpawn].enemyPositions.Count;
        GameManager.Instance.IsGaming += PositionEnemyOnPlace;
      
    }

    void PositionEnemyOnPlace()
    {
        foreach(Enemy spawn in enemySpawn)
        {
            foreach(Transform pos in spawn.enemyPositions)
            {
                GameObject enemy = Instantiate(EnemyPrefab, pos.position, pos.rotation);
            }
           
        }
    }

    public  void HitEnemy()
    {
        indexPos++;
        if (indexPos >= curSpawn)
        {
            indexPos = 0;
            
            if (indexSpawn < enemySpawn.Count-1)
            {
                indexSpawn++;
                curSpawn = enemySpawn[indexSpawn].enemyPositions.Count;
               
            }

            GameManager.Instance.Move();
        }
        else
        {
            GameManager.Instance.Hit();
        }

    }

    public Transform EnemyForward()
    {

        return enemySpawn[indexSpawn].enemyPositions[indexPos];
    }
}
