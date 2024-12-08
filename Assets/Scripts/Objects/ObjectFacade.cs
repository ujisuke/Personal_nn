using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ObjectFacade : MonoBehaviour
    {
        public static void CleanAllObjects()
        {
            ObjectStorage.CleanAllObjects();
        }

        public static bool IsPlayerLiving()
        {
            return ObjectStorage.IsPlayerLiving();
        }

        public static bool IsEnemyLiving()
        {
            return ObjectStorage.IsEnemyLiving();
        }

        public static async UniTask CreateBattleObjects()
        {
            await ObjectCreator.CreateBattleObjects();
        }

        public static async UniTask CreateLobbyObjects()
        {
            await ObjectCreator.CreateLobbyObjects();
        }

        public static async UniTask CreateSettingObjects()
        {
            await ObjectCreator.CreateSettingObjects();
        }

        public static bool IsStartBattleEnemyLiving()
        {
            return ObjectStorage.IsStartBattleEnemyLiving;
        }

        public static bool IsSetGameEnemyLiving()
        {
            return ObjectStorage.IsSetGameEnemyLiving;
        }

        public static bool IsBackToLobbyEnemyLiving()
        {
            return ObjectStorage.IsBackToLobbyEnemyLiving;
        }
    }
}