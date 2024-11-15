using UnityEngine;
using Assets.Scripts.Objects;
using Unity.Mathematics;

namespace Assets.Scripts.Enemies
{
    public class Enemy2Move : MonoBehaviour
    {
        private ObjectMove objectMove;
        private Vector3 targetPos3 = new();
        private Vector3 initialtargetPos3 = new(-1, -1, -1);
        private readonly float stopDistance = 0.2f;

        private void OnEnable()
        {
            objectMove = GetComponent<ObjectMove>();
            objectMove.Initialize(transform.position);
            targetPos3 = initialtargetPos3;
            targetPos3 = objectMove.DrawRePos3AroundRePos3(transform.position);
        }
    
        private void FixedUpdate()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalclateImDirection3BetWeenTwoRePos3(transform.position, targetPos3);
            objectMove.HeadToD(moveDirectionIm3.x >= stopDistance);
            objectMove.HeadToA(moveDirectionIm3.x < -stopDistance);
            objectMove.HeadToW(moveDirectionIm3.y >= stopDistance / 2f);
            objectMove.HeadToS(moveDirectionIm3.y < -stopDistance / 2f);

            objectMove.TryToJump(objectMove.IsDestinationTileZReachableWithJumping(transform.position));
        }

        public bool CanAttack()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalclateImDirection3BetWeenTwoRePos3(transform.position, targetPos3);
            return math.abs(moveDirectionIm3.x) <= stopDistance && math.abs(moveDirectionIm3.y) <= stopDistance / 2f;
        }
    }
}
