// Create, Get(All VaultKeep), Delete
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;
        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }
        internal VaultKeep Create(VaultKeep newVK)
        {
            string sql = @"
            Insert INTO vaultKeeps
            (vaultId, keepId, creatorId)
            Values
            (@VaultId, @KeepId, @CreatorId);
            SELECT LAST_INSERT_ID()
        ;";
            int id = _db.ExecuteScalar<int>(sql, newVK);
            newVK.Id = id;
            return newVK;
        }

        internal VaultKeep GetVaultKeepIfExist(int vaultId, int keepId, string userId)
        {
            string sql = "SELECT * FROM vaultKeeps Where vaultId = @vaultId AND keepId = @keepId AND creatorId = @userId;";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { vaultId, keepId, userId });
        }

        internal VaultKeep GetByVaultKeepId(int id)
        {
            string sql = @"Select
            vk.*,
            a.*
            From vaultKeeps vk
            Join accounts a ON vk.creatorId = a.id
            Where vk.id = @id
            ;";
            return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultKeep, profile) =>
            {
                vaultKeep.Creator = profile;
                return vaultKeep;
            }, new { id }).FirstOrDefault();
        }

        internal void Delete(int id)
        {
            string sql = @"DELETE FROM vaultKeeps WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
        internal List<Vault> GetByVaultId(int id)
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
        // TODO fix failed tests in postman
        internal List<VaultKeepsViewModel> GetKeepsByVaultId(int id)
        {
            string sql = @"
            Select
                k.*,
                vk.id as VaultKeepId,
                a.*
            From vaultKeeps vk
            Join keeps k ON k.id = vk.keepId
            Join accounts a ON k.creatorId = a.id
            Where vk.vaultId = @id
            ;";
            return _db.Query<VaultKeepsViewModel, Profile, VaultKeepsViewModel>(sql, (vaultKeep, profile) =>
            {
                vaultKeep.Creator = profile;
                return vaultKeep;
            }, new { id }).ToList();

        }
    }
}