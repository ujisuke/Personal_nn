using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Enemy4
{
    public class Enemy4Animation : MonoBehaviour
    {
        private Animator animator;
        private Enemy4Parameter _enemy4Parameter;

        public void Initialize(Enemy4Parameter enemy4Parameter)
        {
            _enemy4Parameter = enemy4Parameter;
            animator = GetComponent<Animator>();
            animator.SetFloat("DeadSpeed", 1f / _enemy4Parameter.DeadTime);
            animator.SetFloat("ReadySpeed", 1f / _enemy4Parameter.ReadyTime);
        }

        public void SetLookingDirection((bool PlusImX, bool MinusImX, bool PlusImY, bool MinusImY) isLooking)
        {
            animator.SetBool("IsLookingPlusImX", isLooking.PlusImX);
            animator.SetBool("IsLookingMinusImX", isLooking.MinusImX);
            animator.SetBool("IsLookingPlusImY", isLooking.PlusImY);
            animator.SetBool("IsLookingMinusImY", isLooking.MinusImY);
        }

        public void StartWalk()
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