using Items;

namespace BattleScene.Backpack
{
    public interface IItemHolder
    {
        public bool Add(DraggableItem item);
        public void Remove(DraggableItem item);
    }
}