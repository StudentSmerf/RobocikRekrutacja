using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    public static Movement instance;
    public Rigidbody rb;
    public TMP_Text text;
    public Transform RocketTransform;
    [SerializeField] private Transform forwardPoint, rightPoint;
    public float RotationSpeed;
    public Vector3 DirectionForward;
    public Vector3 DirectionRight;
    
    void Start()
    {
        instance = this;
    }

    void Update(){
        //only for testing
        //text.text = rb.velocity.ToString() + " velocity \n" + rb.angularVelocity.ToString() + " angular velocity \n" + (Camera.main.transform.position - RocketTransform.position).magnitude.ToString() + " distance from rocket \n";

        DirectionForward = forwardPoint.position - RocketTransform.position;
        DirectionRight = rightPoint.position - RocketTransform.position;
    }

}
