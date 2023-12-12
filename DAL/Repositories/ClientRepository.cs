using EFL.Data;
using EFL.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ClientRepository : BaseRepository<Client>
    {

        public ClientRepository(ClientsDbContext context):base(context) {}

        public IQueryable<Client> GetClients(int pageSize, int pageNumber, string? search, int? maritalStatusId, DateTime? birthDate)
        {
            var pageSizeParam = new SqlParameter("@PageSize", pageSize);
            var pageNumberParam = new SqlParameter("@PageNumber", pageNumber);
            var searchParam = new SqlParameter("@Search", search ?? (object)DBNull.Value);
            var maritalStatusIdParam = new SqlParameter("@MaritalStatusID", maritalStatusId ?? (object)DBNull.Value);
            var birthDateParam = new SqlParameter("@BirthDate", birthDate ?? (object)DBNull.Value);

            return _context.Clients.FromSqlRaw("EXEC SP_GetClients @PageSize, @PageNumber, @Search, @MaritalStatusID, @BirthDate",
                pageSizeParam, pageNumberParam, searchParam, maritalStatusIdParam, birthDateParam);

        }

    }
}
