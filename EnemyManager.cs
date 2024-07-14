using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> eneimesInTrigger = new List<Enemy>();

    public void AddEnemy(Enemy enemy)
    {
        eneimesInTrigger.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        eneimesInTrigger.Remove(enemy);
    }
}
