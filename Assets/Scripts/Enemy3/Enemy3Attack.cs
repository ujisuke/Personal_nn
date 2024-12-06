using UnityEngine;
using Assets.Scripts.Objects;
using System.Collections.Generic;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3Attack : MonoBehaviour
    {
        private Enemy3Parameter enemy3Parameter;
        [SerializeField] private GameObject _damageObjectPrefab;
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
    
        public void Initialize(Enemy3Parameter enemy3Parameter)
        {
            this.enemy3Parameter = enemy3Parameter;
            objectMove = GetComponent<ObjectMove>();
        }

        private async void OnEnable()
        {
            isAttacking = true;
            objectMove.Stop();
            List<List<Vector3>> objectRePos3ListList = ObjectMove.GetAllRePos3ReachableWithoutJumping(transform.position);
            IEnemyMain enemy = GetComponent<IEnemyMain>();
            for(int i = 0; i < objectRePos3ListList.Count; i++)
            {
                List<Vector3> objectRePos3List = objectRePos3ListList[i];
                for(int j = 0; j < objectRePos3List.Count; j++)
                    ObjectCreator.InstantiateDamageObject(_damageObjectPrefab, objectRePos3List[j], enemy3Parameter.DamageObjectParameter, enemy);
                await UniTask.Delay(TimeSpan.FromSeconds(enemy3Parameter.WaveMoveTime));
            }
            isAttacking = false;
        }

        public void StopAttack()
        {
            StopAllCoroutines();
        }
    }
}
