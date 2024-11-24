using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
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
            _animator.SetBool("IsLookingPlusImX", newIsLooking.PlusImX);
            _animator.SetBool("IsLookingMinusImX", newIsLooking.MinusImX);
            _animator.SetBool("IsLookingPlusImY", newIsLooking.PlusImY);
            _animator.SetBool("IsLookingMinusImY", newIsLooking.MinusImY);
        }

        public void StartWalk()
        {
            _animator.SetBool("IsWalking", true);
        }

        public void StopWalk()
        {
            _animator.SetBool("IsWalking", false);
        }

        public void StartAttack()
        {
            _animator.SetBool("IsAttacking", true);
        }

        public void StopAttack()
        {
            _animator.SetBool("IsAttacking", false);
        }
    }
}