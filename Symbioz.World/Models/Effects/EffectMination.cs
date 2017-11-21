using Symbioz.Protocol.Types;
using Symbioz.World.Models.Monsters;
using Symbioz.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Effects
{
    public class EffectMination : Effect
    {
        public const ushort DummyTextEffectId = 990;

        public const string EffectMessage = "Contrôle du monstre {0} ({1})";

        public ushort MonsterId
        {
            get;
            set;
        }
        public sbyte GradeId
        {
            get;
            set;
        }
        public string MonsterName
        {
            get;
            set;
        }
        public EffectMination()
        {

        }
        public EffectMination(ushort monsterId, string monsterName, sbyte gradeId)
            : base(DummyTextEffectId)
        {
            this.MonsterId = monsterId;
            this.MonsterName = monsterName;
            this.GradeId = gradeId;
        }
        public MonsterRecord GetTemplate()
        {
            return MonsterRecord.GetMonster(MonsterId);
        }
        public MonsterGrade GetGrade()
        {
            return GetTemplate().GetGrade(GradeId);
        }
        public override ObjectEffect GetObjectEffect()
        {
            return new ObjectEffectString(EffectId, string.Format(EffectMessage, MonsterName, GradeId));
        }
        public override bool Equals(object obj)
        {
            return obj is EffectMination ? this.Equals(obj as EffectMination) : false;
        }
        public bool Equals(EffectMination effect)
        {
            return this.EffectId == effect.EffectId && this.MonsterId == effect.MonsterId && this.GradeId == effect.GradeId && this.MonsterName == effect.MonsterName;
        }
    }
}
