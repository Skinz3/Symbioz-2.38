using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.D2OTypes
{
    [D2oType("MonsterGrade")]
    public class MonsterGrade
    {
        [D2OField("grade")]
        public int Id { get; set; }

        [D2OField("level")]
        public ushort Level { get; set; }

        [D2OField("lifePoints")]
        public uint LifePoints { get; set; }

        [D2OField("actionPoints")]
        public short ActionPoints { get; set; }

        [D2OField("movementPoints")]
        public short MovementPoints { get; set; }

        [D2OField("paDodge")]
        public short PADodge { get; set; }

        [D2OField("pmDodge")]
        public short PmDodge { get; set; }

        [D2OField("wisdom")]
        public ushort Wisdom { get; set; }

        [D2OField("earthResistance")]
        public short EarthResistance { get; set; }

        [D2OField("airResistance")]
        public short AirResistance { get; set; }

        [D2OField("fireResistance")]
        public short FireResistance { get; set; }

        [D2OField("waterResistance")]
        public short WaterResistance { get; set; }

        [D2OField("neutralResistance")]
        public short NeutralResistance { get; set; }

        [D2OField("gradeXp")]
        public int GradeXp { get; set; }

        [D2OField("damageReflect")]
        public int DamageReflect { get; set; }

    }
}
