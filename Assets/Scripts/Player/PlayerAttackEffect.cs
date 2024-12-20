using Assets.Scripts.Objects;
using Assets.Scripts.Stage;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAttackEffect : MonoBehaviour
    {
        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x * 0.6f, 0f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x * 0.6f, transform.localScale.y * 0.3f , transform.localScale.y * 0.3f / StageCreator._TileHeight);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }
    }
}