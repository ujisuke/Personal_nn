using UnityEngine;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Collections.Generic;

namespace Assets.Scripts.Enemy4
{
    public class Enemy4Attack1 : MonoBehaviour
    {
        private Enemy4Parameter _enemy4Parameter;
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        private CancellationTokenSource cancellationTokenSource = null;
    
        public void Initialize(Enemy4Parameter enemy4Parameter)
        {
            _enemy4Parameter = enemy4Parameter;
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
            List<Vector3> objectRePos3List = ObjectMove.GetAllRePos3Cross(transform.position);
            EnemyMain enemy = GetComponent<EnemyMain>();
            for(int i = 0; i < objectRePos3List.Count; i++)
                ObjectCreator.InstantiateEnemyDamageObject(objectRePos3List[i], _enemy4Parameter.EnemyDamageObjectParameter, enemy);
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy4Parameter.AttackCoolDownTime), cancellationToken: cancellationTokenSource.Token);
        }

        public void StopAttack()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
