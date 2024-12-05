using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ObjectFacade : MonoBehaviour
    {
        public static void ClearObjects()
        {
            ObjectStorage.RemoveAndDestroyAll();
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
            await ObjectStorage.CreateBattleObjects();
        }

        public static async UniTask CreateLobbyObjects()
        {
            await ObjectStorage.CreateLobbyObjects();
        }

        public static async UniTask CreateSettingObjects()
        {
            await ObjectStorage.CreateSettingObjects();
        }

        public static bool IsStartBattleEnemyLiving()
        {
            return ObjectStorage.IsStartBattleEnemyLiving();
        }

        public static bool IsSettingEnemyLiving()
        {
            return ObjectStorage.IsSettingEnemyLiving();
        }

        public static bool IsBackToLobbyEnemyLiving()
        {
            return ObjectStorage.IsBackToLobbyEnemyLiving();
        }
    }
}