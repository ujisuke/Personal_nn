using Assets.Scripts.Objects;
using UnityEngine;
using Assets.Scripts.Battle;

namespace Assets.Scripts.ResetDataEnemy
{
    public class ResetDataEnemyDead : MonoBehaviour
    {
        private void OnEnable()
        {
            ResetDataEnemyMain resetDataEnemyMain = GetComponent<ResetDataEnemyMain>();
            ObjectStorage.RemoveAndDestroyEnemyDamageObject(resetDataEnemyMain);
            GetComponent<ObjectMove>().Stop();
            GetComponent<SetShadow>().DestroyShadow();
            BattleFacade.ResetData();
            resetDataEnemyMain.DestroyDeadObject();
        } 
    }
}