using NUnit.Framework;
using UnityEngine;

namespace Assets.Scripts.Stage
{   
    public class StageFacade : MonoBehaviour
    {
        public const int _StageSide = 8;
        public const int _StageHeight = _StageSide * 2;
        public const float _TileHeight = 0.25f;
        public const float _YOffset = 1.75f;
        public static int[,] TileImZs{ get; private set; } = new int[_StageSide, _StageSide];
        private static StageCreator singletonStageCreator;
        public static bool IsCreatingStage = false;

        private void Awake()
        {
            singletonStageCreator = GetComponent<StageCreator>();
        }

        public static void CreateNewStage()
        {
            singletonStageCreator.CreateNewStage();
        }
    }
}
