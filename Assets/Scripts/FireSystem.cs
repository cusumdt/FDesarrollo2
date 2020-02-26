using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSystem : MonoBehaviour
{

    ParticleSystem fire;

    // Start is called before the first frame update
    void Start()
    {
        fire = GetComponent<ParticleSystem>();
        Torret.OnRadyToFire.AddListener(Fire);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fire()
    {
       fire.Emit(1);
    }

    void OnParticleCollision(GameObject other)
    {
        other.SendMessage("DestroyEnemy");
    }
}



   
   