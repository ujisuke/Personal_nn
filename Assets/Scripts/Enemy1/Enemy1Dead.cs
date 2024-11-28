using Assets.Scripts.Objects;
using UnityEngine;
using Assets.ScriptableObjects;
using System.Collections;

namespace Assets.Scripts.Enemy1
{
    public class Enemy1Dead : MonoBehaviour
    {
        private Enemy1Parameter _enemy1Parameter;
        private Enemy1Main enemy1Main;
        
        public void Initialize(Enemy1Parameter enemy1Parameter)
        {
            _enemy1Parameter = enemy1Parameter;
            enemy1Main = GetComponent<Enemy1Main>();
        }

        private void OnEnable()
        {
            ObjectStorage.RemoveAndDestroyEnemyDamageObject(enemy1Main);
            StartCoroutine(WaitAndDestroy());
            GetComponent<ObjectMove>().Stop();
        }

        private IEnumerator WaitAndDestroy()
        {
            yield return new WaitForSeconds(_enemy1Parameter.DeadTime);
            enemy1Main.DestroyDeadObject();
        }        
    }
}