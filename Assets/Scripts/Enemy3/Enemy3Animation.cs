using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3Animation : MonoBehaviour
    {
        private Animator animator;
        private Enemy3Parameter _enemy3Parameter;

        public void Initialize(Enemy3Parameter enemy3Parameter)
        {
            _enemy3Parameter = enemy3Parameter;
            animator = GetComponent<Animator>();
            animator.SetFloat("DeadSpeed", 1f / _enemy3Parameter.DeadTime);
            animator.SetFloat("ReadySpeed", 1f / _enemy3Parameter.ReadyTime);
        }

        public void SetLookingDirection((bool PlusImX, bool MinusImX, bool PlusImY, bool MinusImY) isLooking)
        {
            animator.SetBool("IsLookingPlusImX", isLooking.PlusImX);
            animator.SetBool("IsLookingMinusImX", isLooking.MinusImX);
            animator.SetBool("IsLookingPlusImY", isLooking.PlusImY);
            animator.SetBool("IsLookingMinusImY", isLooking.MinusImY);
        }

        public void StartMove()
        {
            animator.SetBool("IsStanding", true);
        }

        public void StopMove()
        {
            animator.SetBool("IsStanding", false);
        }

        public void StartAttack()
        {
            animator.SetBool("IsAttacking", true);
        }

        public void StopAttack()
        {
            animator.SetBool("IsAttacking", false);
        }

        public void StartDead()
        {
            animator.SetBool("IsDead", true);
        }
    }
}