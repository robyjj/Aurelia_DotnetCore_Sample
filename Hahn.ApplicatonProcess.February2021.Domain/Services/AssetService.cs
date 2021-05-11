using Hahn.ApplicatonProcess.February2021.Data.Contracts;
using Hahn.ApplicatonProcess.February2021.Domain.Contracts;
using Hahn.ApplicatonProcess.February2021.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Domain.Services
{
    public class AssetService : IAssetService
    {
        private readonly IRepository<Asset> _assetRepository;

        public AssetService(IRepository<Asset>  assetRepository)
        {
            _assetRepository = assetRepository;            
        }

        public async Task CreateAsset(Asset asset)
        {
            await _assetRepository.CreateAsset(asset);
        }

        public async Task<Asset> DeleteAsset(int id)
        {
            return await _assetRepository.DeleteAsset(id);
        }

        public async Task<List<Asset>> DeleteMultiple(int[] ids)
        {
            return await _assetRepository.DeleteMultiple(ids);
        }

        public async Task<Asset> GetAssetById(int id)
        {
            return await _assetRepository.GetAssetById(id);
        }

        public async Task<IEnumerable<Asset>> GetAssets()
        {
            return await _assetRepository.GetAssets();
        }
        public async Task<IEnumerable<Asset>> GetFilteredAssets(AssetParameters assetParameters)
        {
            return await _assetRepository.GetFilteredAssets(assetParameters);
        }
        public async Task UpdateAsset(int assetID ,Asset asset)
        {
            await _assetRepository.UpdateAsset(assetID, asset);
        }
    }
}
