using Assets.Scripts.Objects;
using UnityEngine;
using Assets.ScriptableObjects;
using System.Collections;
using Cysharp.Threading.Tasks;
using System;
using Assets.Scripts.Battle;

namespace Assets.Scripts.ResetEnemy
{
    public class ResetEnemyDead : MonoBehaviour
    {
        private void OnEnable()
        {
            ResetEnemyMain resetEnemyMain = GetComponent<ResetEnemyMain>();
            ObjectStorage.RemoveAndDestroyEnemyDamageObject(resetEnemyMain);
            GetComponent<ObjectMove>().Stop();
            resetEnemyMain.DestroyDeadObject();
        } 
    }
}