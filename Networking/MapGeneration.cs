using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle.Core.Generation;

namespace CardBattle.Networking
{
    public class MapGeneration : MonoBehaviour
    {
        [SerializeField] private float hexagonSize = 50f;
        [SerializeField] private float aspect_scale = 1f;
        [SerializeField] private List<Biome> biomes = new List<Biome>();

        public List<GameObject> HexagonalFields = new List<GameObject>();
        public Dictionary<Vector2, GameObject> AxialHexagonalFields = new Dictionary<Vector2, GameObject>();

        private uint step = 0;

        public bool Finished = false;
        public int radius = 10;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Generate());
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator Generate()
        {
            InitBiomes();
            yield return new WaitUntil(() => (step > 0));
            StartCoroutine(GenerateFields());
            yield return new WaitUntil(() => (step > 1));
            GetNeighborsFields();
            yield return new WaitUntil(() => (step > 2));
            StartCoroutine(SlerpFields());
            yield return new WaitUntil(() => AllFieldsSlerped());
            step = 4;
            yield return new WaitUntil(() => (step > 3));
            StartCoroutine(SetBiomes());

            Finished = true;
        }

        private void InitBiomes() //maybe needs a change to an IEnumerator
        {
            //Here needs to get the modification inteface to add/change some biomes
            Biome.biomes = biomes;
            step = 1;
        }

        private IEnumerator SetBiomes()
        {
            foreach (GameObject g in HexagonalFields)
            {
                Biome b = GetRandomBiome();
                MeshRenderer r = g.transform.Find("Hexagon").GetComponent<MeshRenderer>();
                Material[] mats = r.materials;
                for (int i = 0; i < mats.Length; i++)
                {
                    if (mats[i].name.Contains("Blank")) mats[i] = b.Material;
                }
                r.materials = mats;
                r.UpdateGIMaterials();
                g.transform.localScale = new Vector3(g.transform.localScale.x, g.transform.localScale.y * b.HeightMultiplier, g.transform.localScale.z);
                //biome needs to be stored in the hexagonal field
                yield return new WaitForSeconds(0.0125f);
            }
        }

        private Biome GetRandomBiome()
        {
            float rnd = Random.Range(0f, 1f);
            Biome b = null;

            for (int i = 0; i <= biomes.Count - 1; i++)
            {
                if (rnd > (float)i / biomes.Count) b = biomes[i];
            }

            return b;
        }

        private IEnumerator GenerateFields()
        {
            float size = Mathf.Sqrt(3) * hexagonSize;
            float[] stackHeights = new float[7];
            List<Vector2> stackPositions = new List<Vector2>();

            for (int q = -1; q <= 1; q++)
            {
                int r1 = Mathf.Max(-1, -q - 1);
                int r2 = Mathf.Min(1, -q + 1);

                for (int r = r1; r <= r2; r++)
                {
                    float x = -size * Mathf.Sqrt(3) * (r + q / 2.0f);
                    float y = size * 3 / 2.0f * q;
                    stackPositions.Add(new Vector2(40 + x, y));
                }
            }

            int nextStack = 0;

            for (int q = -radius; q <= radius; q++)
            {
                int r1 = Mathf.Max(-radius, -q - radius);
                int r2 = Mathf.Min(radius, -q + radius);

                for (int r = r1; r <= r2; r++)
                {
                    float x = -size * Mathf.Sqrt(3) * (r + q / 2.0f);
                    float y = size * 3 / 2.0f * q;
                    GameObject g = (GameObject)Resources.Load("hexagon field");
                    GameObject i = Instantiate(g, transform); //Needs to be changed to work with networking!!!

                    i.transform.localPosition = new Vector3(stackPositions[nextStack].x, 0.3f * Mathf.Floor(HexagonalFields.Count / 7) - 0.1f, stackPositions[nextStack].y);
                    nextStack += (nextStack + 1 > 6) ? -6 : 1;

                    i.GetComponent<HexagonalFieldController>().Initialize(HexagonalFields.Count, new Vector2(q, r), new Vector3(x, 0, y), this);
                    i.transform.localScale = new Vector3(hexagonSize / Mathf.Sqrt(3), 1, hexagonSize / Mathf.Sqrt(3));
                    HexagonalFields.Add(i);
                    AxialHexagonalFields.Add(new Vector2(q, r), i); //axial coords of hex needs to be stored (for neigboars etc)
                    yield return new WaitForEndOfFrame();
                }
            }
            step = 2;
        }

        private void GetNeighborsFields()
        {
            for (int i = 0; i < HexagonalFields.Count; i++)
            {
                HexagonalFields[i].GetComponent<HexagonalFieldController>().GetNeighbors();
            }
            step = 3;
        }

        private IEnumerator SlerpFields()
        {
            for (int i = HexagonalFields.Count - 1; i >= 0; i--)
            {
                HexagonalFields[i].GetComponent<HexagonalFieldController>().Lerp();
                yield return new WaitForSeconds(0.05f);
            }
        }

        public bool AllFieldsSlerped()
        {
            bool res = true;
            for (int i = 0; i < HexagonalFields.Count; i++)
            {
                if (!HexagonalFields[i].GetComponent<HexagonalFieldController>().IsLerped) res = false;

            }
            return res;
        }
    }
}
