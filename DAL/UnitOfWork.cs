using DAL.Interfaces;
using DAL.Repositories;
using EFL.Data;
using EFL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly ClientsDbContext _context;
    public ClientRepository ClientRepository { get; private set; }
    public BaseRepository<MaritalStatus> MSRepository { get; private set; }

    public UnitOfWork(ClientsDbContext context)
    {
        _context = context;
        ClientRepository = new ClientRepository(_context);
        MSRepository = new BaseRepository<MaritalStatus>(_context);
    }
    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
