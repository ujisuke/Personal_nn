using UnityEngine;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Player
{
    public class PlayerMove : MonoBehaviour
    {
        ObjectMove objectMove;
        [SerializeField] private ObjectData objectData;

        private void Start()
        {
            objectMove = GetComponent<ObjectMove>();
            objectMove.Initialize(objectData, transform.position);
            ObjectFacade.SetPlayer(gameObject);
        }

        private void FixedUpdate()
        {
            objectMove.HeadToA(Input.GetKey(KeyCode.A));
            objectMove.HeadToW(Input.GetKey(KeyCode.W));
            objectMove.HeadToS(Input.GetKey(KeyCode.S));
            objectMove.HeadToD(Input.GetKey(KeyCode.D));
            objectMove.TryToJump(Input.GetKey(KeyCode.Space));
        }
    }
}
