using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using CardBattle.Core.Elements;
using CardBattle.Core;

namespace CardBattle.Networking
{
    [RequireComponent(typeof(HexagonalField))]
    public class HexagonalFieldController : MonoBehaviourPunCallbacks, IPunObservable
    {
        private HexagonalField hex;
        private bool isLerped = false;
        private bool isLerping = false;
        private float lerpTime = 0f;
        private float lerpSpeed = 1f;
        private Vector3 lerpStart;
        public bool IsLerped => isLerped;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isLerping)
            {
                lerpTime += Time.deltaTime * lerpSpeed;
                Lerp();
            }
        }

        public void Initialize(int index, Vector2 axialCoordinates, Vector3 coordinates, MapGeneration indexedBy)
        {
            hex = GetComponent<HexagonalField>();
            hex.controller = this;
            hex.Initialize(index, axialCoordinates, coordinates, indexedBy);
            lerpStart = transform.localPosition;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            throw new System.NotImplementedException();
        }

        public HexagonalField GetNeighbor(DIRECTION direction)
        {
            return hex[direction];
        }

        public void GetNeighbors()
        {
            hex.GetNeighbors();
        }

        public void Lerp()
        {
            if (lerpTime < 1f)
            {
                isLerping = true;
                Vector3 newPos = Vector3.Slerp(lerpStart, hex.Coordinates, Mathf.Clamp01(lerpTime));
                transform.localPosition = newPos;
            }
            else
            {
                transform.localPosition = hex.Coordinates;
                isLerped = true;
                isLerping = false;
            }
        }
    }
}
