using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TelegraphKey : MonoBehaviourPun
{
    [SerializeField]
    private AudioSource _myBeepSource, _othersBeepSource;
    [SerializeField]
    private AudioClip _myBeep, _othersBeep;

    public void Click()
    {
        _myBeepSource.PlayOneShot(_myBeep);
        photonView.RPC("TransferBeep", RpcTarget.All, photonView.ViewID);
    }

    [PunRPC]
    private void TransferBeep(int viewID)
    {
        if (photonView.ViewID == viewID) return;
        _othersBeepSource.PlayOneShot(_othersBeep);
    }
}
