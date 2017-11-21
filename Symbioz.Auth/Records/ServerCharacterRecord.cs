using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Core;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Auth.Records
{
    [Table("ServersCharacters")]
    public class ServerCharacterRecord : ITable
    {
        public static List<ServerCharacterRecord> ServersCharacters = new List<ServerCharacterRecord>();

        [Primary]
        public ulong Id;

        public long CharacterId;

        public int AccountId;

        /// <summary>
        /// Update <=> Dans le cas d'un changement de serveur
        /// </summary>
        [Update]
        public ushort ServerId;

        public ServerCharacterRecord(ulong id, long characterId, int accountId, ushort serverId)
        {
            this.Id = id;
            this.CharacterId = characterId;
            this.AccountId = accountId;
            this.ServerId = serverId;
        }

        public static sbyte GetCharactersCount(int serverId, int accountId)
        {
            return (sbyte)ServersCharacters.FindAll(x => x.ServerId == serverId && x.AccountId == accountId).Count;
        }
        public static ushort[] GetAccountActiveServers(int accountId)
        {
            return ServersCharacters.FindAll(x => x.AccountId == accountId).ConvertAll<ushort>(x => x.ServerId).Distinct().ToArray();
        }

        public static bool Add(long characterId, int accountId, ushort serverId)
        {
            ulong id = ServersCharacters.DynamicPop(x => x.Id);

            if (ServersCharacters.Find(x => x.ServerId == serverId && x.AccountId == accountId && x.CharacterId == characterId) != null)
                return false;
            new ServerCharacterRecord(id, characterId, accountId, serverId).AddInstantElement<ServerCharacterRecord>();
            return true;
        }
        public static void ResetServer(ushort serverId)
        {
            var serversCharacters = ServersCharacters.FindAll(x => x.ServerId == serverId);

            foreach (var sCharacter in serversCharacters)
            {
                sCharacter.RemoveInstantElement<ServerCharacterRecord>();
            }
        }
        public static bool DeleteCharacter(long characterId, ushort serverId)
        {
            var serverCharacter = ServersCharacters.Find(x => x.CharacterId == characterId && x.ServerId == serverId);
            if (serverCharacter == null)
                return false;
            try
            {
                serverCharacter.RemoveInstantElement<ServerCharacterRecord>();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
