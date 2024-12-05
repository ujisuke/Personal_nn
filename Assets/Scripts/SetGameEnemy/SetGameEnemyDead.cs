using Assets.Scripts.Objects;
using UnityEngine;
using Assets.ScriptableObjects;
using System.Collections;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.SetGameEnemy
{
    public class SetGameEnemyDead : MonoBehaviour
    {
        private void OnEnable()
        {
            SetGameEnemyMain setGameEnemyMain = GetComponent<SetGameEnemyMain>();
            ObjectStorage.RemoveAndDestroyEnemyDamageObject(setGameEnemyMain);
            GetComponent<ObjectMove>().Stop();
            setGameEnemyMain.DestroyDeadObject();
        } 
    }
}