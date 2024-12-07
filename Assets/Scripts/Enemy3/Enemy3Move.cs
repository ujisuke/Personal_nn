using UnityEngine;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;
using Unity.Mathematics;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3Move : MonoBehaviour
    {
        private Enemy3Parameter enemy3Parameter;
        private ObjectMove objectMove;
        private bool canAttack = false;
        public bool CanAttack => canAttack;
        CancellationTokenSource cancellationTokenSource = null;

        public void Initialize(Enemy3Parameter enemy3Parameter)
        {
            this.enemy3Parameter = enemy3Parameter;
            objectMove = GetComponent<ObjectMove>();
        }

        private async void OnEnable()
        {
            canAttack = false;
            objectMove.Stop();
            cancellationTokenSource = new();
            await UniTask.Delay(TimeSpan.FromSeconds(enemy3Parameter.AttackCoolDownTime), cancellationToken: cancellationTokenSource.Token).SuppressCancellationThrow();
            canAttack = true;
        }

        public (bool isLookingPlusImX, bool isLookingMinusImX, bool isLookingPlusImY, bool isLookingMinusImY) GetLookingDirection()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalculateImDirection3BetWeenTwoRePos3(transform.position, ObjectStorage.GetPlayerRePos3());
            if(math.abs(moveDirectionIm3.x) > math.abs(moveDirectionIm3.y))
            {
                if(moveDirectionIm3.x > 0)
                    return (true, false, false, false);
                else
                    return (false, true, false, false);
            }
            else
            {
                if(moveDirectionIm3.y > 0)
                    return (false, false, true, false);
                else
                    return (false, false, false, true);
            }
        }

        public void StopMove()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
