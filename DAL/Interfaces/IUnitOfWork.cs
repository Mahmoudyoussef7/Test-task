using DAL.Repositories;
using EFL.Entities;

namespace DAL.Interfaces;

public interface IUnitOfWork:IDisposable
{
    ClientRepository ClientRepository { get; }
    BaseRepository<MaritalStatus> MSRepository { get; }

    int Complete();
}
