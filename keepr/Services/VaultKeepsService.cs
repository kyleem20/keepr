using System;
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

        public VaultKeep Create(VaultKeep newVK)
        {
            VaultKeep inVault = _repo.GetVaultKeepIfExist(newVK.VaultId, newVK.KeepId, newVK.CreatorId);
            if (inVault != null)
            {
                throw new Exception("Already in this vault");
            }
            return _repo.Create(newVK);
        }
    }
}