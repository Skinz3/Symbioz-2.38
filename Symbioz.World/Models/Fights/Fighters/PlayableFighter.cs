using SSync.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Providers.Fights.Challenges;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Fighters
{
    public abstract class PlayableFighter : Fighter
    {
        public PlayableFighter(FightTeam team, ushort mapCellId)
            : base(team, mapCellId)
        {
        }
        public Synchronizer PersonalSynchronizer
        {
            get;
            set;
        }

        public abstract void Send(Message message);

        public abstract CharacterSpell GetSpell(ushort spellId);


        public void NoMove()
        {
            this.Send(new GameMapNoMovementMessage((short)Point.X, (short)Point.Y));

        }
        public override void PassTurn()
        {
            base.PassTurn();
        }

        public override void OnMoveFailed()
        {
            this.NoMove();
        }

        internal void ShowChallengeTargetsList(ushort challengeId)
        {
            Challenge challenge = Fight.GetChallenge(challengeId);

            if (challenge != null)
            {
                challenge.ShowTargetsList(this);
            }
        }
        public abstract Character GetCharacterPlaying();

        public override void OnSpellCastFailed(SpellCastResultEnum result, SpellLevelRecord level)
        {
            Character character = GetCharacterPlaying();

            switch (result)
            {
                case SpellCastResultEnum.NotEnoughAp:
                    character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 170, new object[]
                          {
                            Stats.ActionPoints.TotalInContext(),
                             level.ApCost,
                          });
                    break;
                case SpellCastResultEnum.CantBeSeen:
                    character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 174);
                    break;
                case SpellCastResultEnum.HistoryError:
                    character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 175);
                    break;
                case SpellCastResultEnum.FightEnded:
                    character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 175);
                    break;
                case SpellCastResultEnum.NotPlaying:
                    character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 175);
                    break;
                case SpellCastResultEnum.StateForbidden:
                    character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 175);
                    break;
                case SpellCastResultEnum.StateRequired:
                    character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 175);
                    break;
            }
            character.Client.Send(new GameActionFightNoSpellCastMessage((uint)level.Id));
            base.OnSpellCastFailed(result, level);
        }
    }
}
