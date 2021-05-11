using Hahn.ApplicatonProcess.February2021.Data.Contracts;
using Hahn.ApplicatonProcess.February2021.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data.Repositories
{
    public class AssetRepository : IRepository<Asset>,IDisposable
    {
        private readonly AssetContext _assetContext;
        public AssetRepository(AssetContext assetContext)
        {            
            _assetContext = assetContext;
            _assetContext.Database.EnsureCreated();
        }
        public async Task CreateAsset(Asset asset)
        {
            _assetContext.Assets.Add(asset);
            await _assetContext.SaveChangesAsync();
        }

        public async Task<Asset> DeleteAsset(int id)
        {
            var asset = await _assetContext.Assets.FindAsync(id);
            if(asset == null)
            {
                return null;
            }
            _assetContext.Assets.Remove(asset);
            await _assetContext.SaveChangesAsync();
            return asset;
        }

        public async Task<List<Asset>> DeleteMultiple(int[] assetsIds)
        {
            var assets = new List<Asset>();
            foreach (var id in assetsIds)
            {
                var asset = await _assetContext.Assets.FindAsync(id);
                if (asset != null)
                {
                    assets.Add(asset);
                }
            }
            if (!assets.Any())
            {
                return null;
            }
            _assetContext.Assets.RemoveRange(assets);
            await _assetContext.SaveChangesAsync();
            return assets;
        }

        public async Task<Asset> GetAssetById(int id)
        {
            return await _assetContext.Assets.FindAsync(id);
        }

        public async Task<IEnumerable<Asset>> GetAssets()
        {            
            return await _assetContext.Assets.ToArrayAsync();
        }
        public async Task<IEnumerable<Asset>> GetFilteredAssets(AssetParameters assetParameters)
        {
            IQueryable<Asset> assets = _assetContext.Assets;

            //For now just filtering based on Asset Name
            if(!string.IsNullOrEmpty(assetParameters.AssetName))
            {
                assets = assets.Where(
                    a => a.AssetName.ToLower().Contains(assetParameters.AssetName.ToLower())
                    );
            }

            //Sort By Logic

            if (!string.IsNullOrEmpty(assetParameters.SortBy))
            {
                if(typeof(Asset).GetProperty(assetParameters.SortBy) != null)
                {
                    assets = assets.OrderByCustom(assetParameters.SortBy, assetParameters.SortOrder);
                }
            }

            assets = assets.Skip(assetParameters.Size * (assetParameters.Page - 1))
                .Take(assetParameters.Size);
            return await assets.ToArrayAsync();
        }
        public async Task UpdateAsset(int assetID, Asset asset)
        {
            _assetContext.Entry(asset).State = EntityState.Modified;
            await _assetContext.SaveChangesAsync(); 
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _assetContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
