using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Weapon : MonoBehaviourPun
{
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private GameObject projectile = null;
    [SerializeField] private Transform spawnPoint = null;
    private void Update() {
        if (photonView.IsMine)
        {
            TakeInput();
            //move with mouse
        }

    }
    void TakeInput(){
        if (!Input.GetMouseButton(0))
        {
            return;
        }
        photonView.RPC("FiredProjectile",RpcTarget.All);
    }

    [PunRPC]
    private void FiredProjectile(){
        var projectileInstance = Instantiate(projectile,spawnPoint.position,spawnPoint.rotation);
        projectileInstance.GetComponent<Rigidbody>().velocity = projectileInstance.transform.forward * projectileSpeed;
    }
}
