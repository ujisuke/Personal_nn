using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.EnemyDamageObject;
using Assets.Scripts.Player;

namespace Assets.Scripts.Objects
{
    public class ObjectStorage : MonoBehaviour
    {
        private static PlayerMain player;
        private static List<EnemyGroup> enemyGroupList = new();
        public static bool IsStartBattleEnemyLiving = false;
        public static bool IsSetGameEnemyLiving = false;

        private void FixedUpdate()
        {
            ObjectHittingCalculator.CalculateHitting(player, enemyGroupList);
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
            enemyGroupList.Add(new(enemy));
        }

        public static void RemoveEnemyAndDestroyDamageObject(EnemyMain enemy)
        {
            for(int i = 0; i < enemyGroupList.Count; i++)
            {
                if(enemyGroupList[i].Enemy != enemy)
                    continue;
                enemyGroupList[i].DestroyAllEnemyDamageObjects();
                enemyGroupList.RemoveAt(i);
                break;
            }
        }

        public static void CleanAllObjects()
        {
            if(player != null) 
                player.SetCleaned();
            for(int i = 0; i < enemyGroupList.Count; i++)
            {
                enemyGroupList[i].Enemy.SetCleaned();
                enemyGroupList[i].DestroyAllEnemyDamageObjects();
            }
        }

        public static void AddEnemyDamageObject(EnemyDamageObjectMain enemyDamageObject, EnemyMain enemy)
        {
            for(int i = 0; i < enemyGroupList.Count; i++)
            {
                if(enemyGroupList[i].Enemy != enemy)
                    continue;
                enemyGroupList[i].EnemyDamageObjectList.Add(enemyDamageObject);
                return;
            }
        }

        public static void RemoveEnemyDamageObject(EnemyDamageObjectMain enemyDamageObject)
        {
            for(int i = 0; i < enemyGroupList.Count; i++)
            {
                if(!enemyGroupList[i].EnemyDamageObjectList.Contains(enemyDamageObject))
                    continue;
                enemyGroupList[i].EnemyDamageObjectList.Remove(enemyDamageObject);
                return;
            }
        }

        public static void SetAllObjectsReady()
        {
            if(player == null)
                return;
            player.SetReady();
            for(int i = 0; i < enemyGroupList.Count; i++)
                enemyGroupList[i].Enemy.SetReady();
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
            return enemyGroupList.Count > 0;
        }
    }

    public class EnemyGroup
    {
        private readonly EnemyMain enemy;
        public EnemyMain Enemy => enemy;
        private readonly List<EnemyDamageObjectMain> enemyDamageObjectList = new();
        public List<EnemyDamageObjectMain> EnemyDamageObjectList => enemyDamageObjectList;

        public EnemyGroup(EnemyMain enemy)
        {
            this.enemy = enemy;
        }

        public void DestroyAllEnemyDamageObjects()
        {
            int count = enemyDamageObjectList.Count;
            for(int i = 0; i < count; i++)
                enemyDamageObjectList[0].DestroyObject();
        }
    }
}