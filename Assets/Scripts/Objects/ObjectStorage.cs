using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.EnemyDamageObject;
using Assets.Scripts.Player;
using Assets.Scripts.StartBattleEnemy;
using Assets.Scripts.SetGameEnemy;
using Assets.Scripts.BackToLobbyEnemy;

namespace Assets.Scripts.Objects
{
    public class ObjectStorage : MonoBehaviour
    {
        private static PlayerMain player;
        private static List<IEnemyMain> enemyList = new();
        private static List<(EnemyDamageObjectMain enemyDamageObject, IEnemyMain enemy)> enemyDamageObjectList = new();

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

        public static void AddEnemy(IEnemyMain enemy)
        {
            enemyList.Add(enemy);
        }

        public static void RemoveEnemy(IEnemyMain enemy)
        {
            enemyList.Remove(enemy);
        }

        public static void RemoveAndDestroyEnemyDamageObjects(IEnemyMain enemy)
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

        public static void KillAllObjects()
        {
            if(player != null) 
                PlayerMain.KillAliveObject();
            for(int i = 0; i < enemyList.Count; i++)
                enemyList[i].KillAliveObject();
            int enemyDamageObjectListCount = enemyDamageObjectList.Count;
            for(int i = 0; i < enemyDamageObjectListCount; i++)
                enemyDamageObjectList[0].enemyDamageObject.DestroyObject();
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
            if(player == null)
                return;
            player.SetReady();
            foreach(IEnemyMain enemy in enemyList)
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

        public static bool IsStartBattleEnemyLiving()
        {
            foreach (var enemy in enemyList)
                if (enemy is StartBattleEnemyMain)
                    return true;
            return false;
        }

        public static bool IsSetGameEnemyLiving()
        {
            foreach (var enemy in enemyList)
                if (enemy is SetGameEnemyMain)
                    return true;
            return false;
        }

        public static bool IsBackToLobbyEnemyLiving()
        {
            foreach (var enemy in enemyList)
                if (enemy is BackToLobbyEnemyMain)
                    return true;
            return false;
        }
    }
}