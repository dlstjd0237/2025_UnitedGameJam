using BIS.Define;
using BIS.Pool;
using System;
using UnityEngine;

namespace BIS.Manager
{
    public class EnemyManager
    {
        public GameObject RandomEnemySpawn(Vector3 pos, Quaternion rot)
        {
            Array values = Enum.GetValues(typeof(EEnemyType));

            // Generate a random index
            System.Random random = new System.Random();
            int randomIndex = random.Next(values.Length);

            // Get the random enum value
            EEnemyType randomEnemy = (EEnemyType)values.GetValue(randomIndex);

            GameObject go = PoolManager.SpawnFromPool(randomEnemy.ToString(), pos, rot);
            return go;
        }
    }
}
