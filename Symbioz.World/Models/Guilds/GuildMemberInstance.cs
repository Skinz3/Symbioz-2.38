using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Records;
using Symbioz.World.Records.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Protocol.Enums;
using SSync.Messages;
using Symbioz.Protocol.Messages;

namespace Symbioz.World.Models.Guilds
{
    public class GuildMemberInstance
    {
        public ContextGuildMember Record
        {
            get;
            private set;
        }
        public GuildInstance Guild
        {
            get;
            private set;
        }
        public GuildRightsBitEnum Rights
        {
            get
            {
                return (GuildRightsBitEnum)Record.Rights;
            }
            set
            {
                Record.Rights = (uint)value;
            }
        }
        public long Id
        {
            get
            {
                return Record.CharacterId;
            }
        }
        public Character Character
        {
            get;
            private set;
        }
        public CharacterRecord CharacterRecord
        {
            get;
            private set;
        }
        public bool Connected
        {
            get
            {
                return Character != null;
            }
        }

        public bool IsBoss
        {
            get
            {
                return Record.Rank == 1;
            }
        }

        public void OnDisconnected()
        {
            Character = null;
        }
        public GuildMemberInstance(GuildInstance guild, ContextGuildMember record)
        {
            this.CharacterRecord = CharacterRecord.GetRecord(record.CharacterId);
            this.Record = record;
            this.Guild = guild;
        }

        public bool HasRight(GuildRightsBitEnum rights)
        {
            return this.Rights == GuildRightsBitEnum.GUILD_RIGHT_BOSS || this.Rights.HasFlag(rights);
        }

        public GuildMemberInstance(Character character, GuildInstance guild, ContextGuildMember record)
        {
            this.Record = record;
            this.CharacterRecord = character.Record;
            this.Guild = guild;
            this.Character = character;
            this.Character.OnGuildJoined(Guild, this);

        }

        public void AddExp(int num2)
        {
            this.Character.GuildMember.Record.GivenExperience += (ulong)num2;
            this.Guild.AddXp(num2);
        }

        public void Update()
        {
            Guild.Send(new GuildInformationsMemberUpdateMessage(ToGuildMember()));
        }

        /// <summary>
        /// Todo use informations of local character
        /// </summary>
        /// <returns></returns>
        public GuildMember ToGuildMember()
        {
            return new GuildMember((ulong)Record.CharacterId, CharacterRecord.Name,
                (byte)ExperienceRecord.GetCharacterLevel(CharacterRecord.Exp),
                CharacterRecord.BreedId, CharacterRecord.Sex, Record.Rank,
                Record.GivenExperience, Record.experienceGivenPercent, Record.Rights,
                (sbyte)(Connected == true ? 1 : 0),
            (sbyte)CharacterRecord.Alignment.Side, 0, Record.MoodSmileyId, 0, 0, new PlayerStatus(0));
        }
        public void OnConnected(Character character)
        {
            Character = character;
        }
        public void ChangeParameters(GuildMemberInstance source, uint rights, ushort rank, sbyte xpPercent)
        {
            if (source.IsBoss && rank == 1)
            {
                this.Guild.SetBoss(this);
            }
            else if (source == this || !this.IsBoss)
            {
                if (source.HasRight(GuildRightsBitEnum.GUILD_RIGHT_MANAGE_RANKS) && rank >= 0 && rank <= 35)
                {
                    this.Record.Rank = rank;
                }
                if (source.HasRight(GuildRightsBitEnum.GUILD_RIGHT_MANAGE_RIGHTS))
                {
                    this.Record.Rights = rights;
                }
            }
            if (source.HasRight(GuildRightsBitEnum.GUILD_RIGHT_MANAGE_XP_CONTRIBUTION) || (source == this && source.HasRight(GuildRightsBitEnum.GUILD_RIGHT_MANAGE_MY_XP_CONTRIBUTION)))
            {
                this.Record.experienceGivenPercent = (sbyte)((xpPercent < 90) ? xpPercent : 90);
            }
            this.Guild.Record.UpdateElement();
            this.Update();

            if (this.Connected)
            {
                this.Character.SendGuildMembership();
            }
        }
    }
}
