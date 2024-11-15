using UnityEngine;
using System.Collections;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy1Attack : MonoBehaviour
    {
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;

        private void OnEnable()
        {
            isAttacking = true;
            objectMove = GetComponent<ObjectMove>();
            StartCoroutine(Attack());
        }

        private void FixedUpdate()
        {
            objectMove.HeadToD(false);
            objectMove.HeadToA(false);
            objectMove.HeadToW(false);
            objectMove.HeadToS(false);
            objectMove.TryToJump(false);
        }

        private IEnumerator Attack()
        {
            while(objectMove.IsJumping)
                yield return null;
            yield return new WaitForSeconds(0.5f);
            isAttacking = false;
        }
    }
}
