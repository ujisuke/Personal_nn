using Assets.Scripts.Objects;
using UnityEngine;
using Assets.ScriptableObjects;
using System.Collections;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.ExitEnemy
{
    public class ExitEnemyDead : MonoBehaviour
    {
        private void OnEnable()
        {
            ExitEnemyMain exitEnemyMain = GetComponent<ExitEnemyMain>();
            ObjectStorage.RemoveAndDestroyEnemyDamageObject(exitEnemyMain);
            GetComponent<ObjectMove>().Stop();
            exitEnemyMain.DestroyDeadObject();
        } 
    }
}