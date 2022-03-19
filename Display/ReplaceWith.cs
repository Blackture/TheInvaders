using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

namespace CardBattle.Display
{
    [RequireComponent(typeof(TMP_Text))]
    public class ReplaceWith : MonoBehaviour
    {
        enum Specification
        {
            GameTitle,
            GameVersion,
            GameCompany,
            RoomName
        }

        [SerializeField] private Specification specification;

        // Start is called before the first frame update
        void Start()
        {
            switch (specification)
            {
                case Specification.GameTitle:
                    GetComponent<TMP_Text>().text = Application.productName;
                    break;
                case Specification.GameCompany:
                    GetComponent<TMP_Text>().text = Application.companyName;
                    break;
                case Specification.GameVersion:
                    GetComponent<TMP_Text>().text = Application.version;
                    break;
                case Specification.RoomName:
                    GetComponent<TMP_Text>().text = PhotonNetwork.CurrentRoom.Name;
                    break;
            }
        }
    }
}