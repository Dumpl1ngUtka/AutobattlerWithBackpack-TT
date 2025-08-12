using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using Unity.Mathematics;
using UnityEngine;

namespace BattleScene.Backpack
{
    public class BackpackContainer : MonoBehaviour
    {
        [SerializeField] private BackpackContainerCell _cellPrefab;
        [SerializeField] private List<BackpackContainerCell> _cells;
        private Dictionary<DraggableItem, BackpackContainerCell> _items;

        private void OnEnable()
        {
            foreach (var cell in _cells) 
                cell.Init(this);
            _items = new Dictionary<DraggableItem, BackpackContainerCell>();
        }

        public List<Item> GetItems()
        {
            return _items.Keys.Select(k => k.Item).ToList();
        }

        public bool TryAdd(DraggableItem item, BackpackContainerCell targetCell)
        {
            var relevantCells = GetRelevantCells(item.Item.Slots, targetCell, _cells);

            if (relevantCells.Count < ItemSlotsCount(item.Item))
                return false;
            
            if (_items.TryGetValue(item, out var currentCell))
            {
                Debug.Log("Reposition");
                var currentOccupiedCells = GetRelevantCells(item.Item.Slots, currentCell, _cells);
                if (relevantCells.Any(cell => !cell.IsEmpty && !currentOccupiedCells.Contains(cell)))
                    return false;
                
                Debug.Log("Not false");

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
            foreach (var cell in GetRelevantCells(item.Item.Slots, targetCell, _cells)) 
                cell.SetEmptyStatus(true);
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