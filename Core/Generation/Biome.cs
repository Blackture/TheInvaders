using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardBattle.Core.Generation
{
    [System.Serializable]
    public class Biome
    {
        public static List<Biome> biomes = new List<Biome>();

        [SerializeField] private Material material;
        [SerializeField] private string name;
        [SerializeField] private float heightMultiplier = 1f;

        public Material Material => material;
        public string Name => name;
        public float HeightMultiplier => heightMultiplier;

        public Biome(string name, Material material)
        {
            this.name = name;
            this.material = material;
            biomes.Add(this);
        }
    }
}
