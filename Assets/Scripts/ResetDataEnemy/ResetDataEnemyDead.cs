using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.ResetDataEnemy
{
    public class ResetDataEnemyDead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Dead();
            GetComponent<SetShadow>().DestroyShadow();
            GetComponent<ResetDataEnemyMain>().DestroyDeadObject();
        } 
    }
}