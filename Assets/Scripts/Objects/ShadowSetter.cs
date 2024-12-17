using Assets.Scripts.Stage;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ShadowSetter : MonoBehaviour
    {
        [SerializeField] private GameObject shadowPrefab;
        private SpriteRenderer shadowSpriteRenderer;

        private void Awake()
        {
            shadowSpriteRenderer = shadowPrefab.GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            if (shadowPrefab == null) return;
            Vector3 shadowImPos3 = ObjectMove.ConvertToImPos3FromRePos3(transform.position);
            (int i, int j) objectTileIndex = ObjectMove.ConvertToTileIndexFromRePos3(transform.position);
            shadowImPos3.z = StageFacade.TileImZs[objectTileIndex.i, objectTileIndex.j] + 1f;
            shadowPrefab.transform.position = ObjectMove.ConvertToRePos3FromImPos3(shadowImPos3);
            shadowSpriteRenderer.sortingOrder = ObjectMove.CalculateSortingOrderFromRePos3(shadowPrefab.transform.position);
            shadowPrefab.transform.position = new Vector3(shadowPrefab.transform.position.x, shadowPrefab.transform.position.y, transform.position.z - 0.1f);
        }

        public void DestroyShadow()
        {
            Destroy(shadowPrefab);
        }
    }
}