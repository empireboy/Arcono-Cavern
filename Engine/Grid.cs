using System;

namespace Engine
{
	public class Grid<T> where T : class, new()
	{
		public T[,] Items { get; private set; }
        public int Width
        {
            get
            {
                return Items.GetLength(0);
            }
        }
        public int Height
        {
            get
            {
                return Items.GetLength(1);
            }
        }

        public Grid(int width, int height, bool autoInitialize = true)
		{
            Items = new T[width, height];

            if (autoInitialize)
                Initialize();
        }

        public void SetItem(int x, int y, T item)
		{
            Items[x, y] = item;
		}

        public void ClearItem(int x, int y)
		{
            Items[x, y] = new T();
		}

        public void Clear()
		{
            if (Items == null)
                throw new NullReferenceException("Can't clear " + this + " because Items is null");

            Items = new T[Width, Height];
            Initialize();
		}

        public T[,] Clone()
		{
            return Items.Clone() as T[,];
		}

        public T GetItem(int x, int y)
		{
            return Items[x, y];
		}

        private void Initialize()
		{
            for (int i = 0; i < Items.GetLength(0); i++)
            {
                for (int j = 0; j < Items.GetLength(1); j++)
                {
                    Items[i, j] = new T();
                }
            }
        }
	}
}
