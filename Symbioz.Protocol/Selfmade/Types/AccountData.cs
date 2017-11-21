using SSync.IO;
using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Protocol.Selfmade.Types
{
    public class AccountData
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Nickname { get; set; }

        public bool Banned { get; set; }

        public sbyte CharacterSlots { get; set; }

        public ServerRoleEnum Role { get; set; }

        public string Ticket { get; set; }

        public ushort LastSelectedServerId { get; set; }

        public AccountData(int id, string username, string password, string nickname, bool banned,
        sbyte characterSlots,sbyte role, ushort lastSelectedServerId)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.Nickname = nickname;
            this.Banned = banned;
            this.CharacterSlots = characterSlots;
            this.LastSelectedServerId = lastSelectedServerId;
            this.Role = (ServerRoleEnum)role;
        }
        public AccountData() { }

        public void Serialize(ICustomDataOutput writer)
        {
            writer.WriteInt(Id);
            writer.WriteUTF(Username);
            writer.WriteUTF(Password);
            writer.WriteUTF(Nickname);
            writer.WriteBoolean(Banned);
            writer.WriteSByte(CharacterSlots);
            writer.WriteInt((int)Role);
            writer.WriteUTF(Ticket);
            writer.WriteUShort((ushort)LastSelectedServerId);
        }
        public void Deserialize(ICustomDataInput reader)
        {
            this.Id = reader.ReadInt();
            this.Username = reader.ReadUTF();
            this.Password = reader.ReadUTF();
            this.Nickname = reader.ReadUTF();
            this.Banned = reader.ReadBoolean();
            this.CharacterSlots = reader.ReadSByte();
            this.Role = (ServerRoleEnum)reader.ReadInt();
            this.Ticket = reader.ReadUTF();
            this.LastSelectedServerId = reader.ReadUShort();
        }

    }
}
