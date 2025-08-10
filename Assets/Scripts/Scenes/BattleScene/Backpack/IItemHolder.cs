using Items;

namespace BattleScene.Backpack
{
    public interface IItemHolder
    {
        public void Add(DraggableItem item);
        public void Remove(DraggableItem item);
    }
}