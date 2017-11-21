using Symbioz.ORM;
using Symbioz.World.Records.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records
{
    [Table("AccountsInformations")]
    public class AccountInformationsRecord : ITable
    {
        public static List<AccountInformationsRecord> AccountsInformations = new List<AccountInformationsRecord>();

        [Primary]
        public int AccountId;

        [Update]
        public uint BankKamas;

        public AccountInformationsRecord(int accountId, uint bankKamas)
        {
            this.AccountId = accountId;
            this.BankKamas = bankKamas;
        }

        public static AccountInformationsRecord Create(int accountId)
        {
            var infos = new AccountInformationsRecord(accountId, 0);
            infos.AddInstantElement();
            return infos;
        }
        public static AccountInformationsRecord Load(int accountId)
        {
            var infos = AccountsInformations.Find(x => x.AccountId == accountId);
            return infos != null ? infos : Create(accountId);
        }
        public static void AddBankKamas(int accountId, uint amount)
        {
            var account = AccountsInformations.Find(x => x.AccountId == accountId);
            account.BankKamas += amount;
            account.UpdateElement();
        }
    }
}
