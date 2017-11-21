using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Maps
{
    [Table("Subareas", true, 10)]
    public class SubareaRecord : ITable
    {
        public static List<SubareaRecord> Subareas = new List<SubareaRecord>();

        [Primary]
        public ushort Id;

        public string Name;

        public List<ushort> Monsters;

        public int ExperienceRate;

        public SubareaRecord(ushort id, string name, List<ushort> monsters, int experienceRate)
        {
            this.Id = id;
            this.Name = name;
            this.Monsters = monsters;
            this.ExperienceRate = experienceRate;
        }

        public static SubareaRecord GetSubarea(ushort id)
        {
            return Subareas.Find(x => x.Id == id);
        }

        public override string ToString()
        {
            return Name;
        }


    }
}
