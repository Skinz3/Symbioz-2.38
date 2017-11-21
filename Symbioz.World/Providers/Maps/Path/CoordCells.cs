using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Maps.Path
{
    public static class CoordCells
    {
        public static List<CellData> Cells = new List<CellData>();

        public static CellData GetCell(short id)
        {
            return Cells.FirstOrDefault(cell => cell.Id == id);
        }
        public static CellData GetCell(int x, int y)
        {
            return Cells.FirstOrDefault(cell => cell.X == x && cell.Y == y);
        }

        private static sbyte x = 0;
        private static sbyte y = 0;
        private static short ID = 0;

        static CoordCells()
        {
            var cells = new List<CellData>();
            for (short i = 0; i < 14; i++)
            {
                var data = new sbyte[] { x, y };
                ID = i;
                for (short j = i; j <= 560 - (28 - i); j += 28)
                {
                    cells.Add(new CellData() { Id = ID, X = data[0], Y = data[1] });
                    data[0]++;
                    data[1]--;
                    ID += 28;
                }
                x++;
                y++;
            }
            x = 1;
            y = 0;
            for (short i = 14; i < 28; i++)
            {
                var data = new sbyte[] { x, y };
                ID = i;
                for (short j = i; j <= 560 - (28 - i); j += 28)
                {
                    cells.Add(new CellData() { Id = ID, X = data[0], Y = data[1] });
                    data[0]++;
                    data[1]--;
                    ID += 28;
                }
                x++;
                y++;
            }
            Cells = cells.OrderBy(cell => cell.Id).ToList();

            SearNeighbors();
        }

        private static void SearNeighbors()
        {
            foreach (var cell in Cells)
            {
                // lignes 
                var cells = Cells.FindAll(x => (x.X == cell.X - 1 && x.Y == cell.Y) ||
                                               (x.X == cell.X + 1 && x.Y == cell.Y) ||
                                               (x.Y == cell.Y - 1 && x.X == cell.X) ||
                                               (x.Y == cell.Y + 1 && x.X == cell.X));
                cell.Line = cells;

                // diagonales
                cells = Cells.FindAll(x => (x.X == cell.X - 1 && x.Y == cell.Y - 1) ||
                                           (x.X == cell.X - 1 && x.Y == cell.Y + 1) ||
                                           (x.X == cell.X + 1 && x.Y == cell.Y - 1) ||
                                           (x.X == cell.X + 1 && x.Y == cell.Y + 1));

                cell.Diagonal = cells;
            }
        }

        public class CellData
        {
            public short Id { get; set; }
            public sbyte X { get; set; }
            public sbyte Y { get; set; }

            public List<CellData> Line { get; set; }
            public List<CellData> Diagonal { get; set; }
        }
    }
}
