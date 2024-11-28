using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.EnemyDamageObject;
using Assets.Scripts.Player;

namespace Assets.Scripts.Objects
{
    public class ObjectFacade : MonoBehaviour
    {
        private static PlayerMain player;
        private static List<IEnemyMain> enemyList = new();
        private static List<(EnemyDamageObjectMain enemyDamageObject, IEnemyMain enemy)> enemyDamageObjectList = new();
        private static ObjectCreator singletonObjectCreator;
        
        public static void AddPlayer(PlayerMain newPlayer)
        {
            player = newPlayer;
        }

        public static void RemovePlayer()
        {
            player = null;
        }

        public static void RemoveAndDestroyAlivePlayer()
        {
            if(player == null) return;
            player.DestroyAliveObject();
        }

        public static void AddEnemy(IEnemyMain enemy)
        {
            enemyList.Add(enemy);
        }

        public static void RemoveEnemy(IEnemyMain enemy)
        {
            enemyList.Remove(enemy);
        }

        public static void RemoveAndDestroyEnemyDamageObject(IEnemyMain enemy)
        {
            int i = 0;
            while(true)
            {
                if(i >= enemyDamageObjectList.Count) break;
                if(enemyDamageObjectList[i].enemy != enemy)
                {
                    i++;
                    continue;
                }
                enemyDamageObjectList[i].enemyDamageObject.DestroyObject();
            }
        }

        public static void RemoveAndDestroyAll()
        {
            int enemyListCount = enemyList.Count;
            if(player != null) player.DestroyAliveObject();
            for(int i = 0; i < enemyListCount; i++)
                enemyList[0].DestroyAliveObject();
        }

        public static void AddEnemyDamageObject(EnemyDamageObjectMain enemyDamageObject, IEnemyMain enemy)
        {
            enemyDamageObjectList.Add((enemyDamageObject, enemy));
        }

        public static void RemoveEnemyDamageObject(EnemyDamageObjectMain enemyDamageObject)
        {
            for(int i = 0; i < enemyDamageObjectList.Count; i++)
            {
                if(enemyDamageObjectList[i].enemyDamageObject != enemyDamageObject)
                    continue;
                enemyDamageObjectList.RemoveAt(i);
                return;
            }
        }

        public static void SetAllObjectsReady()
        {
            if(player == null) return;
            player.SetReady();
            foreach(IEnemyMain enemy in enemyList)
                enemy.SetReady();
        }

        public static Vector3 GetPlayerRePos3()
        {
            if(player == null) return Vector3.zero;
            return player.transform.position;
        }

        public static bool IsPlayerLiving()
        {
            return player != null;
        }

        public static bool IsEnemyLiving()
        {
            return enemyList.Count > 0 || enemyDamageObjectList.Count > 0;
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
            ObjectHittingCalculator.CalculateHitting(player, enemyList, enemyDamageObjectList);
        }

        public static void CreateNewObjects()
        {
            singletonObjectCreator.CreateNewObjects();
        }
    }
}