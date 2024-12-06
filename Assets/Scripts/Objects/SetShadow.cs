using Assets.Scripts.Stage;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class SetShadow : MonoBehaviour
    {
        [SerializeField] private GameObject shadow;
        private SpriteRenderer shadowSpriteRenderer;

        private void Awake()
        {
            shadowSpriteRenderer = shadow.GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            if (shadow == null) return;
            Vector3 shadowImPos3 = ObjectMove.ConvertToImPos3FromRePos3(transform.position);
            (int i, int j) objectTileIndex = ObjectMove.ConvertToTileIndexFromRePos3(transform.position);
            shadowImPos3.z = StageFacade.TileImZs[objectTileIndex.i, objectTileIndex.j] + 1f;
            shadow.transform.position = ObjectMove.ConvertToRePos3FromImPos3(shadowImPos3);
            shadowSpriteRenderer.sortingOrder = ObjectMove.CalculateSortingOrderFromRePos3(shadow.transform.position);
            shadow.transform.position = new Vector3(shadow.transform.position.x, shadow.transform.position.y, transform.position.z - 0.1f);
        }

        public void DestroyShadow()
        {
            Destroy(shadow);
        }
    }
}