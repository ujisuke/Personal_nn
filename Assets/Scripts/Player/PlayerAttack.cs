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
            isDamaging = true;
            StartCoroutine(Attack());
        }

        private void FixedUpdate()
        {
            objectMove.HeadToMinusX(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W));
            objectMove.HeadToPlusY(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D));
            objectMove.HeadToMinusY(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A));
            objectMove.HeadToPlusX(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S));
            objectMove.TryToJump(Input.GetKey(KeyCode.Space));
        }

        private IEnumerator Attack()
        {
            yield return new WaitForSeconds(0.1f);
            isDamaging = false;
            isAttacking = false;
        }
    }
}
