using UnityEngine;

namespace CodeBase.Buildings
{
    public static class VectorExtensions
    {
        public static Vector3Int RoundToInt(this Vector3 self)
        {
            var xPos = Mathf.RoundToInt(self.x);
            var yPos = Mathf.RoundToInt(self.y);
            var zPos = Mathf.RoundToInt(self.z);
            return new Vector3Int(xPos, yPos, zPos);
        }
        
        public static Vector2Int ToVector2Int(this Vector3 self)
        {
            var xPos = Mathf.RoundToInt(self.x);
            var zPos = Mathf.RoundToInt(self.z);
            return new Vector2Int(xPos, zPos);
        }
        
        public static Vector2Int ToVector2Int(this Vector3Int self)
        {
            var xPos = Mathf.RoundToInt(self.x);
            var zPos = Mathf.RoundToInt(self.z);
            return new Vector2Int(xPos, zPos);
        }
        
        public static Vector3 ToPosition(this Vector2Int self)
        {
            return new Vector3(self.x, 0f, self.y);
        }
    }
}