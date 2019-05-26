using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace Myshop.DataAccess.InMemory
{ 
    public class ProductCatagoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCatagory> productCategies;

        public ProductCatagoryRepository()
        {
            productCategies = cache["productCategies"] as List<ProductCatagory>;
            if (productCategies == null)
            {
                productCategies = new List<ProductCatagory>();
            }

        }
        public void Commit()
        {
            cache["productCategies"] = productCategies;

        }
        public void Insert(ProductCatagory pc)
        {
            productCategies.Add(pc);
        }

        public void Update(ProductCatagory p, string Id)
        {
            ProductCatagory pcToUpdate = productCategies.Find(pc => pc.Id == Id);

            if(pcToUpdate != null)
            {
            pcToUpdate.Catagory = p.Catagory;

            Commit();
            }
            else
            {
                throw new Exception("produkt kategorie nicht grfunden");
            }



        }
        public ProductCatagory Find(string Id)
        {
            ProductCatagory pCatagory = productCategies.Find(pc => pc.Id == Id);

            if (pCatagory != null) {
                return pCatagory;
            }
            throw new Exception("Produkt kategorie nicht gefunden");
        }


        public IQueryable<ProductCatagory> Collection()
        {
            return productCategies.AsQueryable();
        }
        public void Delete(String Id)
        {
            ProductCatagory productCatagoryToDelete = productCategies.Find(pc => pc.Id == Id);
            if(productCatagoryToDelete != null)
            {
                productCategies.Remove(productCatagoryToDelete);
            }
        }
    }
}
