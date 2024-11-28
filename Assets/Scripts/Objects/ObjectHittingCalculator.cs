using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Player;
using Assets.Scripts.EnemyDamageObject;

namespace Assets.Scripts.Objects
{
    public class ObjectHittingCalculator : MonoBehaviour
    {
        public static void CalculateHitting(PlayerMain player, List<IEnemyMain> enemyMainList, List<(EnemyDamageObjectMain enemyDamageObject, IEnemyMain enemy)> enemyDamageObjectMainList)
        {
            CalculateHittingPlayerToEnemy(player, enemyMainList);
            CalculateHittingEnemyDamageObjectToPlayer(player, enemyDamageObjectMainList);
        }

        private static void CalculateHittingPlayerToEnemy(PlayerMain player, List<IEnemyMain> enemyMainList)
        {
            if(player == null || !player.IsDamaging() || enemyMainList.Count == 0)
                return;
            (Vector3 playerMinImPos3, Vector3 playerMaxImPos3) = player.GetImPos3s();
            foreach(IEnemyMain enemy in enemyMainList)
            {
                (Vector3 enemyMinImPos3, Vector3 enemyMaxImPos3) = enemy.GetImPos3s();
                if(playerMaxImPos3.x < enemyMinImPos3.x || playerMinImPos3.x > enemyMaxImPos3.x
                || playerMaxImPos3.y < enemyMinImPos3.y || playerMinImPos3.y > enemyMaxImPos3.y
                || playerMaxImPos3.z < enemyMinImPos3.z || playerMinImPos3.z > enemyMaxImPos3.z)
                    continue;
                player.DamageTo(enemy);
            }
        }

        private static void CalculateHittingEnemyDamageObjectToPlayer(PlayerMain player, List<(EnemyDamageObjectMain enemyDamageObject, IEnemyMain enemy)> enemyDamageObjectMainList)
        {
            if(player == null || enemyDamageObjectMainList.Count == 0)
                return;
            (Vector3 playerMinImPos3, Vector3 playerMaxImPos3) = player.GetImPos3s();
            for(int i = 0; i < enemyDamageObjectMainList.Count; i++)
            {
                EnemyDamageObjectMain enemyDamageObject = enemyDamageObjectMainList[i].enemyDamageObject;
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

