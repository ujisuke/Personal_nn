using UnityEngine;
using System.Collections;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        private bool isDamaging = false;
        public bool IsDamaging => isDamaging;

        private void OnEnable()
        {
            objectMove = GetComponent<ObjectMove>();
            isAttacking = true;
            isDamaging = false;
            StartCoroutine(Attack());
        }

        private void FixedUpdate()
        {
            objectMove.HeadToPlusX(false);
            objectMove.HeadToMinusX(false);
            objectMove.HeadToPlusY(false);
            objectMove.HeadToMinusY(false);
            objectMove.TryToJump(false);
        }

        private IEnumerator Attack()
        {
            objectMove.ResetPrevZ();
            for(int i = 0; i < 10; i++)
            {
                if(!objectMove.IsJumping)
                {
                    isAttacking = false;
                    yield break;
                }
                yield return new WaitForSeconds(0.01f);
            }
            isDamaging = true;
            while(objectMove.IsJumping)
                yield return null;
            yield return new WaitForSeconds(0.1f);
            isDamaging = false;
            yield return new WaitForSeconds(0.2f);
            isAttacking = false;
        }
    }
}
