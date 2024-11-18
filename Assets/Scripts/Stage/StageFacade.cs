using UnityEngine;

namespace Assets.Scripts.Stage
{   
    public class StageFacade : MonoBehaviour
    {
        public const int _stageSide = 8;
        public const int _stageHeight = _stageSide * 2;
        public const float _tileHeight = 0.25f;
        public static int[,] TileZs{ get; private set; } = new int[_stageSide, _stageSide];
        private static StageCreator singletonStageCreator;

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
