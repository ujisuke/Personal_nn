using Assets.ScriptableObjects;
using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerParameter _playerParameter;
        
        private void Awake()
        {
            GetComponent<ObjectStateMachine>().Initialize(new PlayerReadyState());
            gameObject.GetComponent<PlayerMain>().InitializePlayer(_playerParameter);
            gameObject.GetComponent<PlayerMove>().Initialize(_playerParameter);
            gameObject.GetComponent<PlayerDash>().Initialize(_playerParameter);
            gameObject.GetComponent<PlayerAttack>().Initialize(_playerParameter);
            GetComponent<PlayerAnimation>().Initialize(_playerParameter);
        }
    }
}