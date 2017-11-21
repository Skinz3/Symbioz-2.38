using Symbioz.World.Records.Guilds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Entities;
using Symbioz.Protocol.Messages;
using Symbioz.World.Records;
using Symbioz.Protocol.Types;
using Symbioz.Core;
using Symbioz.World.Providers.Guilds;
using SSync.Messages;
using Symbioz.World.Models.Entities.HumanOptions;
using Symbioz.Protocol.Enums;

namespace Symbioz.World.Models.Guilds
{
    public class GuildInstance
    {
        public int Id
        {
            get
            {
                return Record.Id;
            }
        }
        public ushort Level
        {
            get
            {
                return ExperienceRecord.GetLevelFromGuildExperience(Record.Experience);
            }
        }



        public ulong ExperienceLevelFloor
        {
            get
            {
                return ExperienceRecord.GetExperienceForLevel(Level).Guild;
            }
        }
        public ulong ExperienceNextLevelFloor
        {
            get
            {
                return ExperienceRecord.GetExperienceForNextLevel(Level).Guild;
            }
        }
        public ulong Experience
        {
            get
            {
                return Record.Experience;
            }
        }
        public GuildMemberInstance Boss
        {
            get
            {
                return Members.FirstOrDefault(x => x.IsBoss);
            }
        }
        public List<GuildMemberInstance> Members
        {
            get;
            private set;
        }
        public List<GuildMemberInstance> ConnectedMembers
        {
            get
            {
                return Members.FindAll(x => x.Connected);
            }
        }
        public GuildRecord Record
        {
            get;
            private set;
        }

        public bool CanAddMember()
        {
            return Members.Count < GuildProvider.MAX_MEMBERS_COUNT;
        }

        public GuildInstance(GuildRecord record)
        {
            this.Record = record;

            Members = new List<GuildMemberInstance>();

            foreach (var memberRec in Record.Members)
            {
                Members.Add(new GuildMemberInstance(this, memberRec));
            }
        }
        public void SetMotd(GuildMemberInstance member, string content)
        {
            if (content.Length > GuildProvider.MOTD_MAX_LENGHT)
            {
                Send(new GuildMotdSetErrorMessage(0));
            }
            Record.Motd = new GuildMotd()
            {
                Content = content,
                MemberId = (ulong)member.Id,
                MemberName = member.CharacterRecord.Name,
                Timestamp = DateTime.Now.DateTimeToUnixTimestampSeconds(),
            };
            Record.UpdateElement();
            Send(new GuildMotdMessage(Record.Motd.Content, Record.Motd.Timestamp, Record.Motd.MemberId, Record.Motd.MemberName));
        }
        public void Join(Character character, bool isBoss)
        {
            var memberRecord = ContextGuildMember.New(character, isBoss);
            Record.Members.Add(memberRecord);
            GuildMemberInstance member = new GuildMemberInstance(character, this, memberRecord);
            Members.Add(member);
            Record.UpdateElement();
        }

        public void AddXp(int experience)
        {
            var guildLevel = Level;
            this.Record.Experience += (ulong)experience;
            if (guildLevel != this.Level)
            {
                Foreach(x => x.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 208, this.Level));
                Send(new GuildLevelUpMessage((byte)Level));
            }
        }

        public void Send(Message message)
        {
            foreach (var member in ConnectedMembers)
            {
                member.Character.Client.Send(message);
            }
        }
        public void Foreach(Action<GuildMemberInstance> action)
        {
            foreach (var member in ConnectedMembers)
            {
                action(member);
            }
        }
        public void Leave(GuildMemberInstance member, bool kicked)
        {
            if (member.IsBoss)
                return;

            this.Record.Members.Remove(member.Record);
            Members.Remove(member);
            member.CharacterRecord.GuildId = 0;

            if (member.Connected)
            {
                member.Character.Guild = null;
                member.Character.RemoveHumanOption<CharacterHumanOptionGuild>();
                member.Character.Client.Send(new GuildLeftMessage());
            }
            else
            {
                member.CharacterRecord.HumanOptions.RemoveAll(x => x is CharacterHumanOptionGuild);
                member.CharacterRecord.UpdateElement();
            }

            this.Record.UpdateElement();
            Send(new GuildMemberLeavingMessage(kicked, (ulong)member.Id));

            if (Members.Count == 0)
            {
                GuildProvider.Instance.RemoveGuild(this);
            }

        }

        public long AdjustGivenExperience(Character giver, long amount)
        {
            int num = (int)(giver.Level - this.Level);
            long result;
            for (int i = GuildProvider.XP_PER_GAP.Length - 1; i >= 0; i--)
            {
                if ((double)num > GuildProvider.XP_PER_GAP[i][0])
                {
                    result = (long)((double)amount * GuildProvider.XP_PER_GAP[i][1] * 0.01);
                    return result;
                }
            }
            result = (long)((double)amount * GuildProvider.XP_PER_GAP[0][1] * 0.01);
            return result;
        }

        public void SetBoss(GuildMemberInstance member)
        {
            if (this.Boss != member)
            {
                var oldBoss = this.Boss;
                oldBoss.Record.Rank = 0;
                oldBoss.Rights = GuildRightsBitEnum.GUILD_RIGHT_NONE;
                oldBoss.Update();

                member.Record.Rank = 1;
                member.Rights = GuildRightsBitEnum.GUILD_RIGHT_BOSS;
                member.Update();

                if (Members.Count > 1)
                {
                    Foreach(x => x.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 199, new string[]
                    {
                                    member.Character.Name,
                                   oldBoss.Character.Name,
                                    this.Record.Name,
                    }));
                }

            }
        }

        public GuildMemberInstance[] GetMembers()
        {
            return Members.ToArray();
        }
        public GuildMemberInstance GetMember(long id)
        {
            return Members.FirstOrDefault(x => x.Record.CharacterId == id);
        }
        public BasicGuildInformations GetBasicGuildInformations()
        {
            return new BasicGuildInformations((uint)Id, Record.Name, (byte)Level);
        }
        public GuildInformationsGeneralMessage GetGuildInformationsGeneralMessage()
        {
            return new GuildInformationsGeneralMessage(true, false, (byte)Level, ExperienceLevelFloor,
                Experience, ExperienceNextLevelFloor, DateTime.Now.DateTimeToUnixTimestampSeconds(), (ushort)Members.Count, (ushort)ConnectedMembers.Count);
        }
        public GuildInformationsMembersMessage GetGuildInformationsMembersMessage()
        {
            GuildMember[] members = Array.ConvertAll(Members.ToArray(), x => x.ToGuildMember());
            return new GuildInformationsMembersMessage(members);
        }
    }
}
