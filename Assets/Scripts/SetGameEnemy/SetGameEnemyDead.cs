using Assets.Scripts.Objects;
using UnityEngine;

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