using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ObjectHittingCalculator : MonoBehaviour
    {
        public static void CalculateHitting(IObject player, List<IObject> enemyList)
        {
            if(player == null || enemyList.Count == 0) return;
            (Vector3 playerMinImPos3, Vector3 playerMaxImPos3) = player.GetImPos3s();
            foreach(IObject enemy in enemyList)
            {
                (Vector3 enemyMinImPos3, Vector3 enemyMaxImPos3) = enemy.GetImPos3s();
                if(playerMaxImPos3.x < enemyMinImPos3.x || playerMinImPos3.x > enemyMaxImPos3.x
                || playerMaxImPos3.y < enemyMinImPos3.y || playerMinImPos3.y > enemyMaxImPos3.y
                || playerMaxImPos3.z < enemyMinImPos3.z || playerMinImPos3.z > enemyMaxImPos3.z) continue;
                if(player.IsDamaging()) player.DamageTo(enemy);
                if(enemy.IsDamaging()) enemy.DamageTo(player);
            }
        }
    }
}

