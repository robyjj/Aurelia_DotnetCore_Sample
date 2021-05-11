using Hahn.ApplicatonProcess.February2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Domain.Contracts
{
    public interface IAssetService
    {
        Task<IEnumerable<Asset>> GetAssets();
        Task<IEnumerable<Asset>> GetFilteredAssets(AssetParameters assetParameters);
        Task<Asset> GetAssetById(int id);
        Task CreateAsset(Asset asset);
        Task UpdateAsset(int AssetID,Asset asset);
        Task<Asset> DeleteAsset(int id);
        Task<List<Asset>> DeleteMultiple(int[] ids);
    }
}
