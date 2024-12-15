using System.Collections.Generic;
using Assets.Scripts.Battle;
using UnityEngine;

namespace Assets.Scripts.Stage
{
    public static class StageElement
    {
        private static readonly List<int[,]> _StageElementZs = new()
        {
            new int[3,3]{
                {4, 4, 3},
                {4, 4, 2},
                {4, 4, 1}
            },
            new int[3,3]{
                {4, 3, 2},
                {1, 1, 1},
                {1, 1, 1}
            },
            new int[3,3]{
                {4, 2, 2},
                {3, 2, 1},
                {3, 3, 1}
            },
            new int[3,3]{
                {3, 1, 2},
                {3, 1, 2},
                {3, 1, 2}
            },
            new int[3,3]{
                {3, 3, 3},
                {2, 2, 2},
                {1, 1, 1}
            }
        }; 

        public static void AssignStageElementZs(int[,] area)
        {
            Random.InitState(BattleData.Seed);
            int[,] selectedElementZs = _StageElementZs[Random.Range(0, _StageElementZs.Count)];
            if(Random.Range(0, 2) == 1) FlipElementMatrix(selectedElementZs);
            
            int areaH = area.GetLength(0);
            int areaW = area.GetLength(1);
            int elementH = selectedElementZs.GetLength(0);
            int elementW = selectedElementZs.GetLength(1); 
            for(int i = 0; i < areaH; i++)
                for(int j = 0; j < areaW; j++)
                {
                    int adjustedI = 1;
                    if(i == 0) adjustedI = 0;
                    else if(i == areaH - 1) adjustedI = elementH - 1;

                    int adjustedJ = 1;
                    if(j == 0) adjustedJ = 0;
                    else if(j == areaW - 1) adjustedJ = elementW - 1;

                    area[i, j] = selectedElementZs[adjustedI, adjustedJ];
                }
        }

        private static void FlipElementMatrix(int[,] matrix)
        {
            int h = matrix.GetLength(0);
            int w = matrix.GetLength(1);
            for(int i = 0; i < h; i++)
                for(int j = i; j < w; j++)
                    (matrix[i, j], matrix[j, i]) = (matrix[j, i], matrix[i, j]);
        }
    }
}