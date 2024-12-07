using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Assets.Scripts.Battle;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Stage
{
    public class StageCreator : MonoBehaviour
    {
        [SerializeField] private Tilemap[] stageTilemapList = new Tilemap[_StageSide * 2 - 1];
        private static Tilemap[] _singletonStageTilemapList;
        [SerializeField] private ScriptableObjects.TileData _tileData;
        private static ScriptableObjects.TileData _singletonTileData;

        public const int _StageSide = 8;
        public const int _StageHeight = _StageSide * 2;
        public const float _TileHeight = 0.25f;
        public const float _YOffset = 1.75f;
        public static int[,] TileImZs{ get; private set; } = new int[_StageSide, _StageSide];

        private void Awake()
        {
            _singletonStageTilemapList = stageTilemapList;
            _singletonTileData = _tileData;
        }

        public static async UniTask CreateLobbyStage()
        {
            SetLobbyStageMatrix();
            await SetAllTiles();
        }

        private static void SetLobbyStageMatrix()
        {
            InitializeMatrix(TileImZs);
            for(int i = 0; i < _StageSide; i++)
                for(int j = 0; j < _StageSide; j++)
                    TileImZs[i, j] = 1;
        }

        public static async UniTask CreateSettingStage()
        {
            SetSettingStageMatrix();
            await SetAllTiles();
        }

        private static void SetSettingStageMatrix()
        {
            InitializeMatrix(TileImZs);
            for(int i = 0; i < _StageSide; i++)
                for(int j = 0; j < _StageSide; j++)
                    TileImZs[i, j] = 2;
        }

        public static async UniTask CreateBattleStage()
        {
            SetNewStageMatrix();
            await SetAllTiles();
        }

        private static void SetNewStageMatrix()
        {
            InitializeMatrix(TileImZs);
            SetAllZs();
        }

        private static void InitializeMatrix(int[,] matrix)
        {
            for(int i = 0; i < _StageSide; i++)
                for(int j = 0; j < _StageSide; j++)
                    matrix[i, j] = -1;
        } 
        
        private static void SetAllZs()
        {
            if(BattleFacade.StageDifficulty == 1)
                SetAllZsWhenDifficultyIs1();
            else if(BattleFacade.StageDifficulty == 2)
                SetAllZsWhenDifficultyIs2();
            else if(BattleFacade.StageDifficulty >= 3)
                SetAllZsWhenDifficultyIs3OrMoreThan();
        }

        private static void SetAllZsWhenDifficultyIs1()
        {
            int[,] _stageArea = new int[_StageSide, _StageSide];
            StageElement.AssignStageElementZs(_stageArea);
            for(int i = 0; i < _StageSide; i++)
                for(int j = 0; j < _StageSide; j++)
                    TileImZs[i, j] = _stageArea[i, j];
        }

        private static void SetAllZsWhenDifficultyIs2()
        {
            Random.InitState(DateTime.Now.Millisecond);
            List<(int startI, int startJ, int endI, int endJ)> stageAreaPointList = new();
            int clossI = Random.Range(2, _StageSide - 2);
            int clossJ = Random.Range(2, _StageSide - 2);
            List<(int startI, int startJ,  int endI, int endJ)> clossPointList = new();
            switch(Random.Range(0, 2))
            {
                case 0:
                    clossPointList.Add((0, clossJ, _StageSide - 1, clossJ));
                    stageAreaPointList.Add((0, clossJ + 1, _StageSide - 1, _StageSide - 1));
                    stageAreaPointList.Add((0, 0, _StageSide - 1, clossJ - 1));
                    break;
                case 1:
                    clossPointList.Add((clossI, 0, clossI, _StageSide - 1));
                    stageAreaPointList.Add((clossI + 1, 0, _StageSide - 1, _StageSide - 1));
                    stageAreaPointList.Add((0, 0, clossI - 1, _StageSide - 1));
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
                        TileImZs[stageAreaPointList[i].startI + I, stageAreaPointList[i].startJ + J] = _stageArea[I, J] + offsetZ;
                
                if(clossPointList.Count < i + 1) break;
                for(int I = clossPointList[i].startI; I < clossPointList[i].endI + 1; I++)
                    for(int J = clossPointList[i].startJ; J < clossPointList[i].endJ + 1; J++)
                        TileImZs[I, J] = _stageArea[0, 0] + offsetZ;
                offsetZ += _stageArea[0, 0] - 1;
            }
        }


        private static void SetAllZsWhenDifficultyIs3OrMoreThan()
        {
            Random.InitState(DateTime.Now.Millisecond);
            List<(int startI, int startJ, int endI, int endJ)> stageAreaPointList = new();
            int clossI = Random.Range(2, _StageSide - 2);
            int clossJ = Random.Range(2, _StageSide - 2);
            List<(int startI, int startJ,  int endI, int endJ)> clossPointList = new();
            switch(Random.Range(0, 4))
            {
                case 0:
                    clossPointList.Add((0, clossJ, _StageSide - 1, clossJ));
                    clossPointList.Add((clossI, 0, clossI, clossJ - 1));
                    stageAreaPointList.Add((0, clossJ + 1, _StageSide - 1, _StageSide - 1));
                    stageAreaPointList.Add((clossI + 1, 0, _StageSide - 1, clossJ - 1));
                    stageAreaPointList.Add((0, 0, clossI - 1, clossJ - 1));
                    break;
                case 1:
                    clossPointList.Add((clossI, clossJ + 1, clossI, _StageSide - 1));
                    clossPointList.Add((0, clossJ, _StageSide - 1, clossJ));
                    stageAreaPointList.Add((clossI + 1, clossJ + 1, _StageSide - 1, _StageSide - 1));
                    stageAreaPointList.Add((0, clossJ + 1, clossI - 1, _StageSide - 1));
                    stageAreaPointList.Add((0, 0, _StageSide - 1, clossJ - 1));
                    break;
                case 2:
                    clossPointList.Add((clossI, 0, clossI, _StageSide - 1));
                    clossPointList.Add((0, clossJ, clossI - 1, clossJ));
                    stageAreaPointList.Add((clossI + 1, 0, _StageSide - 1, _StageSide - 1));
                    stageAreaPointList.Add((0, clossJ + 1, clossI - 1, _StageSide - 1));
                    stageAreaPointList.Add((0, 0, clossI - 1, clossJ - 1));
                    break;
                case 3:
                    clossPointList.Add((clossI + 1, clossJ, _StageSide - 1, clossJ));
                    clossPointList.Add((clossI, 0, clossI, _StageSide - 1));
                    stageAreaPointList.Add((clossI + 1, clossJ + 1, _StageSide - 1, _StageSide - 1));
                    stageAreaPointList.Add((clossI + 1, 0, _StageSide - 1, clossJ - 1));
                    stageAreaPointList.Add((0, 0, clossI - 1, _StageSide - 1));
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
                        TileImZs[stageAreaPointList[i].startI + I, stageAreaPointList[i].startJ + J] = _stageArea[I, J] + offsetZ;
                
                if(clossPointList.Count < i + 1) break;
                for(int I = clossPointList[i].startI; I < clossPointList[i].endI + 1; I++)
                    for(int J = clossPointList[i].startJ; J < clossPointList[i].endJ + 1; J++)
                        TileImZs[I, J] = _stageArea[0, 0] + offsetZ;
                offsetZ += _stageArea[0, 0] - 1;
            }
        }

        private static async UniTask SetAllTiles()
        {
            for(int n = 0; n < _StageSide * 2 - 1; n++)
            {
                for(int m = 0; m < _StageSide; m++)
                {
                    (int i, int j) = (_StageSide - 1 - m, _StageSide - 1 - n + m);
                    if(i >= _StageSide || i < 0 || j >= _StageSide || j < 0) continue;
                    int tileHeight = TileImZs[i, j];
                    Vector3Int tilePosition = new((_StageSide - 1) / 2 - i, (_StageSide - 1) / 2 - j, 0);
                    _singletonTileData.SetTile(tileHeight, tilePosition, _singletonStageTilemapList[_StageSide * 2 - 2 - n]);
                }
                await UniTask.Delay(TimeSpan.FromSeconds(0.03f));
            }
        }
    }
}
