using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.ScriptableObjects;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Assets.Scripts.Battle;

namespace Assets.Scripts.Stage
{
    public class StageCreator : MonoBehaviour
    {
        [SerializeField] private Tilemap[] stageTilemapList = new Tilemap[StageFacade._stageSide * 2 - 1];
        [SerializeField] private ScriptableObjects.TileData TileData;

        public void CreateNewStage()
        {
            SetStageMatrix();
            SetAllTiles();
        }

        private static void SetStageMatrix()
        {
            //行列の初期化
            InitializeMatrix(StageFacade.TileZs);
            //高さを設定
            SetAllZs();
        }

        private static void InitializeMatrix(int[,] matrix)
        {
            for(int i = 0; i < StageFacade._stageSide; i++)
                for(int j = 0; j < StageFacade._stageSide; j++)
                    matrix[i, j] = -1;
        } 
        
        private static void SetAllZs()
        {
            if(BattleFacade.Difficulty == 1)
                SetAllZsWhenDifficultyIs1();
            else if(BattleFacade.Difficulty == 2)
                SetAllZsWhenDifficultyIs2();
            else if(BattleFacade.Difficulty == 3)
                SetAllZsWhenDifficultyIs3();
        }

        private static void SetAllZsWhenDifficultyIs1()
        {
            int[,] _stageArea = new int[StageFacade._stageSide, StageFacade._stageSide];
            StageElement.AssignStageElementZs(_stageArea);
            for(int i = 0; i < StageFacade._stageSide; i++)
                for(int j = 0; j < StageFacade._stageSide; j++)
                    StageFacade.TileZs[i, j] = _stageArea[i, j];
        }

        private static void SetAllZsWhenDifficultyIs2()
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            List<(int startI, int startJ, int endI, int endJ)> stageAreaPointList = new();
            int clossI = Random.Range(2, StageFacade._stageSide - 2);
            int clossJ = Random.Range(2, StageFacade._stageSide - 2);
            List<(int startI, int startJ,  int endI, int endJ)> clossPointList = new();
            switch(Random.Range(0, 2))
            {
                case 0:
                    clossPointList.Add((0, clossJ, StageFacade._stageSide - 1, clossJ));
                    stageAreaPointList.Add((0, clossJ + 1, StageFacade._stageSide - 1, StageFacade._stageSide - 1));
                    stageAreaPointList.Add((0, 0, StageFacade._stageSide - 1, clossJ - 1));
                    break;
                case 1:
                    clossPointList.Add((clossI, 0, clossI, StageFacade._stageSide - 1));
                    stageAreaPointList.Add((clossI + 1, 0, StageFacade._stageSide - 1, StageFacade._stageSide - 1));
                    stageAreaPointList.Add((0, 0, clossI - 1, StageFacade._stageSide - 1));
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
                        StageFacade.TileZs[stageAreaPointList[i].startI + I, stageAreaPointList[i].startJ + J] = _stageArea[I, J] + offsetZ;
                
                if(clossPointList.Count < i + 1) break;
                for(int I = clossPointList[i].startI; I < clossPointList[i].endI + 1; I++)
                    for(int J = clossPointList[i].startJ; J < clossPointList[i].endJ + 1; J++)
                        StageFacade.TileZs[I, J] = _stageArea[0, 0] + offsetZ;
                offsetZ += _stageArea[0, 0] - 1;
            }
        }


        private static void SetAllZsWhenDifficultyIs3()
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            List<(int startI, int startJ, int endI, int endJ)> stageAreaPointList = new();
            int clossI = Random.Range(2, StageFacade._stageSide - 2);
            int clossJ = Random.Range(2, StageFacade._stageSide - 2);
            List<(int startI, int startJ,  int endI, int endJ)> clossPointList = new();
            switch(Random.Range(0, 4))
            {
                case 0:
                    clossPointList.Add((0, clossJ, StageFacade._stageSide - 1, clossJ));
                    clossPointList.Add((clossI, 0, clossI, clossJ - 1));
                    stageAreaPointList.Add((0, clossJ + 1, StageFacade._stageSide - 1, StageFacade._stageSide - 1));
                    stageAreaPointList.Add((clossI + 1, 0, StageFacade._stageSide - 1, clossJ - 1));
                    stageAreaPointList.Add((0, 0, clossI - 1, clossJ - 1));
                    break;
                case 1:
                    clossPointList.Add((clossI, clossJ + 1, clossI, StageFacade._stageSide - 1));
                    clossPointList.Add((0, clossJ, StageFacade._stageSide - 1, clossJ));
                    stageAreaPointList.Add((clossI + 1, clossJ + 1, StageFacade._stageSide - 1, StageFacade._stageSide - 1));
                    stageAreaPointList.Add((0, clossJ + 1, clossI - 1, StageFacade._stageSide - 1));
                    stageAreaPointList.Add((0, 0, StageFacade._stageSide - 1, clossJ - 1));
                    break;
                case 2:
                    clossPointList.Add((clossI, 0, clossI, StageFacade._stageSide - 1));
                    clossPointList.Add((0, clossJ, clossI - 1, clossJ));
                    stageAreaPointList.Add((clossI + 1, 0, StageFacade._stageSide - 1, StageFacade._stageSide - 1));
                    stageAreaPointList.Add((0, clossJ + 1, clossI - 1, StageFacade._stageSide - 1));
                    stageAreaPointList.Add((0, 0, clossI - 1, clossJ - 1));
                    break;
                case 3:
                    clossPointList.Add((clossI + 1, clossJ, StageFacade._stageSide - 1, clossJ));
                    clossPointList.Add((clossI, 0, clossI, StageFacade._stageSide - 1));
                    stageAreaPointList.Add((clossI + 1, clossJ + 1, StageFacade._stageSide - 1, StageFacade._stageSide - 1));
                    stageAreaPointList.Add((clossI + 1, 0, StageFacade._stageSide - 1, clossJ - 1));
                    stageAreaPointList.Add((0, 0, clossI - 1, StageFacade._stageSide - 1));
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
                        StageFacade.TileZs[stageAreaPointList[i].startI + I, stageAreaPointList[i].startJ + J] = _stageArea[I, J] + offsetZ;
                
                if(clossPointList.Count < i + 1) break;
                for(int I = clossPointList[i].startI; I < clossPointList[i].endI + 1; I++)
                    for(int J = clossPointList[i].startJ; J < clossPointList[i].endJ + 1; J++)
                        StageFacade.TileZs[I, J] = _stageArea[0, 0] + offsetZ;
                offsetZ += _stageArea[0, 0] - 1;
            }
        }

        private void SetAllTiles()
        {
            for(int i = 0; i < StageFacade._stageSide * 2 - 1; i++)
                for(int j = 0; j < StageFacade._stageSide * 2 - 1; j++)
                {
                    if(i - j >= StageFacade._stageSide || i - j < 0 || j >= StageFacade._stageSide) continue;
                    int tileHeight = StageFacade.TileZs[i - j, j];
                    Vector3Int tilePosition = new((StageFacade._stageSide - 1) / 2 - i + j, (StageFacade._stageSide - 1) / 2 - j, 0);
                    TileData.SetTile(tileHeight, tilePosition, stageTilemapList[i]);
                }
        }
    }
}
