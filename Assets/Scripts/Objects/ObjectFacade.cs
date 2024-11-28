using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ObjectFacade : MonoBehaviour
    {
        public static void ClearAllObjects()
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

        public static void CreateNewObjects()
        {
            ObjectStorage.CreateNewObjects();
        }
    }
}