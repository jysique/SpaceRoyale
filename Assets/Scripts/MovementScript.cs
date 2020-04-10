using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class MovementScript : MonoBehaviourPun
{
    [SerializeField] private float movementSpeed  = 0f;
    private CharacterController controller = null;
    
    //move with mouse
    Vector3 targetPosition;
    Vector3 lookAtTarget;
    Quaternion playetRot;
    [SerializeField] private float rotSpeed = 5;
    [SerializeField] private float speed = 10;
    bool moving = false;
    private void Start() {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            //TakeInput();
            //move with mouse
            
            if (Input.GetMouseButton(0))
            {
                SetTargetPosition();
            }
            if (moving)
            {
                Move();
            }
            
        }    
    }
    //move with directionals

    /*
    private void TakeInput(){
                
        Vector3 mov = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0f,
            z = Input.GetAxisRaw("Vertical")
        }.normalized;
        controller.SimpleMove(mov * movementSpeed * Time.deltaTime);  
    }
    */

    //move with mouse
    private void SetTargetPosition(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,1000))
        {
            targetPosition = hit.point;
            //this.transform.LookAt(targetPosition);
            lookAtTarget = new Vector3(targetPosition.x - transform.position.x,
                            transform.position.y,
                            targetPosition.z - transform.position.z);
            //print(lookAtTarget);
            playetRot  = Quaternion.LookRotation(lookAtTarget);
            moving = true;
        }
    }
    private void Move(){
        transform.rotation = Quaternion.Slerp(transform.rotation,playetRot, rotSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed*Time.deltaTime);
        if (transform.position == targetPosition)
        {
            moving = false;
        }
    }
    
}
