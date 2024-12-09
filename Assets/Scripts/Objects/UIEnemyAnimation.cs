using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class UIEnemyAnimation : MonoBehaviour
    {
        private Animator animator;
        private ObjectParameter _objectParameter;

        public void Initialize(ObjectParameter objectParameter)
        {
            _objectParameter = objectParameter;
            animator = GetComponent<Animator>();
            animator.SetFloat("DeadSpeed" , 1f / _objectParameter.DeadTime);
            animator.SetFloat("ReadySpeed", 1f / _objectParameter.ReadyTime);
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

        public void StartDead()
        {
            animator.SetBool("IsDead", true);
        }
    }
}
