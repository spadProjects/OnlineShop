using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Core.Models;

namespace OnlineShop.Infrastructure.Repositories
{
    public class ProductGroupsRepository : BaseRepository<ProductGroup, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public ProductGroupsRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public ProductGroup GetProductGroup(int id)
        {
            var pg = _context.ProductGroups.Include(p => p.ProductGroupBrands)
                .Include(p => p.ProductGroupFeatures).FirstOrDefault(p => p.Id == id);
            pg.ProductGroupBrands = pg.ProductGroupBrands.Where(b => b.IsDeleted == false).ToList();
            pg.ProductGroupFeatures = pg.ProductGroupFeatures.Where(b => b.IsDeleted == false).ToList();
            return pg;
        }
        public List<Feature> GetFeatures()
        {
            return _context.Features.Where(f => f.IsDeleted == false).ToList();
        }
        public List<Brand> GetBrands()
        {
            return _context.Brands.Where(f => f.IsDeleted == false).ToList();
        }
        public List<ProductGroup> GetProductGroups()
        {
            return _context.ProductGroups.Where(f => f.IsDeleted == false).Include(p => p.Children).ToList();
        }
        public ProductGroup AddNewProductGroup(int parentId, string title, List<int> brandIds, List<int> featureIds)
        {
            var productGroup = new ProductGroup();

            #region Adding Product Group
            productGroup.Title = title;
            if (parentId != 0)
                productGroup.ParentId = parentId;
            _context.ProductGroups.Add(productGroup);
            _context.SaveChanges();

            #endregion

            #region Adding Product Group Brands

            foreach (var brandId in brandIds)
            {
                var productGroupBrand = new ProductGroupBrand();
                productGroupBrand.ProductGroupId = productGroup.Id;
                productGroupBrand.BrandId = brandId;
                _context.ProductGroupBrands.Add(productGroupBrand);
            }
            _context.SaveChanges();

            #endregion
            #region Adding product Group Features
            foreach (var featureId in featureIds)
            {
                var productGroupFeature = new ProductGroupFeature();
                productGroupFeature.ProductGroupId = productGroup.Id;
                productGroupFeature.FeatureId = featureId;
                _context.ProductGroupFeatures.Add(productGroupFeature);
            }

            _context.SaveChanges();
            #endregion
            return productGroup;
        }
        public ProductGroup UpdateProductGroup(int parentId,int productGroupId, string title, List<int> brandIds, List<int> featureIds)
        {
            var productGroup = Get(productGroupId);

            #region Adding Product Group
            productGroup.Title = title;
            if (parentId != 0)
                productGroup.ParentId = parentId;
            else
                productGroup.ParentId = null;
            Update(productGroup);
            //_context.Entry(productGroup).State = EntityState.Modified;
            //_context.SaveChanges();

            #endregion

            #region Product Group Brands

            // Removing Previous Group Brands
            var productGroupBrands = _context.ProductGroupBrands
                .Where(b => b.IsDeleted == false & b.ProductGroupId == productGroup.Id).ToList(); 
            foreach (var item in productGroupBrands)
            {
                item.IsDeleted = true;
                _context.Entry(item).State = EntityState.Modified;
            }
            _context.SaveChanges();
            // Adding new Group Brands
            foreach (var brandId in brandIds)
            {
                var productGroupBrand = new ProductGroupBrand();
                productGroupBrand.ProductGroupId = productGroup.Id;
                productGroupBrand.BrandId = brandId;
                _context.ProductGroupBrands.Add(productGroupBrand);
            }
            _context.SaveChanges();

            #endregion
            #region product Group Features
            var productGroupFeatures = _context.ProductGroupFeatures
                .Where(b => b.IsDeleted == false & b.ProductGroupId == productGroup.Id).ToList();
            // Removing Previous Group Features
            foreach (var item in productGroupFeatures)
            {
                item.IsDeleted = true;
                _context.Entry(item).State = EntityState.Modified;
            }
            _context.SaveChanges();
            // Adding New Group Features
            foreach (var featureId in featureIds)
            {
                var productGroupFeature = new ProductGroupFeature();
                productGroupFeature.ProductGroupId = productGroup.Id;
                productGroupFeature.FeatureId = featureId;
                _context.ProductGroupFeatures.Add(productGroupFeature);
            }
            _context.SaveChanges();
            #endregion
            return productGroup;
        }
    }
}
