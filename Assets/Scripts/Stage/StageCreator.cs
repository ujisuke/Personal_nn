using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.ScriptableObjects;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Assets.Scripts.Battle;
using System.Collections;

namespace Assets.Scripts.Stage
{
    public class StageCreator : MonoBehaviour
    {
        [SerializeField] private Tilemap[] stageTilemapList = new Tilemap[StageFacade._StageSide * 2 - 1];
        [SerializeField] private ScriptableObjects.TileData TileData;

        public void CreateNewStage()
        {
            StageFacade.IsCreatingStage = true;
            SetStageMatrix();
            StartCoroutine(SetAllTiles());
        }

        private static void SetStageMatrix()
        {
            //行列の初期化
            InitializeMatrix(StageFacade.TileImZs);
            //高さを設定
            SetAllZs();
        }

        private static void InitializeMatrix(int[,] matrix)
        {
            for(int i = 0; i < StageFacade._StageSide; i++)
                for(int j = 0; j < StageFacade._StageSide; j++)
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
            int[,] _stageArea = new int[StageFacade._StageSide, StageFacade._StageSide];
            StageElement.AssignStageElementZs(_stageArea);
            for(int i = 0; i < StageFacade._StageSide; i++)
                for(int j = 0; j < StageFacade._StageSide; j++)
                    StageFacade.TileImZs[i, j] = _stageArea[i, j];
        }

        private static void SetAllZsWhenDifficultyIs2()
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            List<(int startI, int startJ, int endI, int endJ)> stageAreaPointList = new();
            int clossI = Random.Range(2, StageFacade._StageSide - 2);
            int clossJ = Random.Range(2, StageFacade._StageSide - 2);
            List<(int startI, int startJ,  int endI, int endJ)> clossPointList = new();
            switch(Random.Range(0, 2))
            {
                case 0:
                    clossPointList.Add((0, clossJ, StageFacade._StageSide - 1, clossJ));
                    stageAreaPointList.Add((0, clossJ + 1, StageFacade._StageSide - 1, StageFacade._StageSide - 1));
                    stageAreaPointList.Add((0, 0, StageFacade._StageSide - 1, clossJ - 1));
                    break;
                case 1:
                    clossPointList.Add((clossI, 0, clossI, StageFacade._StageSide - 1));
                    stageAreaPointList.Add((clossI + 1, 0, StageFacade._StageSide - 1, StageFacade._StageSide - 1));
                    stageAreaPointList.Add((0, 0, clossI - 1, StageFacade._StageSide - 1));
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
                        StageFacade.TileImZs[stageAreaPointList[i].startI + I, stageAreaPointList[i].startJ + J] = _stageArea[I, J] + offsetZ;
                
                if(clossPointList.Count < i + 1) break;
                for(int I = clossPointList[i].startI; I < clossPointList[i].endI + 1; I++)
                    for(int J = clossPointList[i].startJ; J < clossPointList[i].endJ + 1; J++)
                        StageFacade.TileImZs[I, J] = _stageArea[0, 0] + offsetZ;
                offsetZ += _stageArea[0, 0] - 1;
            }
        }


        private static void SetAllZsWhenDifficultyIs3()
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            List<(int startI, int startJ, int endI, int endJ)> stageAreaPointList = new();
            int clossI = Random.Range(2, StageFacade._StageSide - 2);
            int clossJ = Random.Range(2, StageFacade._StageSide - 2);
            List<(int startI, int startJ,  int endI, int endJ)> clossPointList = new();
            switch(Random.Range(0, 4))
            {
                case 0:
                    clossPointList.Add((0, clossJ, StageFacade._StageSide - 1, clossJ));
                    clossPointList.Add((clossI, 0, clossI, clossJ - 1));
                    stageAreaPointList.Add((0, clossJ + 1, StageFacade._StageSide - 1, StageFacade._StageSide - 1));
                    stageAreaPointList.Add((clossI + 1, 0, StageFacade._StageSide - 1, clossJ - 1));
                    stageAreaPointList.Add((0, 0, clossI - 1, clossJ - 1));
                    break;
                case 1:
                    clossPointList.Add((clossI, clossJ + 1, clossI, StageFacade._StageSide - 1));
                    clossPointList.Add((0, clossJ, StageFacade._StageSide - 1, clossJ));
                    stageAreaPointList.Add((clossI + 1, clossJ + 1, StageFacade._StageSide - 1, StageFacade._StageSide - 1));
                    stageAreaPointList.Add((0, clossJ + 1, clossI - 1, StageFacade._StageSide - 1));
                    stageAreaPointList.Add((0, 0, StageFacade._StageSide - 1, clossJ - 1));
                    break;
                case 2:
                    clossPointList.Add((clossI, 0, clossI, StageFacade._StageSide - 1));
                    clossPointList.Add((0, clossJ, clossI - 1, clossJ));
                    stageAreaPointList.Add((clossI + 1, 0, StageFacade._StageSide - 1, StageFacade._StageSide - 1));
                    stageAreaPointList.Add((0, clossJ + 1, clossI - 1, StageFacade._StageSide - 1));
                    stageAreaPointList.Add((0, 0, clossI - 1, clossJ - 1));
                    break;
                case 3:
                    clossPointList.Add((clossI + 1, clossJ, StageFacade._StageSide - 1, clossJ));
                    clossPointList.Add((clossI, 0, clossI, StageFacade._StageSide - 1));
                    stageAreaPointList.Add((clossI + 1, clossJ + 1, StageFacade._StageSide - 1, StageFacade._StageSide - 1));
                    stageAreaPointList.Add((clossI + 1, 0, StageFacade._StageSide - 1, clossJ - 1));
                    stageAreaPointList.Add((0, 0, clossI - 1, StageFacade._StageSide - 1));
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
                        StageFacade.TileImZs[stageAreaPointList[i].startI + I, stageAreaPointList[i].startJ + J] = _stageArea[I, J] + offsetZ;
                
                if(clossPointList.Count < i + 1) break;
                for(int I = clossPointList[i].startI; I < clossPointList[i].endI + 1; I++)
                    for(int J = clossPointList[i].startJ; J < clossPointList[i].endJ + 1; J++)
                        StageFacade.TileImZs[I, J] = _stageArea[0, 0] + offsetZ;
                offsetZ += _stageArea[0, 0] - 1;
            }
        }

        private IEnumerator SetAllTiles()
        {
            for(int n = 0; n < StageFacade._StageSide * 2 - 1; n++)
            {
                for(int m = 0; m < StageFacade._StageSide; m++)
                {
                    (int i, int j) = (StageFacade._StageSide - 1 - m, StageFacade._StageSide - 1 - n + m);
                    if(i >= StageFacade._StageSide || i < 0 || j >= StageFacade._StageSide || j < 0) continue;
                    int tileHeight = StageFacade.TileImZs[i, j];
                    Vector3Int tilePosition = new((StageFacade._StageSide - 1) / 2 - i, (StageFacade._StageSide - 1) / 2 - j, 0);
                    TileData.SetTile(tileHeight, tilePosition, stageTilemapList[14 - n]);
                }
                yield return new WaitForSeconds(0.03f);
            }
            StageFacade.IsCreatingStage = false;
        }
    }
}
