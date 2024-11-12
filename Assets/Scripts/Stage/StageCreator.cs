using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.ScriptableObjects;
using System.Collections.Generic;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using System.Linq;

namespace Assets.Scripts.Stage
{
    public class StageCreator : MonoBehaviour
    {
        private const int _mapSide = 8;
        private const int _mapHeight = 4;
        public static int[,] _tileZs{ get; private set; } = new int[_mapSide, _mapSide];
        [SerializeField] private Tilemap[] tilemapList_Stage = new Tilemap[_mapSide * 2 - 1];
        [SerializeField] private ScriptableObjects.TileData TileData;

        private void Start()
        {
            CreateNewStage();
        }

        public void CreateNewStage()
        {
            SetStageMatrix();
            SetAllTiles();
        }

        private static void SetStageMatrix()
        {
            //行列の初期化
            InitializeMatrix(_tileZs);
            //BFS
            SetAllZsByBFS();
            //1マスの穴を埋める
            FillHole();
        }

        private static void InitializeMatrix(int[,] matrix)
        {
            for(int i = 0; i < _mapSide; i++)
                for(int j = 0; j < _mapSide; j++)
                    matrix[i, j] = 0;
        }

        private static void SetAllZsByBFS()
        {
            Queue<(int i, int j, int h)> queue = new();
            queue.Enqueue((_mapSide - 1, _mapSide - 1, 1));
            while(true)
            {
                if(queue.Count == 0) break;
                (int i, int j, int h) = queue.Dequeue();
                if(i < 0 || j < 0 || i >= _mapSide || j >= _mapSide) continue;
                if(_tileZs[i, j] != 0) continue;
                _tileZs[i, j] = AdjustHToSee(i, j, h);
                int nextH = CalculateNextH(h);
                queue.Enqueue((i - 1, j, nextH));
                queue.Enqueue((i, j - 1, nextH));
            }
        }
        
        private static int AdjustHToSee(int i, int j, int h)
        {
            int adjustedH = h;
            if(i + 1 < _mapSide) adjustedH = math.max(_tileZs[i + 1, j] - 1, adjustedH);
            if(j + 1 < _mapSide) adjustedH = math.max(_tileZs[i, j + 1] - 1, adjustedH);
            if(i + 1 < _mapSide && j + 1 < _mapSide) adjustedH = math.max(_tileZs[i + 1, j + 1] - 1, adjustedH); 
            return adjustedH;
        }
        
        private static void FillHole()
        {
            int[,] updatedTileZs = new int[_mapSide, _mapSide];
            CopyMatrix(_tileZs, updatedTileZs);
            for(int i = 0; i < _mapSide; i++)
                for(int j = 0; j < _mapSide; j++)
                    updatedTileZs[i, j] = AdjustHToFill(i, j);
            CopyMatrix(updatedTileZs, _tileZs);
        }

        private static void CopyMatrix(int[,] matrix, int[,] copiedMatrix)
        {
            for(int i = 0; i < _mapSide; i++)
                for(int j = 0; j < _mapSide; j++)
                    copiedMatrix[i, j] = matrix[i, j];
        }

        private static int AdjustHToFill(int i, int j)
        {
            List<int> aroundTileZs = new();
            if(i + 1 < _mapSide) aroundTileZs.Add(_tileZs[i + 1, j]);
            if(j + 1 < _mapSide) aroundTileZs.Add(_tileZs[i, j + 1]);
            if(i - 1 >= 0) aroundTileZs.Add(_tileZs[i - 1, j]);
            if(j - 1 >= 0) aroundTileZs.Add(_tileZs[i, j - 1]);
            return math.max(aroundTileZs.Min(), _tileZs[i, j]);
        }
        
        private static int CalculateNextH(int h)
        {
            int nextH = h;
            int r = Random.Range(0, 10);
            if(r == 9) nextH++;
            if (r == 8) nextH--;
            if(nextH > _mapHeight) nextH = _mapHeight;
            if(nextH < 1) nextH = 1;
            return nextH;
        }
        
        private void SetAllTiles()
        {
            for(int i = 0; i < _mapSide * 2 - 1; i++)
            {
                for(int j = 0; j < _mapSide * 2 - 1; j++)
                {
                    if(i - j >= _mapSide || i - j < 0 || j >= _mapSide) continue;
                    int tileheight = _tileZs[i - j, j];
                    if(tileheight == 0) continue;
                    Tile tile = TileData.GetStageTile(tileheight);
                    Vector3Int tilePosition = new((_mapSide - 1) / 2 - i + j, (_mapSide - 1) / 2 - j, 0);
                    tilemapList_Stage[i].SetTile(tilePosition, tile);
                }
            }
        }
    }
}
