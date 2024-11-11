using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Stage
{
    public class StageCreator : MonoBehaviour
    {
        private const int _tileSide = 8;
        public static int[,] _tileZs{ get; private set; } = new int[_tileSide, _tileSide]{
        {1,1,1,1,1,0,1,1},
        {1,1,4,1,1,0,1,1},
        {1,1,4,4,1,0,1,1},
        {1,1,4,4,1,0,1,1},
        {1,1,4,4,1,0,1,1},
        {1,1,4,3,1,1,1,1},
        {1,1,4,2,1,3,2,1},
        {1,1,1,1,1,1,1,1},};
        [SerializeField] private Tilemap[] tilemapList_Stage = new Tilemap[_tileSide * 2 - 1];
        [SerializeField] private ScriptableObjects.TileData TileData;

        private void Start()
        {
            CreateNewStage();
        }

        public void CreateNewStage()
        {
            for(int i = 0; i < _tileSide * 2 - 1; i++)
            {
                for(int j = 0; j < _tileSide * 2 - 1; j++)
                {
                    if(i - j >= _tileSide || i - j < 0 || j >= _tileSide) continue;
                    int tileheight = _tileZs[i - j, j];
                    if(tileheight == 0) continue;
                    Tile tile = TileData.GetStageTile(tileheight);
                    Vector3Int tilePosition = new((_tileSide - 1) / 2 - i + j, (_tileSide - 1) / 2 - j, 0);
                    tilemapList_Stage[i].SetTile(tilePosition, tile);
                }
            }
        }
    }
}
