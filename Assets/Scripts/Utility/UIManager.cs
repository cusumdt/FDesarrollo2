using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Text textClock;
    public Text textPoints;
  
    void Start()
    {
        GameManager.lookPoints.AddListener(UpdatePoint);
    }


    void Update()
    {
        int clock = (int)GameManager.instance.clock;
        textClock.text =clock.ToString();
    }

    void UpdatePoint()
    {
        int point = GameManager.instance.points;
        textPoints.text = point.ToString();
    }
}
