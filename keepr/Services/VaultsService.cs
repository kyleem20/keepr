using System;
using System.Collections.Generic;
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
        public Vault GetByVaultId(int id, string userId)
        {
            Vault vault = _repo.GetByVaultId(id);
            if (vault == null)
            {
                throw new Exception("Invalid Vault Id");
            }
            if (vault.IsPrivate == true && vault.CreatorId != userId)
            {
                throw new Exception("This Vault is private");
            }
            if (userId == null)
            {
                return vault;
            }
            return vault;

        }
        public Vault GetByVaultIdNoUser(int id)
        {
            Vault vault = _repo.GetByVaultId(id);
            if (vault == null)
            {
                throw new Exception("Invalid Vault Id");
            }
            if (vault.IsPrivate == true)
            {
                throw new Exception("This Vault is private");
            }

            return vault;

        }

        public List<Vault> GetByCreatorId(string id)
        {
            List<Vault> vaults = _repo.GetByCreatorId(id);
            vaults = vaults.FindAll(v => v.IsPrivate == false);
            return vaults;
        }
        public List<Vault> GetVaultsByAccount(string id)
        {
            return _repo.GetByCreatorId(id);
        }

        internal Vault Edit(Vault update, string userId)
        {
            Vault vault = GetByVaultId(update.Id, userId);
            if (vault.CreatorId != update.CreatorId)
            {
                throw new Exception("You cannot Edit another users vault");
            }
            vault.Name = update.Name != null && update.Name.Trim().Length > 0 ? update.Name : vault.Name;
            vault.Description = update.Description != null && update.Description.Trim().Length > 0 ? update.Description : vault.Description;
            vault.IsPrivate = update.IsPrivate;
            vault.Img = update.Img;
            _repo.Edit(vault);
            return vault;
        }

        public Vault IsVaultOwner(string userId, int id)
        {
            Vault vault = GetByVaultId(id, userId);
            if (vault.CreatorId != userId)
            {
                throw new Exception("You are not the owner of this vault");

            }
            return vault;
        }

        internal void Delete(int id, string userId)
        {
            Vault remove = GetByVaultId(id, userId);
            if (remove.CreatorId != userId)
            {
                throw new Exception("You cannot delete this vault.");
            }
            _repo.Delete(id);
        }
    }
}