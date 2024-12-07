using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.BackToLobbyEnemy
{
    public class BackToLobbyEnemyDead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Dead();
            GetComponent<SetShadow>().DestroyShadow();
            GetComponent<BackToLobbyEnemyMain>().DestroyDeadObject();
        } 
    }
}