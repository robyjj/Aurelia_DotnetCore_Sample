using Hahn.ApplicatonProcess.February2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<Asset> Assets { get; }
        void Commit();
    }
}
