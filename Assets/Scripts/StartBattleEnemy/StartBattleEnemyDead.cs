using Assets.Scripts.Objects;
using UnityEngine;
using Assets.ScriptableObjects;
using System.Collections;
using Cysharp.Threading.Tasks;
using System;

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