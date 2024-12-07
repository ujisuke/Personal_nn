using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.SetGameEnemy
{
    public class SetGameEnemyDead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Dead();
            GetComponent<SetShadow>().DestroyShadow();
            GetComponent<SetGameEnemyMain>().DestroyDeadObject();
        } 
    }
}