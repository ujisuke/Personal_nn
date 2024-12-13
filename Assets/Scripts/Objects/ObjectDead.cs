using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ObjectDead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Freeze();
            GetComponent<SetShadow>().DestroyShadow();
            GetComponent<IObjectMain>().DestroyDeadObject();
        }
    }
}