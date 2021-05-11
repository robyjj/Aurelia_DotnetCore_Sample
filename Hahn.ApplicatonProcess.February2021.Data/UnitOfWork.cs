using Hahn.ApplicatonProcess.February2021.Data.Contracts;
using Hahn.ApplicatonProcess.February2021.Models;

namespace Hahn.ApplicatonProcess.February2021.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Asset> Assets => throw new System.NotImplementedException();

        public void Commit()
        {
            throw new System.NotImplementedException();
        }
    }
}
