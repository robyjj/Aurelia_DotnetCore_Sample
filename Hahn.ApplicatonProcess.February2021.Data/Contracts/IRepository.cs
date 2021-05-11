using Hahn.ApplicatonProcess.February2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data.Contracts
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAssets();
        Task<IEnumerable<T>> GetFilteredAssets(AssetParameters assetParameters);
        Task<T> GetAssetById(int id);
        Task CreateAsset(T asset);
        Task UpdateAsset(int AssetID,T asset);
        Task<T> DeleteAsset(int id);
        Task<List<T>> DeleteMultiple(int[] ids);
    }
}
