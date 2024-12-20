using System.Collections.Generic;
using Assets.Scripts.Stage;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TileData")]
    public class TileData : ScriptableObject
    {
        [SerializeField] private List<Tile> _stageNormalTiles;
        [SerializeField] private Tile _stageTopTile;

        public void SetTile(int height, Vector3Int tilePosition, Tilemap tilemap)
        {
            for(int i = 0; i < StageCreator._StageHeight; i++)
                tilemap.SetTile(tilePosition + new Vector3Int(0, 0, i), null);
            if(height <= 0) return;
            for(int i = 0; i < height - 1; i++)
                tilemap.SetTile(tilePosition + new Vector3Int(0, 0, i), _stageNormalTiles[Random.Range(0, _stageNormalTiles.Count)]);
            tilemap.SetTile(tilePosition + new Vector3Int(0, 0, height - 1), _stageTopTile);
        }
    }
}