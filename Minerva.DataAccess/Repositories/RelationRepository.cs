using Minerva.DataModels.Relations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Minerva.DataAccess.Repositories
{
    public class RelationRepository : IRelationRepository
    {
        private readonly IDbConnection _connection;

        public RelationRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public Task<Relation> AddAsync(Relation relation)
        {
            throw new NotImplementedException();
        }

        public async Task<Relation?> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM Relations WHERE Id = @Id";
            var entity = await _connection.QuerySingleOrDefaultAsync<Relation>(sql, new { Id = id });
            return await Task.FromResult(entity);
        }

    }
}