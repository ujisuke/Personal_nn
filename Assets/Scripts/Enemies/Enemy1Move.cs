using UnityEngine;
using Assets.Scripts.Objects;
using System.Collections;
using Unity.Mathematics;
using Assets.Scripts.Stage;
using Unity.VisualScripting;

namespace Assets.Scripts.Enemies
{
    public class Enemy1Move : MonoBehaviour
    {
        private ObjectMove objectMove;
        private Vector3 targetRePos3 = new(-1, -1, -1);
        private static readonly float stopDistance = 0.4f;
        private static readonly float attackDistance = 1f;

        private void OnEnable()
        {
            objectMove = GetComponent<ObjectMove>();
            objectMove.Initialize(transform.position);
            targetRePos3 = ObjectFacade.GetPlayerRePos3();
            StartCoroutine(UpdateTargetPos3());
        }

        private IEnumerator UpdateTargetPos3()
        {
            while(!ObjectFacade.IsPlayerDead())
            {
                yield return new WaitForSeconds(0.2f);
                targetRePos3 = ObjectFacade.GetPlayerRePos3();
            }
        }
    
        private void FixedUpdate()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalclateImDirection3BetWeenTwoRePos3(transform.position, targetRePos3);
            objectMove.HeadToD(moveDirectionIm3.x >= stopDistance || moveDirectionIm3.y >= stopDistance);
            objectMove.HeadToA(moveDirectionIm3.x < -stopDistance || moveDirectionIm3.y < -stopDistance);
            objectMove.HeadToW(moveDirectionIm3.x < -stopDistance || moveDirectionIm3.y >= stopDistance);
            objectMove.HeadToS(moveDirectionIm3.x >= stopDistance || moveDirectionIm3.y < -stopDistance);
            objectMove.TryToJump(objectMove.IsDestinationTileZReachableWithJumping(transform.position)
            || math.abs(moveDirectionIm3.x) <= attackDistance && math.abs(moveDirectionIm3.y) <= attackDistance);
        }

        private bool IsNearPlayer()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalclateImDirection3BetWeenTwoRePos3(transform.position, targetRePos3);
            return math.abs(moveDirectionIm3.x) <= stopDistance && math.abs(moveDirectionIm3.y) <= stopDistance;
        }

        public bool CanAttack()
        {
            return IsNearPlayer() && objectMove.IsFalling;
        }
    }
}
