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

        public void SetLookingDirection((bool plusImX, bool minusImX, bool plusImY, bool minusImY) isLooking)
        {
            animator.SetBool("IsLookingPlusImX", isLooking.plusImX);
            animator.SetBool("IsLookingMinusImX", isLooking.minusImX);
            animator.SetBool("IsLookingPlusImY", isLooking.plusImY);
            animator.SetBool("IsLookingMinusImY", isLooking.minusImY);
        }

        public void StartMove()
        {
            animator.SetBool("IsMoving", true);
        }

        public void StopMove()
        {
            animator.SetBool("IsMoving", false);
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