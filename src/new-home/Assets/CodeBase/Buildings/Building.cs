using UnityEngine;

namespace CodeBase.Buildings
{
    public class Building : MonoBehaviour
    {
        [field: SerializeField]
        public Vector2Int Size { get; private set; }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 0.5f, 0f, 0.5f);
            for (var x = 0; x < Size.x; x++)
            for (var z = 0; z < Size.y; z++) 
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, z), new Vector3(1, 0.2f, 1));
        }
    }
}
