using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator animator;

        public void Initialize(PlayerParameter playerParameter)
        {
            animator = GetComponent<Animator>();
            animator.SetFloat("DeadSpeed", 1f / playerParameter.DeadTime);
            animator.SetFloat("ReadySpeed", 1f / playerParameter.ReadyTime);
            animator.SetFloat("AttackSpeed", 1f / playerParameter.AttackingTime);
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

        public void StartMove()
        {
            animator.SetBool("IsWalking", true);
        }

        public void StopMove()
        {
            animator.SetBool("IsWalking", false);
        }

        public void StartAttack()
        {
            animator.SetTrigger("Attack");
        }

        public void StartDead()
        {
            animator.SetBool("IsDead", true);
        }
    }
}