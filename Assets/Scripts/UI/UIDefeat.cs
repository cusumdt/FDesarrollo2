using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
public class UIDefeat : MonoBehaviour
{
    public Text textPoints;
    public Text textEnemys;
    public Text textDistance;
    // Start is called before the first frame update
    void Start()
    {   
        int result = (int)GameManager.instance.distance;
        string cantEnemys = GameManager.instance.cantEnemys.ToString();
        string cantDistance= result.ToString();
        string cantPoints = GameManager.instance.points.ToString();
        textPoints.text = "Points: " + cantPoints ;
        textDistance.text ="Distance: " + cantDistance;
        textEnemys.text ="Enemies Destroyed: "+ cantEnemys;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
