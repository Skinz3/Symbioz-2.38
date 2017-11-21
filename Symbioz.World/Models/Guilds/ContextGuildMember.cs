using Symbioz.Protocol;
using Symbioz.Protocol.Types;
using Symbioz.World.Network;
using Symbioz.World.Records;
using Symbioz.World.Records.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Entities;
using Symbioz.Protocol.Enums;

namespace Symbioz.World.Models.Guilds
{
    public class ContextGuildMember
    {
        public long CharacterId
        {
            get;
            set;
        }
        public ushort Rank
        {
            get;
            set;
        }
        public ulong GivenExperience
        {
            get;
            set;
        }
        /// <summary>
        /// Majuscule sur le e u_u
        /// </summary>
        public sbyte experienceGivenPercent
        {
            get;
            set;
        }

        public uint Rights
        {
            get;
            set;
        }
        public bool Connected
        {
            get
            {
                return WorldServer.Instance.IsOnline(CharacterId);
            }
        }
        public ushort MoodSmileyId
        {
            get;
            set;
        }
        public int AchievementPoints
        {
            get;
            set;
        }

      
        public static ContextGuildMember New(Character character, bool isBoss)
        {
            return new ContextGuildMember()
            {
                AchievementPoints = 0,
                CharacterId = character.Id,
                experienceGivenPercent = 0,
                GivenExperience = 0,
                MoodSmileyId = 0, 
                Rank = (ushort)(isBoss ? 1 : 0),
                Rights = (uint)(isBoss ? GuildRightsBitEnum.GUILD_RIGHT_BOSS : GuildRightsBitEnum.GUILD_RIGHT_NONE),
            };
        }

    }
}
