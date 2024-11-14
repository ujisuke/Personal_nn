using UnityEngine;
using Assets.Scripts.Objects;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    public class Enemy1Move : MonoBehaviour
    {
        ObjectMove objectMove;
        private Vector3 targetPos3 = new();
        private Vector3 initialtargetPos3 = new(-1, -1, -1);
        private readonly float distanceBetweenPlayerAndEnemy = 0.4f;

        private void Start()
        {
            objectMove = GetComponent<ObjectMove>();
            targetPos3 = initialtargetPos3;
            StartCoroutine(UpdateTargetPos3());
        }

        private IEnumerator UpdateTargetPos3()
        {
            while(!ObjectFacade.IsPlayerDead())
            {
                yield return new WaitForSeconds(0.2f);
                targetPos3 = ObjectFacade.GetPlayerPos3();
                Vector3 moveDirection = targetPos3 - transform.position;
                objectMove.HeadToD(moveDirection.x >= distanceBetweenPlayerAndEnemy);
                objectMove.HeadToA(moveDirection.x < -distanceBetweenPlayerAndEnemy);
                objectMove.HeadToW(moveDirection.y >= distanceBetweenPlayerAndEnemy / 2f);
                objectMove.HeadToS(moveDirection.y < -distanceBetweenPlayerAndEnemy / 2f);
            }
        }
    
        private void FixedUpdate()
        {
            objectMove.TryToJump(objectMove.IsDestinationTileZMoreThanOrEqual(transform.position.z));
        }
    }
}
