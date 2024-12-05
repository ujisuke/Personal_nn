using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.SetGameEnemy
{
    public class SetGameEnemyAnimation : MonoBehaviour
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

        public void StartWalk()
        {
            animator.SetBool("IsWalking", true);
        }

        public void StopWalk()
        {
            animator.SetBool("IsWalking", false);
        }

        public void StartDead()
        {
            animator.SetBool("IsDead", true);
        }
    }
}
