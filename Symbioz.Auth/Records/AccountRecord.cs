using Symbioz.ORM;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Selfmade.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Auth.Records
{
    [Table("Accounts", false)]
    public class AccountRecord : ITable
    {
        [Primary]
        public int Id;

        public string Username;

        public string Password;

        [Update]
        public string Nickname;

        public sbyte Role;

        [Ignore]
        public ServerRoleEnum RoleEnum { get { return (ServerRoleEnum)Role; } }

        [Update]
        public bool Banned;

        public sbyte CharacterSlots;

        [Update]
        public ushort LastSelectedServerId;


        public AccountRecord(int id, string username, string password, string nickname, sbyte role,
            bool banned, sbyte characterSlots, ushort lastSelectedServerId)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.Nickname = nickname;
            this.Role = role;
            this.Banned = banned;
            this.CharacterSlots = characterSlots;
            this.LastSelectedServerId = lastSelectedServerId;
        }
        public AccountRecord(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public AccountData ToAccountData()
        {
            return new AccountData(Id, Username, Password, Nickname, Banned, CharacterSlots, Role, LastSelectedServerId);
        }
        public static AccountData GetAccountByUsername(string username)
        {
            return Query("Username", username);
        }
        public static AccountData GetAccountByNickname(string nickname)
        {
            return Query("Nickname", nickname);
        }
        public static AccountRecord GetAccountRecord(int id)
        {
            return DatabaseReader<AccountRecord>.ReadFirst("Id =" + "'" + id + "'");
        }
        private static AccountData Query(string field, string fieldValue)
        {

            var record = DatabaseReader<AccountRecord>.ReadFirst("" + field + " =" + "'" + fieldValue + "'");
            return record != null ? record.ToAccountData() : null;

        }
        static AccountRecord ToAccountRecord(AccountData accountData)
        {
            return new AccountRecord(accountData.Id, accountData.Username, accountData.Password,
                accountData.Nickname, (sbyte)accountData.Role, accountData.Banned,
                accountData.CharacterSlots, accountData.LastSelectedServerId);
        }
        public void Ban()
        {
            Banned = true;
            this.UpdateInstantElement();
        }
        public static bool NicknameExist(string nickname)
        {
            AccountRecord account = DatabaseReader<AccountRecord>.ReadFirst("Nickname =" + "'" + nickname + "'");
            return account != null ? true : false;
        }
        public static void UpdateAccount(AccountData account)
        {
            ToAccountRecord(account).UpdateInstantElement<AccountRecord>();
        }
    }
}
