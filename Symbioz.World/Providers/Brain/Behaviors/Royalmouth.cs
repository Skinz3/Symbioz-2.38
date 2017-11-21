using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Models.Maps;
using Symbioz.World.Providers.Fights.Buffs;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Providers.Brain.Actions;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    [Behavior("Royalmouth")]
    public class Royalmouth : Behavior
    {
        public const ushort InitmouthSpellId = 2356;

        private SpellRecord InitmouthRecord
        {
            get;
            set;
        }
        private SpellLevelRecord InitmouthLevel
        {
            get;
            set;
        }
        private bool IsInitmouthed
        {
            get
            {
                return Fighter.HasState(x => x.Invulnerable);
            }
        }

        public short MpBuffAmount
        {
            get
            {
                return (short)InitmouthLevel.Effects.FirstOrDefault(x => x.EffectEnum == EffectsEnum.Effect_AddMP_128).DiceMin;
            }
        }
        public EffectInstance InvulnerabilityEffect
        {
            get
            {
                return InitmouthLevel.Effects.FirstOrDefault(x => x.EffectEnum == EffectsEnum.Effect_AddState);
            }

        }
        public Royalmouth(BrainFighter fighter)
            : base(fighter)
        {
            this.Fighter.Fight.FightStartEvt += Fight_FightStart;
            this.Fighter.AfterSlideEvt += Fighter_OnSlideEvt;
            this.Fighter.OnPushDamages += Fighter_OnPushDamages;
            this.Fighter.OnTurnStartEvt += fighter_OnTurnStartEvt;
            this.InitmouthRecord = SpellRecord.GetSpellRecord(InitmouthSpellId);
            this.InitmouthLevel = this.InitmouthRecord.GetLastLevel();
        }

        void Fighter_OnPushDamages(Fighter obj, Fighter source, short delta, sbyte cellsCount, bool headOn)
        {
            if (source.Point.IsInLine(Fighter.Point))
            {
                KillsFightersInLine(Fighter.CellId, Fighter.Point.OrientationTo(source.Point, false));
            }
        }

        void Fighter_OnSlideEvt(Fighter obj, Fighter source, short startCellId, short endCellId)
        {
            if (IsInitmouthed)
            {
                DebuffInitmouth();
                RegainMpOnSlided();
            }
        }

        void fighter_OnTurnStartEvt(Fighter obj)
        {
            bool seq = Fighter.Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_SPELL);
            if (!IsInitmouthed)
                this.AddInvulnerabilityBuff();

            if (seq)
                Fighter.Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_SPELL);
        }



        void Fight_FightStart(Fight fight)
        {
            Inimouth();
        }
        /// <summary>
        /// Tue les joueurs en ligne lorsque le Royalmouth est poussé contre un obstacle
        /// </summary>
        /// <param name="startCellId"></param>
        /// <param name="endCellId"></param>
        private void KillsFightersInLine(short startCellId, DirectionsEnum direction)
        {
            MapPoint startPoint = new MapPoint(startCellId);
            MapPoint point2 = new MapPoint(startCellId);
            short i = 1;

            while (point2 != null)
            {
                Fighter target = Fighter.Fight.GetFighter(point2);

                if (target != null && Fighter.OposedTeam() == target.Team)
                {
                    target.Stats.CurrentLifePoints = 0;
                }

                point2 = startPoint.GetCellInDirection(direction, i);
                i++;
            }

            Fighter.Fight.CheckDeads();

        }
        /// <summary>
        /// Supprime les effets de l'invulnerabilité donné par Initmouth.
        /// </summary>
        public void DebuffInitmouth()
        {
            Fighter.DispellSpellBuffs(Fighter, InitmouthRecord.Id);
        }
        /// <summary>
        /// Initialize le Royalmouth au début du combat
        /// </summary>
        private void Inimouth()
        {
            Fighter.CastSpell(InitmouthRecord, InitmouthRecord.GetLastLevelGrade(), Fighter.CellId, Fighter.CellId, false);
        }
        /// <summary>
        /// Ajoute au royalmouth un buff d'invulnérabilité 
        /// </summary>
        private void AddInvulnerabilityBuff()
        {
            StateBuff buff = new StateBuff(Fighter.BuffIdProvider.Pop(), Fighter, Fighter, InitmouthLevel, InvulnerabilityEffect,
                InitmouthSpellId, false,FightDispellableEnum.REALLY_NOT_DISPELLABLE, SpellStateRecord.GetState(InvulnerabilityEffect.Value));
            Fighter.AddAndApplyBuff(buff);
        }
        /// <summary>
        /// Ajoute au royalmouth les points de mouvement apres avoir été poussé
        /// </summary>
        private void RegainMpOnSlided()
        {
            Fighter.RegainMp(Fighter.Id, MpBuffAmount);
        }
        public override ActionType[] GetSortedActions()
        {
            return new ActionType[] { ActionType.MoveToEnemy, ActionType.CastSpell };
        }
    }
}
