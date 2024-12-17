using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Player;
using Assets.Scripts.EnemyDamageObject;

namespace Assets.Scripts.Objects
{
    public class ObjectHittingCalculator : MonoBehaviour
    {
        public static void CalculateHitting(PlayerMain player, List<EnemyGroup> enemyGroupList)
        {
            CalculateHittingPlayerToEnemy(player, enemyGroupList);
            CalculateHittingEnemyDamageObjectToPlayer(player, enemyGroupList);
        }

        private static void CalculateHittingPlayerToEnemy(PlayerMain player, List<EnemyGroup> enemyGroupList)
        {
            if(player == null || !player.IsDamaging() || enemyGroupList.Count == 0)
                return;
            (Vector3 playerMinImPos3, Vector3 playerMaxImPos3) = player.GetImPos3s();
            for(int i = 0; i < enemyGroupList.Count; i++)
            {
                (Vector3 enemyMinImPos3, Vector3 enemyMaxImPos3) = enemyGroupList[i].Enemy.GetImPos3s();
                if(playerMaxImPos3.x < enemyMinImPos3.x || playerMinImPos3.x > enemyMaxImPos3.x
                || playerMaxImPos3.y < enemyMinImPos3.y || playerMinImPos3.y > enemyMaxImPos3.y
                || playerMaxImPos3.z < enemyMinImPos3.z || playerMinImPos3.z > enemyMaxImPos3.z)
                    continue;
                player.DamageTo(enemyGroupList[i].Enemy);
            }
        }

        private static void CalculateHittingEnemyDamageObjectToPlayer(PlayerMain player, List<EnemyGroup> enemyGroupList)
        {
            if(player == null || enemyGroupList.Count == 0)
                return;
            (Vector3 playerMinImPos3, Vector3 playerMaxImPos3) = player.GetImPos3s();
            for(int i = 0; i < enemyGroupList.Count; i++)
            {
                for(int j = 0; j < enemyGroupList[i].EnemyDamageObjectList.Count; j++)
                {
                    EnemyDamageObjectMain enemyDamageObject = enemyGroupList[i].EnemyDamageObjectList[j];
                    if(!enemyDamageObject.IsDamaging())
                        continue;
                    (Vector3 enemyDamageObjectMinImPos3, Vector3 enemyDamageObjectMaxImPos3) = enemyDamageObject.GetImPos3s();
                    if(playerMaxImPos3.x < enemyDamageObjectMinImPos3.x || playerMinImPos3.x > enemyDamageObjectMaxImPos3.x
                    || playerMaxImPos3.y < enemyDamageObjectMinImPos3.y || playerMinImPos3.y > enemyDamageObjectMaxImPos3.y
                    || playerMaxImPos3.z < enemyDamageObjectMinImPos3.z || playerMinImPos3.z > enemyDamageObjectMaxImPos3.z)
                        continue;
                    enemyDamageObject.DamageTo(player);
                }
            }
        }
    }
}

