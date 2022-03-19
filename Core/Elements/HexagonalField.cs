using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle.Networking;

namespace CardBattle.Core.Elements
{
    public class HexagonalField : MonoBehaviour
    {
        private int index;
        private Vector2 axialCoordinates;
        private Vector3 coordinates;
        private HexagonalField[] neighborHexagonalFields;
        private MapGeneration indexedBy;
        public HexagonalFieldController controller;

        /// <summary>
        /// q = x; r = y
        /// </summary>
        public Vector2 AxialCoordinates => new Vector2(axialCoordinates.x, axialCoordinates.y);
        /// <summary>
        /// q = x; r = y; s = z
        /// </summary>
        public Vector3 CubeCoordinates => new Vector3(axialCoordinates.x, axialCoordinates.y, -axialCoordinates.x - axialCoordinates.y);
        public Vector3 Coordinates => new Vector3(coordinates.x, coordinates.y, coordinates.z);
        public MapGeneration IndexedBy => indexedBy;

        public HexagonalField this[DIRECTION direction] => neighborHexagonalFields[(int)direction];

        public void Initialize(int index, Vector2 axialCoordinates, Vector3 coordinates, MapGeneration indexedBy)
        {
            this.index = index;
            this.axialCoordinates = axialCoordinates;
            this.coordinates = coordinates;
            this.indexedBy = indexedBy;
        }

        public void GetNeighbors()
        {
            Vector2[] neighborAxialCoords = new Vector2[]
            {
                new Vector2(AxialCoordinates.x + 1, AxialCoordinates.y + 0),
                new Vector2(AxialCoordinates.x + 0, AxialCoordinates.y + 1),
                new Vector2(AxialCoordinates.x - 1, AxialCoordinates.y + 1),
                new Vector2(AxialCoordinates.x - 1, AxialCoordinates.y + 0),
                new Vector2(AxialCoordinates.x + 0, AxialCoordinates.y - 1),
                new Vector2(AxialCoordinates.x + 1, AxialCoordinates.y - 1),
            };

            neighborHexagonalFields = new HexagonalField[6];

            for (int i = 0; i < 6; i++)
            {
                if (Mathf.Abs(neighborAxialCoords[i].x) <= indexedBy.radius / 2 && Mathf.Abs(neighborAxialCoords[i].y) <= indexedBy.radius / 2)
                {
                    neighborHexagonalFields[i] = indexedBy.AxialHexagonalFields[neighborAxialCoords[i]].GetComponent<HexagonalField>();
                }
                else neighborHexagonalFields[i] = null;
            }
        }
    }
}
