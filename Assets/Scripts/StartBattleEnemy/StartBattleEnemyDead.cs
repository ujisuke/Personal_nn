using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.StartBattleEnemy
{
    public class StartBattleEnemyDead : MonoBehaviour
    {
        private void OnEnable()
        {
            StartBattleEnemyMain startBattleEnemyMain = GetComponent<StartBattleEnemyMain>();
            ObjectStorage.RemoveAndDestroyEnemyDamageObject(startBattleEnemyMain);
            GetComponent<ObjectMove>().Stop();
            startBattleEnemyMain.DestroyDeadObject();
        } 
    }
}