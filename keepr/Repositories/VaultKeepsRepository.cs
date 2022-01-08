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
        internal List<VaultKeep> GetByVaultId(int id)
        {
            string sql = @"
            Select
                a.*,
                vk.id AS VaultKeepId
            From vaultKeeps vk
            Join accounts a ON a.id = vk.creatorId
            Where vk.vaultId = @id
            ;";
            return _db.Query<VaultKeep>(sql, new { id }).ToList();

        }
    }
}