using System.Collections.Generic;
using Assets.Scripts.Stage;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TileData")]
    public class TileData : ScriptableObject
    {
        [SerializeField] private List<Tile> _stageTiles;

        public void SetTile(int height, Vector3Int tilePosition, Tilemap tilemap)
        {
            for(int i = 0; i < StageFacade._stageHeight; i++)
                tilemap.SetTile(tilePosition + new Vector3Int(0, 0, i), null);
            if(height <= 0) return;
            for(int i = 0; i < height; i++)
                tilemap.SetTile(tilePosition + new Vector3Int(0, 0, i), _stageTiles[0]);
        }
    }
}