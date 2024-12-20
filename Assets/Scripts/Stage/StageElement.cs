using System.Collections.Generic;
using Assets.Scripts.Battle;
using UnityEngine;

namespace Assets.Scripts.Stage
{
    public static class StageElement
    {
        private static readonly List<int[,]> _StageElementZs = new()
        {
            new int[4,4]{
                {4, 4, 3, 2},
                {4, 1, 1, 1},
                {3, 1, 1, 1},
                {2, 1, 1, 1}
            },
            new int[4,4]{
                {3, 3, 2, 2},
                {3, 3, 2, 2},
                {4, 4, 1, 1},
                {4, 4, 1, 1}
            },
            new int[4,4]{
                {2, 2, 2, 1},
                {2, 3, 3, 1},
                {2, 3, 3, 1},
                {1, 1, 1, 1}
            },
            new int[4,4]{
                {3, 3, 3, 2},
                {4, 4, 4, 2},
                {4, 4, 4, 1},
                {4, 4, 4, 1}
            },
        }; 

        public static void AssignStageElementZs(int[,] area, int count)
        {
            Random.InitState(BattleData.BattleSeed + count);
            int[,] selectedElementZs = _StageElementZs[Random.Range(0, _StageElementZs.Count)];
            if(Random.Range(0, 2) == 1) 
                selectedElementZs = FlipElementMatrix(selectedElementZs);
            int areaH = area.GetLength(0);
            int areaW = area.GetLength(1);
            int elementH = selectedElementZs.GetLength(0);
            int elementW = selectedElementZs.GetLength(1); 
            for(int i = 0; i < areaH; i++)
                for(int j = 0; j < areaW; j++)
                {
                    int adjustedI;
                    if(i == 0) 
                        adjustedI = 0;
                    else if(i == areaH - 1) 
                        adjustedI = elementH - 1;
                    else 
                        adjustedI = i / (areaH / 2) + 1;

                    int adjustedJ;
                    if(j == 0) 
                        adjustedJ = 0;
                    else if
                        (j == areaW - 1) adjustedJ = elementW - 1;
                    else 
                        adjustedJ = j / (areaW / 2) + 1;
                    area[i, j] = selectedElementZs[adjustedI, adjustedJ];
                }
        }

        private static int[,] FlipElementMatrix(int[,] matrix)
        {
            int[,] newMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
            for(int i = 0; i < matrix.GetLength(0); i++)
                for(int j = i; j < matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = matrix[j, i];
                    newMatrix[j, i] = matrix[i, j];
                }
            return newMatrix;
        }
    }
}