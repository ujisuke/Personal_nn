using UnityEngine;
using Assets.Scripts.Objects;
using Assets.Scripts.Stage;

namespace Assets.Scripts.Player
{
    public class PlayerMove : MonoBehaviour
    {
        private ObjectMove objectMove;

        private void Start()
        {
            objectMove = GetComponent<ObjectMove>();
            objectMove.Initialize(transform.position);
        }

        private void FixedUpdate()
        {
            objectMove.HeadToA(Input.GetKey(KeyCode.A));
            objectMove.HeadToW(Input.GetKey(KeyCode.W));
            objectMove.HeadToS(Input.GetKey(KeyCode.S));
            objectMove.HeadToD(Input.GetKey(KeyCode.D));
            objectMove.TryToJump(Input.GetKey(KeyCode.Space));
        }
        
        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x / 4f, 0f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x / 4f, transform.localScale.y, transform.localScale.y / StageCreator._tileHeight);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }
    }
}
