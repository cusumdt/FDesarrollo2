using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Tank : MonoBehaviour
{
    public static UnityEvent DistanceTraveled = new UnityEvent();
    #region Float
    [SerializeField] float movementSpeed = 3.0f;
    [SerializeField] float rotationSpeed = 90.0f;
    [SerializeField] float maxRayDistance = 10.0f;
    [SerializeField] float smoothRotation = 0.3f;
    [SerializeField] float movementTank ;
    [SerializeField] float resultAngle;
    [SerializeField] float rayDistanceForward;
    [SerializeField] float resultBackAngle;
    [SerializeField] float rayDistanceBackward;
    [SerializeField] float limitAngle = 0.5f;
    #endregion

    #region Vector3
    [SerializeField] Vector3 angleRotation;
    [SerializeField] Vector3 rayOffset;
    #endregion
    
    [SerializeField] Rigidbody myRig;
    [SerializeField] LayerMask rayMask;


    // Start is called before the first frame update
    void Awake()
    {
        myRig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TankMovement();
         CheckGround();
         CheckSlope();
        TankRotation();
       
    }

    void TankMovement()
    {
        movementTank = Input.GetAxis("Vertical");
        if(Input.GetAxis("Vertical") != 0)
        {
            DistanceTraveled.Invoke();
        }
        angleRotation = myRig.transform.localRotation.eulerAngles;
        LimitMovement();
        Vector3 movement = transform.forward * movementTank * movementSpeed * Time.deltaTime;
        myRig.MovePosition(transform.position + movement);
    }
    void LimitMovement()
    {
           if(resultAngle > limitAngle && movementTank>0)
        {
            movementTank = 0;
        }
        if(resultBackAngle > limitAngle && movementTank<0)
        {
            movementTank = 0;
        }
    }
    void TankRotation()
    {
        float rotateTank = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * rotationSpeed * rotateTank * Time.deltaTime );
    }
    void CheckGround()
    {
        RaycastHit hit;


        Physics.Raycast(transform.position, -transform.up,out hit,maxRayDistance, rayMask);
        //transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(hit.normal),1);
        Quaternion resultRotation = Quaternion.FromToRotation(transform.up,hit.normal) *transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, resultRotation,smoothRotation);
    }
    void CheckSlope()
    {
        RaycastHit hitForward;
        RaycastHit hitBackward;

        Physics.Raycast(transform.position+rayOffset, transform.forward,out hitForward, rayDistanceForward, rayMask);
        resultAngle = Vector3.Dot(Vector3.up,hitForward.normal);

        Physics.Raycast(transform.position + rayOffset, -transform.forward, out hitBackward,rayDistanceBackward,rayMask);
        resultBackAngle = Vector3.Dot(Vector3.up, hitBackward.normal);
    }
    void OnDrawGizmos()
    {
        DrawRaycast(transform.position, -transform.up, maxRayDistance, rayMask);
        DrawRaycast(transform.position + rayOffset, transform.forward, rayDistanceForward, rayMask);
        DrawRaycast(transform.position + rayOffset, -transform.forward, rayDistanceBackward, rayMask);
    }   
    void DrawRaycast(Vector3 Position, Vector3 Direction, float Distance, LayerMask Mask)
    {
        if(Physics.Raycast(Position, Direction, Distance, Mask))
        {
            Gizmos.color = Color.yellow;    
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawRay(Position,Direction * Distance);
        
    }

}
