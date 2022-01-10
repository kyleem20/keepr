using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsService _vs;
        private readonly KeepsService _ks;

        public VaultKeepsService(VaultKeepsRepository repo, VaultsService vs, KeepsService ks)
        {
            _repo = repo;
            _vs = vs;
            _ks = ks;
        }

        internal VaultKeep Create(VaultKeep newVK, Account userInfo)
        {
            VaultKeep inVault = _repo.GetVaultKeepIfExist(newVK.VaultId, newVK.KeepId, newVK.CreatorId);
            var vault = _vs.IsVaultOwner(userInfo.Id, newVK.VaultId);
            if (inVault != null)
            {
                throw new Exception("Already in this vault");
            }

            return _repo.Create(newVK);
        }

        internal VaultKeep GetByVaultKeepId(int id)
        {
            VaultKeep vaultKeep = _repo.GetByVaultKeepId(id);
            if (vaultKeep == null)
            {
                throw new Exception("Invalid keep Id");
            }
            return vaultKeep;
        }

        internal void Delete(int id, string userId)
        {
            VaultKeep toDelete = GetByVaultKeepId(id);
            if (toDelete.CreatorId != userId)
            {
                throw new Exception("You cannot delete another users vault");
            }
            _repo.Delete(id);
        }
        // TODO allow users not logged in to get vaults and keeps, should be fixed through vaults controller
        internal List<VaultKeep> GetKeepsByVaultId(int id, string userId)
        {
            Vault vault = _vs.GetByVaultId(id, userId);
            if (vault.IsPrivate == true)
            {
                throw new Exception("This is a private vault");
            }
            List<VaultKeep> vksi = _repo.GetByVaultId(id);
            return vksi;
        }
    }
}