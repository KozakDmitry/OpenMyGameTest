using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.Logic.Gameplay.Field
{
    public class Grid<T>
    {
        public int Width => _size.x;
        public int Height => _size.y;

        public Vector2Int Size => _size;

        private Vector2Int _size;
        private T[][] _matrix;

        public Grid(Vector2Int size)
        {
            UpdateMatrix(size);
        }

        public void UpdateMatrix(Vector2Int size)
        {
            if (_size == size)
            {
                return;
            }

            _size = size;
            _matrix = new T[size.y][];

            for (int i = 0; i < _size.y; i++)
            {
                _matrix[i] = new T[_size.x];
            }
        }


        public T this[int x, int y]
        {
            get
            {
                return _matrix[y][x];
            }

            set
            {
                _matrix[y][x] = value;
            }
        }

        public T this[Vector2Int index]
        {
            get => this[index.x, index.y];

            set => this[index.x, index.y] = value;
        }

    }
}
