using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.BaseLogic
{
    public class RopeCreator
    {
        private const string RopePath = "Rope";
        
        public Rope Create(Vector2 position)
        {
            Rope prefab = Resources.Load<Rope>(RopePath);
            Rope rope = Object.Instantiate(prefab, position, Quaternion.identity);

            return rope;
        }
    }
}