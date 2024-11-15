using UnityEngine;
using System.Collections;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy2Attack : MonoBehaviour
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
            yield return new WaitForSeconds(2f);
            isAttacking = false;
        }
    }
}
