using UnityEngine;
using Photon.Pun;

namespace CardBattle.Networking
{
    [RequireComponent(typeof(PhotonView))]
    public class NetworkManagers : MonoBehaviourPunCallbacks
    {
        public static NetworkManagers instance;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(instance);
        }

        public void Send(object o)
        {
            photonView.RPC("SendRPC", RpcTarget.All, o);
        }

        public void Send<T>(T o)
        {
            photonView.RPC("SendRPC", RpcTarget.All, o);
        }


        [PunRPC]
        private void SendRPC(object o)
        {
            SendRPC(o);
        }

        [PunRPC]
        private void SendRPC<T>(T o)
        {
            SendRPC(o);
        }

        public void SendSecure(object o)
        {
            photonView.RpcSecure("SendSecureRPC", RpcTarget.All, true, o);
        }

        public void SendSecure<T>(T o)
        {
            photonView.RpcSecure("SendSecureRPC", RpcTarget.All, true, o);
        }


        [PunRPC]
        private void SendSecureRPC(object o)
        {
            SendSecureRPC(o);
        }

        [PunRPC]
        private void SendSecureRPC<T>(T o)
        {
            SendSecureRPC(o);
        }
    }
}
