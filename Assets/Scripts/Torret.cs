using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Torret : MonoBehaviour
{
    public static UnityEvent OnRadyToFire = new UnityEvent();

    #region Transform
    public Vector3 targetPosition;
    public float rotVelocity;
    Quaternion playerRot;
    Vector3 rot;
    Vector3 lookAtTarget;
    RaycastHit hitTarget;
    public bool fire = true;
    public bool movement = true;
    public float angle;
    #endregion

    void Update()
    {
        TorretFollow();
        Fire();
    }
    void TorretFollow()
    {
        if(Input.GetMouseButtonDown(0) && movement)
        {
            SetTargetPosition();
            movement = false;
            fire = true;
        }
        //transform.rotation = Quaternion.Slerp(transform.rotation,playerRot,rotVelocity * Time.deltaTime);
        rot = Vector3.RotateTowards(transform.forward,lookAtTarget,rotVelocity * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(rot);
    
        angle = Vector3.Dot(transform.right,lookAtTarget);
    }
    void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hitTarget, 1000))
        {
            targetPosition = hitTarget.point;
            var target = new Vector3(targetPosition.x -transform.position.x,
            0.0f,
            targetPosition.z-transform.position.z);
            lookAtTarget = target;
        }
    }
    void OnDrawGizmos()
    {            
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 100f);
    }
    void Fire()
    {
        if(angle <2f && angle>-2 && fire)
        {
            OnRadyToFire.Invoke();
            fire = false;
            movement = true;
        }
    }
}
