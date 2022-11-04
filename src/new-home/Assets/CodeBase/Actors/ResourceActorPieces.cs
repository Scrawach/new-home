using CodeBase.GameResources;
using UnityEngine;

namespace CodeBase.Actors
{
    public class ResourceActorPieces : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _minerals;

        public void UpdatePiece(ResourceActorPack pack)
        {
            if (pack.Type == ResourceType.Minerals)
                _minerals.SetActive(pack.Count > 0);
        }

        public void Disable()
        {
            _minerals.SetActive(false);
        }
    }
}