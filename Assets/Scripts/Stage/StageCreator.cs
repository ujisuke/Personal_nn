using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.ScriptableObjects;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Stage
{
    public class StageCreator : MonoBehaviour
    {
        public const int _stageSide = 8;
        public const int _stageHeight = _stageSide * 2;
        public const float _tileHeight = 0.25f;
        public static int[,] TileZs{ get; private set; } = new int[_stageSide, _stageSide];
        [SerializeField] private Tilemap[] stageTilemapList = new Tilemap[_stageSide * 2 - 1];
        [SerializeField] private ScriptableObjects.TileData TileData;

        private void Start()
        {
            CreateNewStage();
        }

        public void CreateNewStage()
        {
            SetStageMatrix();
            SetAllTiles();
            GetComponent<ObjectCreator>().CreateNewObjects(3);
        }

        private static void SetStageMatrix()
        {
            //行列の初期化
            InitializeMatrix(TileZs);
            //高さを設定
            SetAllZs();
        }

        private static void InitializeMatrix(int[,] matrix)
        {
            for(int i = 0; i < _stageSide; i++)
                for(int j = 0; j < _stageSide; j++)
                    matrix[i, j] = -1;
        } 
        
        private static void SetAllZs()
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            List<(int startI, int startJ, int endI, int endJ)> stageAreaPointList = new();
            int clossI = Random.Range(2, _stageSide - 2);
            int clossJ = Random.Range(2, _stageSide - 2);
            List<(int startI, int startJ,  int endI, int endJ)> clossPointList = new();
            switch(Random.Range(0, 4))
            {
                case 0:
                    clossPointList.Add((0, clossJ, _stageSide - 1, clossJ));
                    clossPointList.Add((clossI, 0, clossI, clossJ - 1));
                    stageAreaPointList.Add((0, clossJ + 1, _stageSide - 1, _stageSide - 1));
                    stageAreaPointList.Add((clossI + 1, 0, _stageSide - 1, clossJ - 1));
                    stageAreaPointList.Add((0, 0, clossI - 1, clossJ - 1));
                    break;
                case 1:
                    clossPointList.Add((clossI, clossJ + 1, clossI, _stageSide - 1));
                    clossPointList.Add((0, clossJ, _stageSide - 1, clossJ));
                    stageAreaPointList.Add((clossI + 1, clossJ + 1, _stageSide - 1, _stageSide - 1));
                    stageAreaPointList.Add((0, clossJ + 1, clossI - 1, _stageSide - 1));
                    stageAreaPointList.Add((0, 0, _stageSide - 1, clossJ - 1));
                    break;
                case 2:
                    clossPointList.Add((clossI, 0, clossI, _stageSide - 1));
                    clossPointList.Add((0, clossJ, clossI - 1, clossJ));
                    stageAreaPointList.Add((clossI + 1, 0, _stageSide - 1, _stageSide - 1));
                    stageAreaPointList.Add((0, clossJ + 1, clossI - 1, _stageSide - 1));
                    stageAreaPointList.Add((0, 0, clossI - 1, clossJ - 1));
                    break;
                case 3:
                    clossPointList.Add((clossI + 1, clossJ, _stageSide - 1, clossJ));
                    clossPointList.Add((clossI, 0, clossI, _stageSide - 1));
                    stageAreaPointList.Add((clossI + 1, clossJ + 1, _stageSide - 1, _stageSide - 1));
                    stageAreaPointList.Add((clossI + 1, 0, _stageSide - 1, clossJ - 1));
                    stageAreaPointList.Add((0, 0, clossI - 1, _stageSide - 1));
                    break;
            }
            
            int offsetZ = 0; 
            for(int i = 0; i < stageAreaPointList.Count; i++)
            {
                int h = stageAreaPointList[i].endI - stageAreaPointList[i].startI + 1;
                int w = stageAreaPointList[i].endJ - stageAreaPointList[i].startJ + 1;
                int[,] _stageArea = new int[h, w];
                StageElement.AssignStageElementZs(_stageArea);
                for(int I = 0; I < h; I++)
                    for(int J = 0; J < w; J++)
                        TileZs[stageAreaPointList[i].startI + I, stageAreaPointList[i].startJ + J] = _stageArea[I, J] + offsetZ;
                
                if(clossPointList.Count < i + 1) break;
                for(int I = clossPointList[i].startI; I < clossPointList[i].endI + 1; I++)
                    for(int J = clossPointList[i].startJ; J < clossPointList[i].endJ + 1; J++)
                        TileZs[I, J] = _stageArea[0, 0] + offsetZ;
                offsetZ += _stageArea[0, 0] - 1;
            }
        }

        private void SetAllTiles()
        {
            for(int i = 0; i < _stageSide * 2 - 1; i++)
                for(int j = 0; j < _stageSide * 2 - 1; j++)
                {
                    if(i - j >= _stageSide || i - j < 0 || j >= _stageSide) continue;
                    int tileHeight = TileZs[i - j, j];
                    Vector3Int tilePosition = new((_stageSide - 1) / 2 - i + j, (_stageSide - 1) / 2 - j, 0);
                    TileData.SetTile(tileHeight, tilePosition, stageTilemapList[i]);
                }
        }
    }
}
