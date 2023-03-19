using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    private Transform CameraTr;
    private Transform RocketTr;
    [SerializeField] private float sensivity;
    
    void Start(){
        CameraTr = Camera.main.transform;
        RocketTr = this.transform;
    }

    void Update()
    {
        CameraTr.LookAt(RocketTr);
        if(Input.GetMouseButton(0)){
            CameraTr.RotateAround(RocketTr.position, Vector3.up, Input.GetAxisRaw("Mouse X") * sensivity);
            CameraTr.RotateAround(RocketTr.position, Vector3.forward, Input.GetAxisRaw("Mouse Y") * sensivity);
        }
        
        if(Input.mouseScrollDelta.y != 0){
            CameraTr.Translate(Vector3.forward * Input.mouseScrollDelta.y, Space.Self);
        }

        if((CameraTr.position - RocketTr.position).magnitude > 70f){
            CameraTr.Translate(Vector3.forward, Space.Self);
        }
        if((CameraTr.position - RocketTr.position).magnitude > 120f){
            CameraTr.Translate(Vector3.forward * 5f, Space.Self);
        }
        if((CameraTr.position - RocketTr.position).magnitude > 300f){
            CameraTr.Translate(Vector3.forward * 120f, Space.Self);
        }
        
    }

    
}