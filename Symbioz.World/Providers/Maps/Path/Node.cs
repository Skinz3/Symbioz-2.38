using System;
using System.Collections.Generic;
using System.Linq;
using _cell = Symbioz.World.Providers.Maps.Path.CoordCells.CellData;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Maps.Path
{
    internal class Node : INode
    {
        // FIELDS 
        private _cell m_cell;
        private Node m_parent;
        private int m_g;
        private int m_h;
        private bool m_walkable;

        // CONSTRUCTOR
        public Node(_cell cell)
        {
            this.m_cell = cell;
        }

        // PROPERTIES
        public short CellID { get { return this.m_cell.Id; } }
        public List<_cell> Neighbors { get { return this.m_cell.Line; } }

        public sbyte X { get { return this.m_cell.X; } }
        public sbyte Y { get { return this.m_cell.Y; } }
        public Node Parent { get { return this.m_parent; } set { this.SetParent(value); } }

        public int G { get { return this.m_g; } }
        public int H { get { return this.m_h; } }
        public int F { get { return this.m_g + this.H; } }
        public bool Walkable { get { return this.m_walkable; } set { this.m_walkable = value; } }

        // METHODS
        private void SetParent(Node parent)
        {
            this.m_parent = parent;
            if (parent != null)
                this.m_g = this.m_parent.G + 10;
        }

        public void SetHeuristic(Node endPoint)
        {
            this.m_h = Math.Abs(this.X - endPoint.X) + Math.Abs(this.Y - endPoint.Y);
        }

        public int CostWillBe()
        {
            return (this.m_parent != null ? this.m_parent.G + 10 : 0);
        }
    }

    internal class NodeList<T> : List<T> where T : INode
    {
        public T RemoveFirst()
        {
            T first = this[0];
            this.RemoveAt(0);
            return first;
        }

        public new bool Contains(T node)
        {
            return this[node] != null;
        }

        public T this[T node]
        {
            get
            {
                foreach (T n in this)
                {
                    if (n.CellID == node.CellID) return n;
                }
                return default(T);
            }
        }
    }

    internal class SortedNodeList<T> : NodeList<T> where T : INode
    {
        public void AddDichotomic(T node)
        {
            int left = 0;
            int right = this.Count - 1;
            int center = 0;

            while (left <= right)
            {
                center = (left + right) / 2;
                if (node.F < this[center].F)
                    right = center - 1;
                else if (node.F > this[center].F)
                    left = center + 1;
                else
                {
                    left = center;
                    break;
                }
            }
            this.Insert(left, node);
        }
    }

    internal interface INode
    {
        int F { get; }
        short CellID { get; }
    }
}
