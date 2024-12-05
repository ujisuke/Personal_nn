using Assets.Scripts.Objects;
using UnityEngine;
using Assets.ScriptableObjects;
using System.Collections;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.SettingEnemy
{
    public class SettingEnemyDead : MonoBehaviour
    {
        private void OnEnable()
        {
            SettingEnemyMain settingEnemyMain = GetComponent<SettingEnemyMain>();
            ObjectStorage.RemoveAndDestroyEnemyDamageObject(settingEnemyMain);
            GetComponent<ObjectMove>().Stop();
            settingEnemyMain.DestroyDeadObject();
        } 
    }
}