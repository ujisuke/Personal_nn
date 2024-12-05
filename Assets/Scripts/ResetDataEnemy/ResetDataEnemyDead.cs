using Assets.Scripts.Objects;
using UnityEngine;
using Assets.ScriptableObjects;
using System.Collections;
using Cysharp.Threading.Tasks;
using System;
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
            BattleFacade.ResetData();
            resetDataEnemyMain.DestroyDeadObject();
        } 
    }
}