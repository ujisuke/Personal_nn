using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Player
{
    public class PlayerMove : MonoBehaviour
    {
        ObjectMove objectMove;

        private void Start()
        {
            objectMove = GetComponent<ObjectMove>();
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
