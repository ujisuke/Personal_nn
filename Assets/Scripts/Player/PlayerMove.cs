using UnityEngine;
using Assets.Scripts.Objects;
using Assets.Scripts.Stage;

namespace Assets.Scripts.Player
{
    public class PlayerMove : MonoBehaviour
    {
        private ObjectMove objectMove;
        private bool isAfterAttack = true;

        private void OnEnable()
        {
            objectMove = GetComponent<ObjectMove>();
            isAfterAttack = true;
        }

        private void FixedUpdate()
        {
            objectMove.HeadToMinusX(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W));
            objectMove.HeadToPlusY(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D));
            objectMove.HeadToMinusY(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A));
            objectMove.HeadToPlusX(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S));
            objectMove.TryToJump(Input.GetKey(KeyCode.Space));

            if(isAfterAttack && !Input.GetMouseButton(0)) isAfterAttack = false;
        }
        
        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x / 4f, 0f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x / 4f, transform.localScale.y, transform.localScale.y / StageFacade._tileHeight);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }

        public bool CanAttack()
        {
            return Input.GetMouseButton(0) && !isAfterAttack;
        }
    }
}
