using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.BackToLobbyEnemy
{
    public class BackToLobbyEnemyDead : MonoBehaviour
    {
        private void OnEnable()
        {
            BackToLobbyEnemyMain exitEnemyMain = GetComponent<BackToLobbyEnemyMain>();
            ObjectStorage.RemoveAndDestroyEnemyDamageObject(exitEnemyMain);
            GetComponent<ObjectMove>().Dead();
            GetComponent<SetShadow>().DestroyShadow();
            exitEnemyMain.DestroyDeadObject();
        } 
    }
}