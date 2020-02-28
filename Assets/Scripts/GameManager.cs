using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

//Calcular distancia real.

public class GameManager : Singleton<GameManager>
{
    public static UnityEvent lookPoints = new UnityEvent();
    public static UnityEvent totalDistance = new UnityEvent();
    public float clock = 30.0f;
    public int points = 0;
    public int cantEnemys = 0;
    public int winCondition = 6;
    public float distance = 0.0f;
    public int pointValue = 100;
    bool inGame = true;

    void Start()
    {
        EnemySystem.EnemyDefeat.AddListener(EnemyDefeat);
        UIMenu.ResetStats.AddListener(Reset);
    }

    void Update()
    {
        if (inGame)
        {
            clock -= Time.deltaTime; 
            if (clock <= 0.0f)
            {
                inGame = false;
                totalDistance.Invoke();
                SceneManager.LoadScene("FinishGame");
            }
            else if(cantEnemys == winCondition)
            {
                inGame = false;
                totalDistance.Invoke();
                SceneManager.LoadScene("FinishGame");
            }
        }
    }

    void EnemyDefeat()
    {
        cantEnemys++;
        points += pointValue;
        lookPoints.Invoke();
    }
    public void AddDistance(Vector3 initDistance, Vector3 finishDistance)
    {
        var x = Mathf.Pow((finishDistance.x - initDistance.x), 2);
        var y = Mathf.Pow((finishDistance.y - initDistance.y), 2);
        var z = Mathf.Pow((finishDistance.z - initDistance.z), 2);
        distance = Mathf.Sqrt(x+y+z);
    }
    void Reset()
    {
        inGame = true;
        clock = 30.0f;
        points = 0;
        cantEnemys = 0;
        distance = 0.0f;
    }

}
