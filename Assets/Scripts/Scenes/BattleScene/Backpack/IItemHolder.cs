using Items;

namespace BattleScene.Backpack
{
    public interface IItemHolder
    {
        public void Add(Item item);
        public void Remove(DraggableItem item);
        public void Hide(DraggableItem itemToHide);
    }
}