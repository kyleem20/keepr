using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;
// Create, Get(all keeps), Get(Keep by Id), Edit, Delete
namespace keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;
        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }
        public Keep Create(Keep newKeep)
        {
            string sql = @"
            INSERT INTO keeps
            (name, description, img, creatorId)
            VALUES
            (@Name, @Description, @Img, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newKeep);
            newKeep.Id = id;
            return newKeep;
        }

        // FIXME Get all is not working properly
        public List<Keep> GetAll()
        {
            string sql = @"
            Select
                k.*,
                a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id 
            ;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }).ToList();
        }

        internal Keep GetByKeepId(int id)
        {
            string sql = @"Select
            k.*,
            a.*
            From keeps k
            Join accounts a ON k.creatorId = a.id
            Where k.id = @id
            ;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }).FirstOrDefault();
        }
        public Keep GetByKeepAddView(int id)
        {
            string sql = @"
                SELECT
                k.*,
                a.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId
            WHERE k.id = @id;
            UPDATE keeps
            SET views = + 1
            WHERE id = @id;
            ";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }).FirstOrDefault();
        }

        internal List<Keep> GetByCreatorId(string id)
        {
            string sql = @"
            SELECT
                k.*,
                a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.creatorId = @id;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
         {
             keep.Creator = profile;
             return keep;
         }, new { id }).ToList();
        }



        internal void Edit(Keep original)
        {
            string sql = @"
            Update keeps
            Set
                name = @Name,
                description = @Description,
                img = @Img
            Where id = @Id
            ;";
            _db.Execute(sql, original);
        }

        internal void Delete(int id)
        {
            string sql = "Delete FROM keeps Where id = @id Limit 1";
            _db.Execute(sql, new { id });
        }
    }
}