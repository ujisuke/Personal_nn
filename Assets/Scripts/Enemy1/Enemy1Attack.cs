using UnityEngine;
using Assets.Scripts.Objects;
using System.Collections.Generic;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Assets.Scripts.Sounds;

namespace Assets.Scripts.Enemy1
{
    public class Enemy1Attack : MonoBehaviour
    {
        private ObjectMove objectMove;
        private Enemy1Parameter _enemy1Parameter;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        private CancellationTokenSource cancellationTokenSource = null;

        public void Initialize(Enemy1Parameter enemy1Parameter)
        {
            objectMove = GetComponent<ObjectMove>();
            _enemy1Parameter = enemy1Parameter;
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
            List<Vector3> damageObjectRePos3List = ObjectMove.DrawSomeRePos3AtRandom(_enemy1Parameter.AttackPanelCount, ObjectMove.ConvertToTileIndexFromRePos3(transform.position),
            _enemy1Parameter.AttackPanelMinImRadius, _enemy1Parameter.AttackPanelMaxImRadius);
            EnemyMain enemy = GetComponent<EnemyMain>();
            for(int i = 0; i < damageObjectRePos3List.Count; i++)
                ObjectCreator.InstantiateEnemyDamageObject(damageObjectRePos3List[i], _enemy1Parameter.EnemyDamageObjectParameter, enemy);
            PlaySE.SingletonInstance.PlayInstantiateEnemy1DamageObject();
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy1Parameter.AttackCoolDownTime), cancellationToken: cancellationTokenSource.Token);
        }

        public void StopAttack()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
