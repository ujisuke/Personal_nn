using UnityEngine;
using Assets.Scripts.Objects;
using Unity.Mathematics;

namespace Assets.Scripts.Enemies
{
    public class Enemy2Move : MonoBehaviour
    {
        private ObjectMove objectMove;
        private Vector3 targetRePos3 = new();
        private Vector3 initialtargetRePos3 = new(-1, -1, -1);
        private readonly float stopDistance = 0.2f;

        private void OnEnable()
        {
            objectMove = GetComponent<ObjectMove>();
            objectMove.Initialize(transform.position);
            targetRePos3 = initialtargetRePos3;
            targetRePos3 = objectMove.DrawRePos3AroundRePos3(transform.position);
        }
    
        private void FixedUpdate()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalclateImDirection3BetWeenTwoRePos3(transform.position, targetRePos3);
            objectMove.HeadToD(moveDirectionIm3.x >= stopDistance || moveDirectionIm3.y >= stopDistance);
            objectMove.HeadToA(moveDirectionIm3.x < -stopDistance || moveDirectionIm3.y < -stopDistance);
            objectMove.HeadToW(moveDirectionIm3.x < -stopDistance || moveDirectionIm3.y >= stopDistance);
            objectMove.HeadToS(moveDirectionIm3.x >= stopDistance || moveDirectionIm3.y < -stopDistance);

            objectMove.TryToJump(objectMove.IsDestinationTileZReachableWithJumping(transform.position));
        }

        public bool CanAttack()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalclateImDirection3BetWeenTwoRePos3(transform.position, targetRePos3);
            return math.abs(moveDirectionIm3.x) <= stopDistance && math.abs(moveDirectionIm3.y) <= stopDistance;
        }
    }
}
