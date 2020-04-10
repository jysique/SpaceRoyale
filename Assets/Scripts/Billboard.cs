using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform mainCameraTransform;
    private void Start() {
        mainCameraTransform = Camera.main.transform; 
    }
    //Happens every frame but after normal update
    private void LateUpdate() {
        transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward,
            mainCameraTransform.rotation*Vector3.up);
        
    }
}
