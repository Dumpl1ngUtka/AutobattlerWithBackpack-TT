using System.Collections.Generic;
using System.Linq;
using Items;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene.Backpack
{
    public class BackpackContainer : MonoBehaviour
    {
        [SerializeField] private List<BackpackContainerCell> _cells;
        [SerializeField] private GridLayoutGroup _grid;
        [SerializeField] private RectTransform _increaseOptionPanel;
        private Vector2Int _maxCellIndex;
        private Dictionary<DraggableItem, BackpackContainerCell> _items;
        private Vector2Int _enableCellsIndex;
        
        private List<BackpackContainerCell> EnableCells => _cells.Where(cell => cell.gameObject.activeSelf).ToList();

        private void OnEnable()
        {
            foreach (var cell in _cells)
            {
                cell.Init(this);
                if (cell.GridPosition.x > _maxCellIndex.x)
                    _maxCellIndex.x = cell.GridPosition.x;
                if (cell.GridPosition.y > _maxCellIndex.y)
                    _maxCellIndex.y = cell.GridPosition.y;
                cell.gameObject.SetActive(false);
            }
            _items = new Dictionary<DraggableItem, BackpackContainerCell>();
            _enableCellsIndex = new Vector2Int();
            IncreaseCellIndex(new Vector2Int(2,2));
            SwitchOptionPanelVisible();
        }

        public void SwitchOptionPanelVisible()
        {
            _increaseOptionPanel.gameObject.SetActive(!_increaseOptionPanel.gameObject.activeSelf);
        }
        
        public void IncreaseRowIndex() => IncreaseCellIndex(new Vector2Int(0, 1));
        public void IncreaseColumnIndex() => IncreaseCellIndex(new Vector2Int(1, 0));

        private void IncreaseCellIndex(Vector2Int cellIndex)
        {
            _enableCellsIndex += cellIndex;
            if (_enableCellsIndex.x > _maxCellIndex.x)
                _enableCellsIndex.x = _maxCellIndex.x;
            if (_enableCellsIndex.y > _maxCellIndex.y)
                _enableCellsIndex.y = _maxCellIndex.y;
            _grid.constraintCount = _enableCellsIndex.x + 1;

            foreach (var cell in _cells)
            {
                if (cell.GridPosition.x <= _enableCellsIndex.x && cell.GridPosition.y <= _enableCellsIndex.y)
                    cell.gameObject.SetActive(true);
            }
            Invoke(nameof(SetItemsPosition), 0.01f);
        }
        
        public List<Item> GetItems()
        {
            return _items.Keys.Select(k => k.Item).ToList();
        }

        public bool TryAdd(DraggableItem item, BackpackContainerCell targetCell)
        {
            var relevantCells = GetRelevantCells(item.Item.Slots, targetCell, EnableCells);

            if (relevantCells.Count < ItemSlotsCount(item.Item))
                return false;
            
            if (_items.TryGetValue(item, out var currentCell))
            {
                var currentOccupiedCells = GetRelevantCells(item.Item.Slots, currentCell, EnableCells);
                if (relevantCells.Any(cell => !cell.IsEmpty && !currentOccupiedCells.Contains(cell)))
                    return false;
                
                foreach (var cell in currentOccupiedCells) 
                    cell.SetEmptyStatus(true);
                
                foreach (var cell in relevantCells) 
                    cell.SetEmptyStatus(false);
                
                item.SetHolder(targetCell);
                item.SetTargetPosition(targetCell.RectPosition);
                _items[item] = targetCell;
                
                return false;
            }
            
            if (relevantCells.Any(cell => !cell.IsEmpty))
                return false;

            _items.Add(item, targetCell);
                
            foreach (var cell in relevantCells) 
                cell.SetEmptyStatus(false);
                
            return true;
        }

        public void Remove(DraggableItem item, BackpackContainerCell targetCell)
        {
            _items.Remove(item);
            foreach (var cell in GetRelevantCells(item.Item.Slots, targetCell, EnableCells)) 
                cell.SetEmptyStatus(true);
        }

        private void SetItemsPosition()
        {
            foreach (var item in _items)
            {
                item.Key.SetTargetPosition(item.Value.RectPosition);
            }
        }
        
        private int ItemSlotsCount(Item item)
        {
            var count = 0;
            for (var i = 0; i < 3; i++) {
                for (var j = 0; j < 3; j++) {
                    count += item.Slots[i][j] ? 1 : 0;
                }
            }
            return count;
        }
        
        private List<BackpackContainerCell> GetRelevantCells(bool3x3 mask, BackpackContainerCell target, List<BackpackContainerCell> cells)
        {
            var range = new List<BackpackContainerCell>();
            foreach (var cell in cells)
            {
                var columnDelta = (cell.GridPosition - target.GridPosition).x;
                var lineDelta = (cell.GridPosition - target.GridPosition).y;
                if (GetMatrixValue(mask, lineDelta, columnDelta))
                    range.Add(cell);
            }
            return range;
        }
        
        private bool GetMatrixValue(bool3x3 mask, int lineDelta, int columnDelta)
        {
            if (Mathf.Abs(lineDelta) > 1 || Mathf.Abs(columnDelta) > 1)
                return false;

            if (lineDelta == 0 && columnDelta == 0)
                return mask.c1.y;
            
            var column = GetColumnByDelta(columnDelta, mask);
            return GetLineByDelta(lineDelta, column);
        }

        private bool3 GetColumnByDelta(int columnDelta, bool3x3 matrix)
        {
            return columnDelta switch
            {
                -1 => matrix.c0,
                0 => matrix.c1,
                1 => matrix.c2,
                _ => new bool3(false, false, false)
            };
        }

        private bool GetLineByDelta(int lineDelta, bool3 line)
        {
            return lineDelta switch
            {
                -1 => line.x,
                0 => line.y,
                1 => line.z,
                _ => false
            };
        }
    }
}