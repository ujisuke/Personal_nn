using UnityEngine;
using Assets.Scripts.Objects;
using System.Collections;
using Unity.Mathematics;

namespace Assets.Scripts.Enemies
{
    public class Enemy1Move : MonoBehaviour
    {
        private ObjectMove objectMove;
        private Vector3 targetPos3 = new(-1, -1, -1);
        private static readonly float stopDistance = 0.4f;
        private static readonly float attackDistance = 1f;

        private void OnEnable()
        {
            objectMove = GetComponent<ObjectMove>();
            objectMove.Initialize(transform.position);
            targetPos3 = ObjectFacade.GetPlayerPos3();
            StartCoroutine(UpdateTargetPos3());
        }

        private IEnumerator UpdateTargetPos3()
        {
            while(!ObjectFacade.IsPlayerDead())
            {
                yield return new WaitForSeconds(0.2f);
                targetPos3 = ObjectFacade.GetPlayerPos3();
            }
        }
    
        private void FixedUpdate()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalclateImDirection3BetWeenTwoRePos3(transform.position, targetPos3);
            objectMove.HeadToD(moveDirectionIm3.x >= stopDistance || moveDirectionIm3.y >= stopDistance);
            objectMove.HeadToA(moveDirectionIm3.x < -stopDistance || moveDirectionIm3.y < -stopDistance);
            objectMove.HeadToW(moveDirectionIm3.x < -stopDistance || moveDirectionIm3.y >= stopDistance);
            objectMove.HeadToS(moveDirectionIm3.x >= stopDistance || moveDirectionIm3.y < -stopDistance);
            objectMove.TryToJump(objectMove.IsDestinationTileZReachableWithJumping(transform.position)
            || IsNearPlayer());
        }

        private bool IsNearPlayer()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalclateImDirection3BetWeenTwoRePos3(transform.position, targetPos3);
            return math.abs(moveDirectionIm3.x) <= attackDistance && math.abs(moveDirectionIm3.y) <= attackDistance;
        }

        public bool CanAttack()
        {
            return IsNearPlayer() && objectMove.IsJumping;
        }
    }
}
