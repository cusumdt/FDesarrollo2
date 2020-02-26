using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public Text textClock;
    public float clock = 30.0f;
    public Text textPoints;
    public int points = 0;
    public int cantEnemys = 0;
    public int winCondition = 6;
    public float distance = 0.0f;
    public int pointValue = 100;
    public float distanceValue = 0.1f;
    public bool AddText = false;
    void Start()
    {
        EnemySystem.EnemyDefeat.AddListener(EnemyDefeat);
        Tank.DistanceTraveled.AddListener(AddDistance);
        UIMenu.ResetStats.AddListener(Reset);

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (AddText)
            {
                textClock = GameObject.Find("Text").GetComponent<Text>();
                textPoints = GameObject.Find("Puntaje").GetComponent<Text>();
                AddText = false;
            }
            clock -= Time.deltaTime;
            int result = (int)clock;
            textClock.text = result.ToString();
            if (clock <= 0.0f)
            {
                SceneManager.LoadScene("FinishGame");
            }
            else if(cantEnemys == winCondition)
            {
                SceneManager.LoadScene("FinishGame");
            }
        }
    }
    void EnemyDefeat()
    {
        cantEnemys++;
        points += pointValue;
        textPoints.text = "Puntaje: " + points.ToString();
    }
    void AddDistance()
    {
        distance += distanceValue;
    }
    void Reset()
    {
        clock = 30.0f;
        points = 0;
        cantEnemys = 0;
        distance = 0.0f;
        AddText = true;
    }

}
