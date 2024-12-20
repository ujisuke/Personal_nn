using Assets.Scripts.Objects;
using UnityEngine;
using Assets.Scripts.Stage;
using Assets.ScriptableObjects;
using Assets.Scripts.Player;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Assets.Scripts.EnemyDamageObject
{
    public class EnemyDamageObjectMain : MonoBehaviour
    {
        private EnemyDamageObjectParameter _enemyDamageObjectParameter;
        private bool isDamaging = false;
        private CancellationTokenSource cancellationTokenSource = null;

        public async UniTask Initialize(EnemyDamageObjectParameter _enemyDamageObjectParameter, EnemyMain enemy)
        {
            cancellationTokenSource = new();
            this._enemyDamageObjectParameter = _enemyDamageObjectParameter;
            ObjectStorage.AddEnemyDamageObject(this, enemy);
            GetComponent<EnemyDamageObjectAnimation>().Initialize(_enemyDamageObjectParameter);
            await Damage().SuppressCancellationThrow();
        }

        private async UniTask Damage()
        {
            isDamaging = false;
            await UniTask.Delay(TimeSpan.FromSeconds(_enemyDamageObjectParameter.ReadyTime), cancellationToken: cancellationTokenSource.Token);
            isDamaging = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_enemyDamageObjectParameter.DamagingTime), cancellationToken: cancellationTokenSource.Token);
            DestroyObject();
        }

        public bool IsDamaging()
        {
            return isDamaging;
        }

        public void DamageTo(PlayerMain player)
        {
            player.TakeDamage(_enemyDamageObjectParameter.AttackPower);
        }

        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x * 0.3f, 0f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x * 0.3f, transform.localScale.y * StageCreator._TileHeight, transform.localScale.y);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }

        public void DestroyObject()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            ObjectStorage.RemoveEnemyDamageObject(this);
            Destroy(gameObject);
        }
    }
}
