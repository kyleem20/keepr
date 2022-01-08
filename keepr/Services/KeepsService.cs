using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;
        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }
        internal Keep Create(Keep newKeep)
        {
            return _repo.Create(newKeep);
        }

        // FIXME Get all is not working properly
        internal object GetAll()
        {
            return _repo.GetAll();
        }

        internal Keep GetByKeepId(int id)
        {
            Keep keep = _repo.GetByKeepId(id);
            if (keep == null)
            {
                throw new Exception("Invalid keep Id");
            }
            return keep;
        }

        internal List<Keep> GetByCreatorId(string id)
        {
            return _repo.GetByCreatorId(id);
        }

        internal Keep Edit(Keep update)
        {
            Keep original = GetByKeepId(update.Id);
            original.Name = update.Name != null && update.Name.Trim().Length > 0 ? update.Name : original.Name;
            original.Description = update.Description != null && update.Description.Trim().Length > 0 ? update.Description : original.Description;
            original.Img = update.Img;
            _repo.Edit(original);
            return original;
        }

        internal void Delete(int id, string userId)
        {
            Keep remove = GetByKeepId(id);
            if (remove.CreatorId != userId)
            {
                throw new Exception("You cannot delete this Keep.");
            }
            _repo.Delete(id);
        }
    }
}