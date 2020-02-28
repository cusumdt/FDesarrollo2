using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tank : MonoBehaviour
{

    [SerializeField, Range(0f, 100f)]
	float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)]
	float maxAcceleration = 10f, maxAirAcceleration = 1f;
    [SerializeField, Range(0, 90)]
	float maxGroundAngle = 25f;
    [SerializeField, Range(0f, 10f)]
	float jumpHeight = 2f;
    [SerializeField, Range(0, 5)]
	int maxAirJumps = 0;
    [SerializeField, Range(0f, 100f)]
    float rotationSpeed;
    [SerializeField, Range(0f, 100f)]
    float maxRayDistance;
    [SerializeField, Range(0f, 100f)]
    float smoothRotation;
    int jumpPhase;
    Vector3 desiredVelocity;
    int groundContactCount;
	bool OnGround => groundContactCount > 0;
    bool desiredJump;
    public Vector3 velocity;
    Vector3 initialPosition;
    Vector2 playerInput;
    Vector3 contactNormal;
    [SerializeField] Rigidbody myRig;
    [SerializeField] LayerMask rayMask;
   
 
    float minGroundDotProduct;


    void OnValidate()
    {
        minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
    }
    void Awake()
    {
        myRig = GetComponent<Rigidbody>();
        OnValidate();
    }

    void Start()
    {
        initialPosition = transform.localPosition;
        GameManager.totalDistance.AddListener(Distance);
    }
    void FixedUpdate()
    {
        TankMovement();
        CheckGround();
    }
    void Update()
    {
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput,1f);
        transform.Rotate(Vector3.up * rotationSpeed * playerInput.x * Time.deltaTime );
        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        //desiredJump |= Input.GetButtonDown("Jump");
    }

    void TankMovement()
    {
        
        UpdateState();
        AdjustVelocity();
       if(desiredJump)
       {
           desiredJump = false;
           Jump();
       }
       
        myRig.velocity = velocity;
        ClearState();
    }

    void UpdateState()
    {
        velocity = myRig.velocity;
        if(OnGround)
        {
            jumpPhase = 0;
			if (groundContactCount > 1) 
            {
				contactNormal.Normalize();
			}
        }
        else
        {
            contactNormal = Vector3.up;
        }
    }
    void Jump()
    {	
        if (OnGround || jumpPhase < maxAirJumps) 
        {
			jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            float alignedSpeed = Vector3.Dot(velocity, contactNormal);
			if (alignedSpeed > 0f) {
				jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);
			}
            velocity += contactNormal * jumpSpeed;
        }
    }
    void ClearState()
    {
        groundContactCount = 0;
        contactNormal= Vector3.zero;
    }
    void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);
    }
    void OnCollisionStay (Collision collision) 
    {   

		EvaluateCollision(collision);
	}	
	void EvaluateCollision (Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++) 
        {
			Vector3 normal = collision.GetContact(i).normal;
            if(normal.y >= minGroundDotProduct)
            {
                groundContactCount += 1;
                contactNormal += normal;
            }
        }
    }
    Vector3 ProjectOnContactPlane (Vector3 vector) {
		return vector - contactNormal * Vector3.Dot(vector, contactNormal);
	}
    void AdjustVelocity () 
    {
		Vector3 zAxis = ProjectOnContactPlane(transform.forward).normalized;
        //Vector3 zAxis = transform.forward;
		float currentZ = Vector3.Dot(velocity, zAxis);
    	float acceleration = OnGround ? maxAcceleration : maxAirAcceleration;
		float maxSpeedChange = acceleration * Time.deltaTime;
		float newZ =
			Mathf.MoveTowards(currentZ, desiredVelocity.z, maxSpeedChange);
        velocity += zAxis * (newZ - currentZ);
    }


    void CheckGround()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.up,out hit,maxRayDistance, rayMask);
        Quaternion resultRotation = Quaternion.FromToRotation(transform.up,hit.normal) *transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, resultRotation,smoothRotation);
    }
    void OnTriggerEnter(Collider other)
    {
        transform.localPosition = initialPosition;
    }
    void Distance()
    {
        GameManager.instance.AddDistance(initialPosition, transform.localPosition);
    }
    
}
