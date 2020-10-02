using UnityEngine;
 
namespace Valkyrie.Extensions
{
    public static class VectorExtensions 
    {
        /// <summary>
        /// Converts a Vector2 to a Vector2Int.
        /// </summary>
        public static Vector2Int ToVector2Int(this Vector2 self) 
        {
            return new Vector2Int(Mathf.RoundToInt(self.x),
                Mathf.RoundToInt(self.y));
        }
         
        /// <summary>
        /// Converts a Vector2Int to a Vector2.
        /// </summary>
        public static Vector2 ToVector2(this Vector2Int self) 
        {
            return new Vector3(self.x, self.y);
        }
        
        /// <summary>
        /// Converts a Vector3 to a Vector3Int.
        /// </summary>
        public static Vector3Int ToVector3Int(this Vector3 self)
        {
            return new Vector3Int(Mathf.RoundToInt(self.x),
                Mathf.RoundToInt(self.y),
                Mathf.RoundToInt(self.z));
        }
         
        /// <summary>
        /// Converts a Vector3Int to a Vector3.
        /// </summary>
        public static Vector3 ToVector3(this Vector3Int self) 
        {
            return new Vector3(self.x, self.y, self.z);
        }

        public static bool Approximate(this Vector2 self, Vector2 other)
        {
            bool x = Mathf.Approximately(self.x, other.x);
            bool y = Mathf.Approximately(self.y, other.y);

            return x && y;
        }
        
        public static bool Approximate(this Vector3 self, Vector3 other)
        {
            bool x = Mathf.Approximately(self.x, other.x);
            bool y = Mathf.Approximately(self.y, other.y);
            bool z = Mathf.Approximately(self.z, other.z);

            return x && y && z;
        }
    }
}