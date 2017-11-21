using Symbioz.Core;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities.Stats;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Models.Fights.Results;
using Symbioz.World.Models.Items;
using Symbioz.World.Models.Monsters;
using Symbioz.World.Providers;
using Symbioz.World.Providers.Brain;
using Symbioz.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Fighters
{
    public class MonsterFighter : BrainFighter
    {
        private readonly Dictionary<MonsterDrop, int> m_dropsCount = new Dictionary<MonsterDrop, int>();

        public MonsterFighter(FightTeam team, Monster monster, ushort mapCellId)
            : base(team, mapCellId, monster.Template, monster.GradeId)
        {
        }
        public MonsterFighter(FightTeam team, MonsterRecord template, sbyte gradeId, ushort mapCellId)
            : base(
                team, mapCellId, template, gradeId)
        {

        }
        public override GameFightFighterInformations GetFightFighterInformations()
        {
            return new GameFightMonsterInformations(Id, Look.ToEntityLook(), new EntityDispositionInformations((short)CellId, (sbyte)Direction),
                Team.Id, 0, Alive, Stats.GetFightMinimalStats(), new ushort[0], Template.Id, GradeId);
        }
        public override uint GetDroppedKamas()
        {
            AsyncRandom asyncRandom = new AsyncRandom();
            return (uint)asyncRandom.Next(base.Template.MinDroppedKamas, base.Template.MaxDroppedKamas + 1);
        }
        public override void OnTurnStarted()
        {
            base.OnTurnStarted();

            this.PassTurn();
        }
        public override IEnumerable<DroppedItem> RollLoot(IFightResult looter, int dropBonusPercent)
        {
            IEnumerable<DroppedItem> result;
            if (Alive)
            {
                result = new DroppedItem[0];
            }
            else
            {
                AsyncRandom asyncRandom = new AsyncRandom();
                List<DroppedItem> list = new List<DroppedItem>();
                int prospectingSum = base.OposedTeam().GetFighters<CharacterFighter>().Sum((CharacterFighter entry) => entry.Stats.Prospecting.TotalInContext());
                foreach (var current in
                    from droppableItem in base.Template.Drops
                    where prospectingSum >= droppableItem.ProspectingLock && !droppableItem.HasCriteria
                    select droppableItem)
                {
                    int num = 0;
                    while (num < current.Count && (current.DropLimit <= 0 || !this.m_dropsCount.ContainsKey(current) || this.m_dropsCount[current] < current.DropLimit))
                    {
                        var deci = asyncRandom.NextDouble();
                        double num2 = (double)asyncRandom.Next(0, 100) + deci;
                        double num3 = FormulasProvider.Instance.AdjustDropChance(looter, current, GradeId, (int)base.Fight.AgeBonus, dropBonusPercent);

                        if (num3 >= num2)
                        {
                            list.Add(new DroppedItem(current.ItemId, 1u));
                            if (!this.m_dropsCount.ContainsKey(current))
                            {
                                this.m_dropsCount.Add(current, 1);
                            }
                            else
                            {
                                System.Collections.Generic.Dictionary<MonsterDrop, int> dropsCount;
                                MonsterDrop key;
                                (dropsCount = this.m_dropsCount)[key = current] = dropsCount[key] + 1;
                            }
                        }
                        num++;
                    }
                }
                result = list;
            }
            return result;
        }

        public override FightTeamMemberInformations GetFightTeamMemberInformations()
        {
            return new FightTeamMemberMonsterInformations((double)Id, (int)base.Template.Id, base.GradeId);
        }




    }
}
