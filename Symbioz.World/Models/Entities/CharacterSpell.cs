using Symbioz.Protocol.Types;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace Symbioz.World.Models.Entities
{
    /// <summary>
    /// Représente un sort possédé par un personnage, serializé en XML dans CharacterRecord.cs
    /// </summary>
    public class CharacterSpell
    {
        public CharacterSpell(ushort spellId, sbyte grade)
        {
            this.SpellId = spellId;
            this.Grade = grade;
        }
        public CharacterSpell()
        {

        }
        public ushort SpellId
        {
            get;
            private set;
        }

        public sbyte Grade
        {
            get;
            private set;
        }

        [YAXDontSerialize]
        private SpellRecord m_template;

        [YAXDontSerialize]
        public SpellRecord Template
        {
            get
            {
                if (m_template == null)
                {
                    m_template = SpellRecord.GetSpellRecord((ushort)SpellId);
                    return m_template;
                }
                else
                {
                    return m_template;
                }
            }
        }
        public void SetGrade(sbyte grade)
        {
            this.Grade = grade;
        }
        public SpellItem GetSpellItem()
        {
            return new SpellItem(SpellId, Grade);
        }
        public static ushort GetBoostCost(sbyte actualspellgrade, sbyte newgrade)
        {
            ushort cost = 0;
            for (sbyte i = actualspellgrade; i < newgrade; i++)
            {
                cost += (ushort)i;
            }
            return (ushort)(cost);
        }


    }
}
