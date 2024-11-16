using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ObjectFacade : MonoBehaviour
    {
        private static IObject player;
        private static List<IObject> enemyList = new();
        
        public static void SetPlayer(IObject newPlayer)
        {
            player = newPlayer;
        }

        public static void AddEnemy(IObject newEnemy)
        {
            enemyList.Add(newEnemy);
        }

        public static void RemoveEnemy(IObject obj)
        {
            enemyList.Remove(obj);
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

        //当たり判定の計算専用クラスで行う予定
        private void FixedUpdate()
        {
            if(player == null) return;
            (Vector3 playerMinImPos3, Vector3 playerMaxImPos3) = player.GetImPos3s();
            foreach(IObject enemy in enemyList)
            {
                if(!enemy.IsDamaging()) continue;
                (Vector3 enemyMinImPos3, Vector3 enemyMaxImPos3) = enemy.GetImPos3s();
                if(playerMaxImPos3.x < enemyMinImPos3.x || playerMinImPos3.x > enemyMaxImPos3.x) continue;
                if(playerMaxImPos3.y < enemyMinImPos3.y || playerMinImPos3.y > enemyMaxImPos3.y) continue;
                if(playerMaxImPos3.z < enemyMinImPos3.z || playerMinImPos3.z > enemyMaxImPos3.z) continue;
                player.DamagedBy(enemy);
            }
        }
    }
}