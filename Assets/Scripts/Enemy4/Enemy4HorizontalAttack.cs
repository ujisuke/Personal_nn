using UnityEngine;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Collections.Generic;
using Assets.Scripts.Sounds;

namespace Assets.Scripts.Enemy4
{
    public class Enemy4HorizontalAttack : MonoBehaviour
    {
        private Enemy4Parameter _enemy4Parameter;
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        private CancellationTokenSource cancellationTokenSource = null;
        AudioSource audioSource;

        public void Initialize(Enemy4Parameter enemy4Parameter)
        {
            _enemy4Parameter = enemy4Parameter;
            objectMove = GetComponent<ObjectMove>();
            audioSource = GetComponent<AudioSource>();
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
            List<Vector3> objectRePos3List = ObjectMove.GetAllHorizontalRePos3(transform.position);
            EnemyMain enemy = GetComponent<EnemyMain>();
            for(int i = 0; i < objectRePos3List.Count; i++)
                ObjectCreator.SingletonInstance.InstantiateEnemyDamageObject(objectRePos3List[i], _enemy4Parameter.EnemyDamageObjectParameter, enemy);
            SEPlayer.SingletonInstance.PlayInstantiateEnemy4DamageObject(audioSource);
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy4Parameter.AttackCoolDownTime), cancellationToken: cancellationTokenSource.Token);
        }

        public void StopAttack()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
