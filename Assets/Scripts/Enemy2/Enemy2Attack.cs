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
        private Enemy2Parameter enemy2Parameter;
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        private CancellationTokenSource cancellationTokenSource = null;
        
        public void Initialize(Enemy2Parameter enemy2Parameter)
        {
            this.enemy2Parameter = enemy2Parameter;
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
            EnemyMain enemy = GetComponent<EnemyMain>();
            for(int i = 0; i < enemy2Parameter.AttackCount; i++)
            {
                ObjectCreator.InstantiateEnemyDamageObject(ObjectMove.ConvertToTileRePos3FromImPos3(ObjectMove.ConvertToImPos3FromRePos3(ObjectStorage.GetPlayerRePos3()) + new Vector3(0f, 0f, enemy2Parameter.SearchedTargetZ)), enemy2Parameter.EnemyDamageObjectParameter, enemy);
                PlaySE._SingletonInstance.PlayInstantiateEnemy2DamageObject();
                await UniTask.Delay(TimeSpan.FromSeconds(enemy2Parameter.AttackCoolDownTime), cancellationToken: cancellationTokenSource.Token);
            } 
        }

        public void StopAttack()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
