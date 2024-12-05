using Assets.Scripts.Objects;
using UnityEngine;
using Assets.ScriptableObjects;
using System.Collections;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.BackToLobbyEnemy
{
    public class BackToLobbyEnemyDead : MonoBehaviour
    {
        private void OnEnable()
        {
            BackToLobbyEnemyMain exitEnemyMain = GetComponent<BackToLobbyEnemyMain>();
            ObjectStorage.RemoveAndDestroyEnemyDamageObject(exitEnemyMain);
            GetComponent<ObjectMove>().Stop();
            exitEnemyMain.DestroyDeadObject();
        } 
    }
}