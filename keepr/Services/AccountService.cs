using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class AccountService
    {
        private readonly AccountsRepository _repo;
        private readonly VaultsRepository _vrepo;

        public AccountService(AccountsRepository repo, VaultsRepository vrepo)
        {
            _repo = repo;
            _vrepo = vrepo;
        }

        internal string GetProfileEmailById(string id)
        {
            return _repo.GetById(id).Email;
        }
        internal Account GetProfileByEmail(string email)
        {
            return _repo.GetByEmail(email);
        }
        internal Account GetOrCreateProfile(Account userInfo)
        {
            Account profile = _repo.GetById(userInfo.Id);
            if (profile == null)
            {
                return _repo.Create(userInfo);
            }
            return profile;
        }

        internal Account Edit(Account editData, string userEmail)
        {
            Account original = GetProfileByEmail(userEmail);
            original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
            original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
            return _repo.Edit(original);
        }

        public Profile GetByProfileId(string id)
        {
            Profile profile = _repo.GetByProfileId(id);
            if (profile == null)
            {
                throw new Exception("No profile with this id");
            }
            return profile;
        }

        internal List<Vault> GetMyVaults(string id)
        {
            return _vrepo.GetByCreatorId(id);
        }
    }
}