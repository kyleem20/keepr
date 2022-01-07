using System;
using keepr.Models;
using keepr.Repositories;



namespace keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;
        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }
        internal Vault Create(Vault newVault)
        {
            return _repo.Create(newVault);
        }
        public Vault GetByVaultId(int id)
        {
            Vault vault = _repo.GetByVaultId(id);
            if (vault == null)
            {
                throw new Exception("Invalid Vault Id");
            }
            return vault;

        }
        internal Vault Edit(Vault update)
        {
            Vault original = GetByVaultId(update.Id);
            original.Name = update.Name != null && update.Name.Trim().Length > 0 ? update.Name : original.Name;
            original.Description = update.Description != null && update.Description.Trim().Length > 0 ? update.Description : original.Description;
            original.IsPrivate = update.IsPrivate;
            _repo.Edit(original);
            return original;
        }

        internal void Delete(int id, string userId)
        {
            Vault remove = GetByVaultId(id);
            if (remove.CreatorId != userId)
            {
                throw new Exception("You cannot delete this vault.");
            }
            _repo.Delete(id);
        }
    }
}