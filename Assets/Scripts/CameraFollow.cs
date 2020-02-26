using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Rigidbody player;
    public Vector3 offSet;
    public float velocity;
    Vector3 resultPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        resultPosition = player.position + offSet;
        transform.position = Vector3.Lerp(transform.position, resultPosition,velocity * Time.deltaTime);
        transform.LookAt(player.position);
    }
}
