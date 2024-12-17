using UnityEngine;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Assets.Scripts.Sounds;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2Attack : MonoBehaviour
    {
        private Enemy2Parameter _enemy2Parameter;
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        private CancellationTokenSource cancellationTokenSource = null;
        private AudioSource audioSource;
        
        public void Initialize(Enemy2Parameter enemy2Parameter)
        {
            _enemy2Parameter = enemy2Parameter;
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
            EnemyMain enemy = GetComponent<EnemyMain>();
            for(int i = 0; i < _enemy2Parameter.AttackCount; i++)
            {
                ObjectCreator.SingletonInstance.InstantiateEnemyDamageObject(ObjectMove.ConvertToTileRePos3FromImPos3(ObjectMove.ConvertToImPos3FromRePos3(ObjectStorage.GetPlayerRePos3()) + new Vector3(0f, 0f, _enemy2Parameter.SearchedTargetZ)), _enemy2Parameter.EnemyDamageObjectParameter, enemy);
                SEPlayer.SingletonInstance.PlayInstantiateEnemy2DamageObject(audioSource);
                await UniTask.Delay(TimeSpan.FromSeconds(_enemy2Parameter.AttackCoolDownTime), cancellationToken: cancellationTokenSource.Token);
            } 
        }

        public void StopAttack()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
