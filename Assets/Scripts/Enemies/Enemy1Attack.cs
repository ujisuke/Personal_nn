using UnityEngine;
using System.Collections;
using Assets.Scripts.Objects;
using Unity.VisualScripting;

namespace Assets.Scripts.Enemies
{
    public class Enemy1Attack : MonoBehaviour
    {
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        private bool isDamaging = false;
        public bool IsDamaging => isDamaging;

        private void OnEnable()
        {
            isAttacking = true;
            objectMove = GetComponent<ObjectMove>();
            StartCoroutine(Attack());
        }

        private void FixedUpdate()
        {
            if(!objectMove.IsFalling) return;
            objectMove.HeadToD(false);
            objectMove.HeadToA(false);
            objectMove.HeadToW(false);
            objectMove.HeadToS(false);
            objectMove.TryToJump(false);
        }

        private IEnumerator Attack()
        {
            for(int i = 0; i < 10; i++)
            {
                if(!objectMove.IsJumping)
                {
                    isAttacking = false;
                    yield break;
                }
                yield return new WaitForSeconds(0.03f);
            }
            while(objectMove.IsJumping)
            {
                isDamaging = objectMove.IsFalling;
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            isDamaging = false;
            yield return new WaitForSeconds(0.4f);
            isAttacking = false;
        }
    }
}
