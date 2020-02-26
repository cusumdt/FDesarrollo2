using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemySystem : MonoBehaviour
{
    public static UnityEvent EnemyDefeat = new UnityEvent();
    public void DestroyEnemy()
    {
        EnemyDefeat.Invoke();
        Destroy(this.gameObject);
    }
}
