using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }
        public Vault Create(Vault newVault)
        {
            string sql = @"
            INSERT INTO vaults
            (name, description, isPrivate, img, creatorId)
            VALUES
            (@Name, @Description, @IsPrivate, @Img, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newVault);
            newVault.Id = id;
            return newVault;
        }

        internal Vault GetByVaultId(int id)
        {
            string sql = @"Select
            v.*,
            a.*
            From vaults v
            Join accounts a ON v.creatorId = a.id
            Where v.id = @id
            ;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { id }).FirstOrDefault();
        }

        internal Vault GetByVaultIdNoUser(int id)
        {
            string sql = @"
            Select 
            v.*,
            a.*
            From vaults v 
            Join accounts a On v.creatorId = a.id 
            Where id = @id;";
            return _db.QueryFirstOrDefault<Vault>(sql, new { id });
        }

        internal List<Vault> GetVaultsByAccount(string id)
        {
            string sql = @"
            SELECT
                v.*,
                a.*
            FROM accounts a
            JOIN vaults v ON v.creatorId = a.id
            WHERE v.creatorId = @id;";
            return _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
            {
                vault.Creator = account;
                return vault;
            }, new { id }).ToList();
        }

        // internal List<Vault> GetByAccount()
        // {
        //     string sql = @"
        //     SELECT
        //         v.*,
        //         a.*
        //     FROM vaults v
        //     JOIN accounts a ON v.creatorId = a.id
        //     WHERE v.creatorId = @id;";
        //     return _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        //     {
        //         vault.Creator = account;
        //         return vault;
        //     }, new { id }).ToList();
        // }

        internal List<Vault> GetByCreatorId(string id)
        {
            string sql = @"
            SELECT
                v.*,
                a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.creatorId = @id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { id }).ToList();
        }

        internal void Edit(Vault original)
        {
            string sql = @"
            Update vaults
            Set
                name = @Name,
                description = @Description,
                isPrivate = @IsPrivate,
                img = @Img
            Where id = @Id
            ;";
            _db.Execute(sql, original);
        }

        internal void Delete(int id)
        {
            string sql = "Delete FROM vaults Where id = @id Limit 1";
            _db.Execute(sql, new { id });
        }

    }
}