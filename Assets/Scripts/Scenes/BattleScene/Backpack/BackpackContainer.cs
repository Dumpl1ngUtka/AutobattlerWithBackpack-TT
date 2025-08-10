using System;
using System.Collections.Generic;
using Items;
using Unity.Mathematics;
using UnityEngine;

namespace BattleScene.Backpack
{
    public class BackpackContainer : MonoBehaviour
    {
        [SerializeField] private BackpackContainerCell _cellPrefab;
        [SerializeField] private List<BackpackContainerCell> _cells;

        private void OnEnable()
        {
            foreach (var cell in _cells) 
                cell.Init(this);
        }

        public bool TryAdd(DraggableItem item, BackpackContainerCell targetCell)
        {
            foreach (var cell in GetRelevantCells(item.Item.Slots, targetCell, _cells))
            {
                if (!cell.IsEmpty)
                    return false;
            }

            return true;
        }
        
        public List<BackpackContainerCell> GetRelevantCells(bool3x3 mask, BackpackContainerCell target, List<BackpackContainerCell> cells)
        {
            var range = new List<BackpackContainerCell>();
            foreach (var cell in cells)
            {
                var columnDelta = (cell.Position - target.Position).x;
                var lineDelta = (cell.Position - target.Position).y;
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