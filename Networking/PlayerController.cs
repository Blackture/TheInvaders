using UnityEngine;
using Photon.Pun;
using CardBattle.RemoteConfig;

namespace CardBattle.Networking
{
    class PlayerController : MonoBehaviourPunCallbacks
    {
        private PhotonView view;
        [SerializeField] private float speed = Configs.PlayerController_Speed;

        private void Start()
        {
            view = GetComponent<PhotonView>();
        }

        private void Update()
        {
            if (view.IsMine)
            {
                Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
                transform.position += input.normalized * speed * Time.deltaTime;
            }
        }
    }
}
