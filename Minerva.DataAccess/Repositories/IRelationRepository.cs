using Minerva.DataModels.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minerva.DataAccess.Repositories
{
    public interface IRelationRepository
    {
        Task<Relation?> GetByIdAsync(int id);
        Task<Relation> AddAsync(Relation relation);
    }
}
