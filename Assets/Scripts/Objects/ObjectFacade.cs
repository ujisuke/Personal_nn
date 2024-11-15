using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ObjectFacade : MonoBehaviour
    {
        private static GameObject player;
        
        public static void SetPlayer(GameObject newPlayer)
        {
            player = newPlayer;
        }

        public static Vector3 GetPlayerPos3()
        {
            if(player == null) return Vector3.zero;
            return player.transform.position;
        }

        public static bool IsPlayerDead()
        {
            return false;
        }
    }
}