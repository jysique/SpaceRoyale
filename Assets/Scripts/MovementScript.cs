using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class MovementScript : MonoBehaviourPun
{
    [SerializeField] private float movementSpeed  = 0f;
    private CharacterController controller = null;
    // Update is called once per frame
    private void Start() {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            TakeInput();
        }    
    }
    private void TakeInput(){
        Vector3 mov = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0f,
            z = Input.GetAxisRaw("Vertical")
        }.normalized;
        controller.SimpleMove(mov * movementSpeed * Time.deltaTime);
    }
}
