using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Monsters
{
    [Table("DayFights")]
    public class DayFightRecord : ITable
    {
        public static List<DayFightRecord> DayFights = new List<DayFightRecord>();

        public string DayOfWeek;

        public int MapId;

        public List<ushort> Monsters;

        [Ignore]
        public DayOfWeek DayOfWeekEnum
        {
            get
            {
                return (DayOfWeek)Enum.Parse(typeof(DayOfWeek), DayOfWeek);
            }
        }
        public DayFightRecord(string dayOfWeek, int mapId, List<ushort> monsters)
        {
            this.DayOfWeek = dayOfWeek;
            this.MapId = mapId;
            this.Monsters = monsters;
        }

        public static DayFightRecord GetDayFight()
        {
            return DayFights.Find(x => x.DayOfWeekEnum == DateTime.Now.DayOfWeek);
        }
    }
}
