// Create, Get(All VaultKeep), Delete
using System.Data;
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
    }
}