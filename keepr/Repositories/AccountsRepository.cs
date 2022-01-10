using System.Data;
using keepr.Models;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace keepr.Repositories
{
    public class AccountsRepository
    {
        private readonly IDbConnection _db;

        public AccountsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Account GetByEmail(string userEmail)
        {
            string sql = "SELECT * FROM accounts WHERE email = @userEmail";
            return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
        }

        internal Account GetById(string id)
        {
            string sql = "SELECT * FROM accounts WHERE id = @id";
            return _db.QueryFirstOrDefault<Account>(sql, new { id });
        }
        public Profile GetByProfileId(string id)
        {
            var sql = "SELECT * FROM accounts a WHERE a.id = @id;";
            return _db.Query<Profile>(sql, new { id }).FirstOrDefault();
        }
        internal Account Create(Account newAccount)
        {
            string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
            _db.Execute(sql, newAccount);
            return newAccount;
        }

        internal Account Edit(Account update)
        {
            string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
            _db.Execute(sql, update);
            return update;
        }

        internal List<Account> GetMyVaults(string id)
        {
            string sql = @"
            SELECT
                a.*,
                v.*
            FROM accounts a
            JOIN vaults v ON v.creatorId = a.id
            Where v.creatorId = @id ;";
            return _db.Query<Account, Vault, Account>(sql, (account, vault) =>
         {
             account = (Account)vault.Creator;
             return account;
         }, new { id }).ToList();
        }
    }
}
