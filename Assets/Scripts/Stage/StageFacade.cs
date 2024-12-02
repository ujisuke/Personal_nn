using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Stage
{   
    public class StageFacade : MonoBehaviour
    {
        public static int StageSide => StageCreator._StageSide;
        public static int StageHeight => StageCreator._StageHeight;
        public static float TileHeight => StageCreator._TileHeight;
        public static float YOffset => StageCreator._YOffset;
        public static int[,] TileImZs => StageCreator.TileImZs;
        private static StageCreator singletonStageCreator;
        public static bool IsCreatingStage => StageCreator.IsCreatingStage;

        private void Awake()
        {
            singletonStageCreator = GetComponent<StageCreator>();
        }

        public static async UniTask CreateBattleStage()
        {
            await singletonStageCreator.CreateBattleStage();
        }

        public static async UniTask CreateLobbyStage()
        {
            while (singletonStageCreator == null)
                await UniTask.DelayFrame(1); 
            await singletonStageCreator.CreateLobbyStage();
        }
    }
}
