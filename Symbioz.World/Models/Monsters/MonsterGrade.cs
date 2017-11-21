using Symbioz.World.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Monsters
{
    public class MonsterGrade
    {
        public int Id { get; set; }

        public ushort Level { get; set; }

        public uint LifePoints { get; set; }

        public short ActionPoints { get; set; }

        public short MovementPoints { get; set; }

        public short PADodge { get; set; }

        public short PmDodge { get; set; }

        public ushort Wisdom { get; set; }

        public short EarthResistance { get; set; }

        public short AirResistance { get; set; }

        public short FireResistance { get; set; }

        public short WaterResistance { get; set; }

        public short NeutralResistance { get; set; }

        public int GradeXp { get; set; }

        public int DamageReflect { get; set; }
    }
}
