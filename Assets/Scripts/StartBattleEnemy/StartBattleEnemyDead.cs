using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.StartBattleEnemy
{
    public class StartBattleEnemyDead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Dead();
            GetComponent<SetShadow>().DestroyShadow();
            GetComponent<StartBattleEnemyMain>().DestroyDeadObject();
        } 
    }
}