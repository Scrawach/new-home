using UnityEngine;

namespace CodeBase.Buildings
{
    public class BuildingsGrid
    {
        private readonly Vector2Int _size;
        private readonly Building[,] _buildings;

        public BuildingsGrid(Vector2Int size)
        {
            _size = size;
            _buildings = new Building[_size.x, _size.y];
        }

        public bool IsValidPlace(Vector2Int point, Vector2Int size)
        {
            return InBorder(point, size) && IsAvailablePlace(point, size);
        }

        public void Place(Vector2Int point, Building building)
        {
            for (var x = 0; x < building.Size.x; x++)
            for (var y = 0; y < building.Size.y; y++)
                _buildings[point.x + x, point.y + y] = building;
        }

        private bool IsAvailablePlace(Vector2Int point, Vector2Int size)
        {
            for (var x = 0; x < size.x; x++)
            for (var y = 0; y < size.y; y++) 
                if (_buildings[point.x + x, point.y + y] != null) 
                    return false;
            return true;
        }

        private bool InBorder(Vector2Int point, Vector2Int buildingSize)
        {
            if (point.x < 0 || point.x > _size.x - buildingSize.x)
                return false;
            
            if (point.y < 0 || point.y > _size.y - buildingSize.y)
                return false;
            
            return true;
        }
    }
}