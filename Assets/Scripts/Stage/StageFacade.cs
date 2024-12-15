using Cysharp.Threading.Tasks;
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

        public static async UniTask CreateBattleStage()
        {
            await StageCreator.SingletonInstance.CreateBattleStage();
        }

        public static async UniTask CreateLobbyStage()
        {
            await StageCreator.SingletonInstance.CreateLobbyStage();
        }
    }
}
