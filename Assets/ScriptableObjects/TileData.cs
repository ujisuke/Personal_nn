using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TileData")]
    public class TileData : ScriptableObject
    {
        [SerializeField] private List<Tile> _stageTiles;

        public Tile GetStageTile(int height)
        {
            return _stageTiles[height - 1];
        }
    }
}