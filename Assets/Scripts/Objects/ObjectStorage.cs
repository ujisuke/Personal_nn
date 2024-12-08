using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.EnemyDamageObject;
using Assets.Scripts.Player;

namespace Assets.Scripts.Objects
{
    public class ObjectStorage : MonoBehaviour
    {
        private static PlayerMain player;
        private static List<EnemyMain> enemyList = new();
        private static List<(EnemyDamageObjectMain enemyDamageObject, EnemyMain enemy)> enemyDamageObjectList = new();
        public static bool IsStartBattleEnemyLiving = false;
        public static bool IsSetGameEnemyLiving = false;
        public static bool IsBackToLobbyEnemyLiving = false;

        private void FixedUpdate()
        {
            ObjectHittingCalculator.CalculateHitting(player, enemyList, enemyDamageObjectList);
        }

        public static void AddPlayer(PlayerMain newPlayer)
        {
            player = newPlayer;
        }

        public static void RemovePlayer()
        {
            player = null;
        }

        public static void AddEnemy(EnemyMain enemy)
        {
            enemyList.Add(enemy);
        }

        public static void RemoveEnemyAndDestroyDamageObject(EnemyMain enemy)
        {
            enemyList.Remove(enemy);
            RemoveAndDestroyEnemyDamageObjects(enemy);
        }

        private static void RemoveAndDestroyEnemyDamageObjects(EnemyMain enemy)
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

        public static void CleanAllObjects()
        {
            if(player != null) 
                player.SetCleaned();
            for(int i = 0; i < enemyList.Count; i++)
                enemyList[i].SetCleaned();
            int enemyDamageObjectListCount = enemyDamageObjectList.Count;
            for(int i = 0; i < enemyDamageObjectListCount; i++)
                enemyDamageObjectList[0].enemyDamageObject.DestroyObject();
        }

        public static void AddEnemyDamageObject(EnemyDamageObjectMain enemyDamageObject, EnemyMain enemy)
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
            if(player == null)
                return;
            player.SetReady();
            foreach(EnemyMain enemy in enemyList)
                enemy.SetReady();
        }

        public static Vector3 GetPlayerRePos3()
        {
            if(player == null)
                return Vector3.zero;
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
    }
}