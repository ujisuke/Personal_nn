using UnityEngine;
using Assets.Scripts.Objects;
using Unity.Mathematics;
using Assets.ScriptableObjects;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Enemy4
{
    public class Enemy4Move : MonoBehaviour
    {
        private Enemy4Parameter _enemy4Parameter;
        private ObjectMove objectMove;
        private bool canAttack = false;
        public bool CanAttack => canAttack;
        CancellationTokenSource cancellationTokenSource = null;

        public void Initialize(Enemy4Parameter enemy4Parameter)
        {
            _enemy4Parameter = enemy4Parameter;
            objectMove = GetComponent<ObjectMove>();
        }

        private async void OnEnable()
        {
            canAttack = false;
            objectMove.Stop();
            cancellationTokenSource = new();
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy4Parameter.AttackCoolDownTime), cancellationToken: cancellationTokenSource.Token).SuppressCancellationThrow();
            canAttack = true;
        }

        public void StopMove()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
