using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; } //Only instance of this class to become a singleton
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public EnemyData enemyData;

    public void InitEnemyManager()
    {
        enemyData.InitEnemyData();
    }
}