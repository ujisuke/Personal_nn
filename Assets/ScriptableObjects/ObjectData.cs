using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ObjectData")]
    public class ObjectData : ScriptableObject
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpTime;

        public float MoveSpeed => _moveSpeed;
        public float JumpHeight => _jumpHeight;
        public float JumpTime => _jumpTime;
    }
}