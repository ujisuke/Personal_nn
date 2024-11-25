using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator animator;
        private PlayerParameter _playerParameter;

        public void Initialize(PlayerParameter playerParameter)
        {
            _playerParameter = playerParameter;
            animator = GetComponent<Animator>();
            animator.SetFloat("DeadSpeed", 1f / _playerParameter.DeadTime);
            animator.SetFloat("ReadySpeed", 1f / _playerParameter.ReadyTime);
        }

        public void SetLookingDirection((bool PlusImX, bool MinusImX, bool PlusImY, bool MinusImY) isLooking)
        {
            (bool PlusImX, bool MinusImX, bool PlusImY, bool MinusImY) newIsLooking = isLooking;
            if (isLooking.PlusImX && isLooking.MinusImX)
            {
                newIsLooking.PlusImX = false;
                newIsLooking.MinusImX = false;
            }
            if (isLooking.PlusImY && isLooking.MinusImY)
            {
                newIsLooking.PlusImY = false;
                newIsLooking.MinusImY = false;
            }
            animator.SetBool("IsLookingPlusImX", newIsLooking.PlusImX);
            animator.SetBool("IsLookingMinusImX", newIsLooking.MinusImX);
            animator.SetBool("IsLookingPlusImY", newIsLooking.PlusImY);
            animator.SetBool("IsLookingMinusImY", newIsLooking.MinusImY);
        }

        public void StartWalk()
        {
            animator.SetBool("IsWalking", true);
        }

        public void StopWalk()
        {
            animator.SetBool("IsWalking", false);
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