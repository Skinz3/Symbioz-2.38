using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.ORM;
using Symbioz.World.Models.Entities.Look;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Models.Monsters;
using Symbioz.World.Providers.Brain.Actions;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Monsters
{
    [Table("Monsters", true, 9)]
    public class MonsterRecord : ITable
    {
        public static List<MonsterRecord> Monsters = new List<MonsterRecord>();

        [Primary]
        public ushort Id;

        public string Name;

        [Update]
        public ContextActorLook Look;

        public bool IsBoss;

        public bool IsMiniBoss;

        public short Race;

        public bool UseSummonSlot;

        public bool UseBombSlot;

        [Xml, Update]
        public List<MonsterDrop> Drops;

        public List<ushort> Spells;

        [Ignore]
        public List<SpellRecord> SpellRecords;

        [Xml]
        public List<MonsterGrade> Grades;

        [Update]
        public int MinDroppedKamas;

        [Update]
        public int MaxDroppedKamas;

        [Update]
        public int Power;

        public string BehaviorName;

        public MonsterRecord(ushort id, string name, ContextActorLook look, bool isBoss, bool isMiniboss, short race,
            bool useSummonSlot, bool useBombSlot, List<MonsterDrop> drops, List<ushort> spells, List<MonsterGrade> grades,
            int minDroppedKamas, int maxDroppedKamas, int power, string behaviorName)
        {
            this.Id = id;
            this.Name = name;
            this.Look = look;
            this.IsBoss = isBoss;
            this.IsMiniBoss = isMiniboss;
            this.Race = race;
            this.UseSummonSlot = useSummonSlot;
            this.UseBombSlot = useBombSlot;
            this.Drops = drops;
            this.Spells = spells;
            this.Grades = grades;
            this.MinDroppedKamas = minDroppedKamas;
            this.MaxDroppedKamas = maxDroppedKamas;
            this.SpellRecords = Spells.ConvertAll<SpellRecord>(x => SpellRecord.GetSpellRecord(x));
            this.Power = power;
            this.BehaviorName = behaviorName;
        }
        public bool GradeExist(sbyte gradeId)
        {
            return Grades.Count >= gradeId;
        }
        public MonsterGrade LastGrade()
        {
            return Grades.Last();
        }
        public MonsterGrade GetGrade(sbyte gradeId)
        {
            return Grades[gradeId - 1];
        }
        public sbyte RandomGrade(AsyncRandom random)
        {
            sbyte value = (sbyte)(random.Next(1, Grades.Count + 1));
            return value;
        }
        public static MonsterRecord GetMonster(ushort id)
        {
            return Monsters.Find(x => x.Id == id);
        }

    }
}
