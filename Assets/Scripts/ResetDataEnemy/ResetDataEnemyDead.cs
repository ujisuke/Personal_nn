using Assets.Scripts.Objects;
using UnityEngine;
using Assets.Scripts.Battle;

namespace Assets.Scripts.ResetDataEnemy
{
    public class ResetDataEnemyDead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Dead();
            GetComponent<SetShadow>().DestroyShadow();
            BattleFacade.ResetData();
            GetComponent<ResetDataEnemyMain>().DestroyDeadObject();
        } 
    }
}