using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIShearer : BaseMeshEffect
    {
        [SerializeField] private Vector2 shear = new(0f, 0f);

        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive())
                return;

            UIVertex vertex = new UIVertex();
            for (var i = 0; i < vh.currentVertCount; i++)
            {
                vh.PopulateUIVertex(ref vertex, i);
                vertex.position.x += vertex.position.y * shear.x;
                vertex.position.y += vertex.position.x * shear.y;
                vh.SetUIVertex(vertex, i);
            }
        }
    }
}