using UnityEngine;
using Assets.Scripts.Objects;
using System.Collections.Generic;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3Attack : MonoBehaviour
    {
        private Enemy3Parameter enemy3Parameter;
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        private CancellationTokenSource cancellationTokenSource = null;
    
        public void Initialize(Enemy3Parameter enemy3Parameter)
        {
            this.enemy3Parameter = enemy3Parameter;
            objectMove = GetComponent<ObjectMove>();
        }

        private async void OnEnable()
        {
            cancellationTokenSource = new();
            isAttacking = true;
            objectMove.Stop();
            await Attack().SuppressCancellationThrow();
            isAttacking = false;
        }

        private async UniTask Attack()
        {
            List<List<Vector3>> objectRePos3ListList = ObjectMove.GetAllRePos3ReachableWithoutJumping(transform.position);
            IEnemyMain enemy = GetComponent<IEnemyMain>();
            for(int i = 0; i < objectRePos3ListList.Count; i++)
            {
                List<Vector3> objectRePos3List = objectRePos3ListList[i];
                for(int j = 0; j < objectRePos3List.Count; j++)
                    ObjectCreator.InstantiateEnemyDamageObject(objectRePos3List[j], enemy3Parameter.EnemyDamageObjectParameter, enemy);
                await UniTask.Delay(TimeSpan.FromSeconds(enemy3Parameter.WaveMoveTime), cancellationToken: cancellationTokenSource.Token);
            }
        }

        public void StopAttack()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
