using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class PlayerNameTag : MonoBehaviourPun
{
    [SerializeField] private TextMeshProUGUI nametext;
    void Start()
    {
     //if (photonView.IsMine)
     //{
         SetName();
     //}   

    }
    private void SetName()
    {
        print(photonView.Owner.NickName);
        nametext.text = photonView.Owner.NickName;
        
    }
}
