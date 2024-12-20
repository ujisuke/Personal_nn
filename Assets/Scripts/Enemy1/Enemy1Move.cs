using UnityEngine;
using Assets.Scripts.Objects;
using Unity.Mathematics;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;


namespace Assets.Scripts.Enemy1
{
    public class Enemy1Move : MonoBehaviour
    {
        private Enemy1Parameter _enemy1Parameter;
        private ObjectMove objectMove;
        private Vector3 targetRePos3 = new(-1, -1, -1);
        private CancellationTokenSource cancellationTokenSource = null;

        public void Initialize(Enemy1Parameter enemy1Parameter)
        {
            _enemy1Parameter = enemy1Parameter;
            objectMove = GetComponent<ObjectMove>();
        }

        private async void OnEnable()
        {
            cancellationTokenSource = new();
            await Move().SuppressCancellationThrow();
        }

        private async UniTask Move()
        {
            targetRePos3 = ObjectStorage.GetPlayerRePos3();
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: cancellationTokenSource.Token);   
                targetRePos3 = ObjectStorage.GetPlayerRePos3();
            }
        }

        public void StopMove()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }

        private void FixedUpdate()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalculateImDirection3BetWeenTwoRePos3(transform.position, targetRePos3);
            objectMove.HeadToPlusImX(moveDirectionIm3.x >= _enemy1Parameter.StopMoveImDistanceFromPlayer);
            objectMove.HeadToMinusImX(moveDirectionIm3.x < -_enemy1Parameter.StopMoveImDistanceFromPlayer);
            objectMove.HeadToPlusImY(moveDirectionIm3.y >= _enemy1Parameter.StopMoveImDistanceFromPlayer);
            objectMove.HeadToMinusImY(moveDirectionIm3.y < -_enemy1Parameter.StopMoveImDistanceFromPlayer);
            objectMove.TryToJump(objectMove.IsDestinationTileZReachableWithJumping(transform.position));
        }

        private bool IsNearPlayer()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalculateImDirection3BetWeenTwoRePos3(transform.position, targetRePos3);
            return math.abs(moveDirectionIm3.x) <= _enemy1Parameter.AttackImDistanceFromPlayer && math.abs(moveDirectionIm3.y) <= _enemy1Parameter.AttackImDistanceFromPlayer;
        }

        public bool CanAttack()
        {
            return IsNearPlayer() && !objectMove.IsJumping;
        }
    }
}
