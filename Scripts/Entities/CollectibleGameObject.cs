using Engine;

namespace Arcono
{
    public class CollectibleGameObject : SpriteGameObject
    {
        public bool isCollected;

        public CollectibleGameObject(int x, int y, string assetName) : base(x, y, assetName) 
        {

        }

        public CollectibleGameObject(string assetName) : base(assetName)
        {

        }

        // This function will detect when an item is collected
        public virtual void CollectObject(Player player) 
        {
            isCollected = true;
            visible = false;
        }
    }
}
