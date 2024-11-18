using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ObjectFacade : MonoBehaviour
    {
        private static IObject player;
        private static List<IObject> enemyList = new();
        private static ObjectCreator singletonObjectCreator;
        
        public static void AddPlayer(IObject newPlayer)
        {
            player = newPlayer;
        }

        public static void RemovePlayer()
        {
            player = null;
        }

        public static void RemoveAndDestroyPlayer()
        {
            if(player == null) return;
            player.DestroyObject();
        }

        public static void AddEnemy(IObject newEnemy)
        {
            enemyList.Add(newEnemy);
        }

        public static void RemoveEnemy(IObject obj)
        {
            enemyList.Remove(obj);
        }

        public static void RemoveAndDestroyAllEnemies()
        {
            int enemyListCount = enemyList.Count;
            for(int i = 0; i < enemyListCount; i++)
                enemyList[0].DestroyObject();
        }

        public static void SetAllObjectsReady()
        {
            if(player == null) return;
            player.SetReady();
            foreach(IObject enemy in enemyList)
                enemy.SetReady();
        }

        public static Vector3 GetPlayerRePos3()
        {
            if(player == null) return Vector3.zero;
            return player.GetRePos3();
        }

        public static bool IsPlayerDead()
        {
            return false;
        }

        public static bool IsEnemyLiving()
        {
            return enemyList.Count > 0;
        }

        private void Awake()
        {
            singletonObjectCreator = GetComponent<ObjectCreator>();
        }

        private void FixedUpdate()
        {
            ExecuteObjectAttack();
        }

        private static void ExecuteObjectAttack()
        {
            ObjectHittingCalculator.CalculateHitting(player, enemyList);
        }

        public static void CreateNewObjects()
        {
            singletonObjectCreator.CreateNewObjects();
        }
    }
}