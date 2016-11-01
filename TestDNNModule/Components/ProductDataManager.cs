/*
' Copyright (c) 2016 cuonghq612
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System.Collections;
using System.Linq;
using System.Collections.Generic;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using COM.Modules.TestDNNTestDNNModule.Models;
using COM.Modules.TestDNNTestDNNModule.ViewModel;

namespace COM.Modules.TestDNNTestDNNModule.Components
{

    public interface IProductDataManager : IDataManager<Product>
    {

        void CreateProductCategory(ProductCategory p);
        void CreateProductColor(ProductColor p);
        void DeleteProductCategory(ProductCategory p);
        void DeleteProduct(int id);
        ProductFormVM GetProduct(int id);
        IList<Product> SearchProduct(ProductSearchVM vm);

    }

    /// <summary>
    /// TODO: check sql injection, include entities
    /// </summary>
    public class ProductDataManager : ServiceLocator<IProductDataManager, ProductDataManager>, IProductDataManager
    {

        public IList<Product> SearchProduct(ProductSearchVM vm)
        {
            IList<Product> list = new List<Product>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var repProduct = ctx.GetRepository<Product>();
                if (vm.CategoryId <= 0 && string.IsNullOrWhiteSpace(vm.Code))
                {
                    list = repProduct.Get().ToList();
                }
                else
                {
                    if (vm.CategoryId > 0)
                    {
                        var repProductCategory = ctx.GetRepository<ProductCategory>();
                        var productCategoryList = repProductCategory.Find("WHERE CATEGORYID = " + vm.CategoryId).ToList();
                        if (productCategoryList != null && productCategoryList.Count > 0)
                        {
                            string whereProductCategoryList = string.Join(",", productCategoryList.Select(a => a.ProductId).ToArray());
                            string condition = "WHERE ID in (" + whereProductCategoryList + ")";
                            if (!string.IsNullOrWhiteSpace(vm.Code))
                            {
                                condition += " and CODE LIKE '%" + vm.Code + "%'";
                            }

                            list = repProduct.Find(condition).ToList();
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(vm.Code))
                        {
                            list = repProduct.Find("WHERE CODE LIKE '%" + vm.Code + "%'").ToList();
                        }
                    }
                }
            }

            return list;
        }

        public void DeleteProductCategory(ProductCategory p)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var repProductCategory = ctx.GetRepository<ProductCategory>();
                repProductCategory.Delete("WHERE PRODUCTID = " + p.ProductId + " and CATEGORYID = " + p.CategoryId);
            }
        }

        public ProductFormVM GetProduct(int id)
        {
            ProductFormVM vm = new ProductFormVM();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Product>();
                vm.Product = rep.GetById(id);

                var repCategory = ctx.GetRepository<Category>();
                vm.Categories = repCategory.Get().ToList();
                var repProductCategory = ctx.GetRepository<ProductCategory>();
                IList<ProductCategory> productCategoryList = repProductCategory.Find("WHERE PRODUCTID = " + id).ToList();
                if (productCategoryList != null && productCategoryList.Count > 0)
                {
                    string whereProductCategoryList = string.Join(",", productCategoryList.Select(a => a.CategoryId).ToArray());
                    vm.SelectedCategories = ctx.GetRepository<Category>().Find("WHERE ID in (" + whereProductCategoryList + ")").ToList();
                }

                var repProductColor = ctx.GetRepository<ProductColor>();
                IList<ProductColor> productColorList = repProductColor.Find("WHERE PRODUCTID = " + id).ToList();
                if (productColorList != null && productColorList.Count > 0)
                {
                    string whereProductColorList = string.Join(",", productColorList.Select(a => a.ColorId).ToArray());
                    vm.Colors = ctx.GetRepository<Color>().Find("WHERE ID in (" + whereProductColorList + ")").ToList();
                }

            }

            return vm;
        }

        public void DeleteProduct(int id)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var repProductCategory = ctx.GetRepository<ProductCategory>();
                repProductCategory.Delete("WHERE PRODUCTID = " + id);

                var repProductColor = ctx.GetRepository<ProductColor>();
                repProductColor.Delete("WHERE PRODUCTID = " + id);

                var repProduct = ctx.GetRepository<Product>();
                repProduct.Delete("WHERE ID = " + id);
            }
        }

        public Product GetNewItem()
        {
            Product p = null;
            using (var ctx = DataContext.Instance())
            {
                var repo = ctx.GetRepository<Product>();
                p = repo.Find("ORDER BY ID DESC").FirstOrDefault();
            }

            return p;
        }

        public IEnumerable<T> GetObjects<T>(string sqlConditon, params object[] args) where T : class
        {
            IEnumerable<T> infos = null;

            using (var ctx = DataContext.Instance())
            {
                var repo = ctx.GetRepository<T>();
                infos = repo.Find(sqlConditon, args);
            }

            return infos;
        }

        public IEnumerable<T> GetObjects<T>(System.Data.CommandType cmdType, string sql, params object[] args) where T : class
        {
            IEnumerable<T> infos = null;

            using (var ctx = DataContext.Instance())
            {
                infos = ctx.ExecuteQuery<T>(cmdType, sql, args);
            }

            return infos;
        }

        public void CreateProductCategory(ProductCategory t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<ProductCategory>();
                rep.Insert(t);
            }
        }

        public void CreateProductColor(ProductColor t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<ProductColor>();
                rep.Insert(t);
            }
        }

        public void Create(Product t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Product>();
                rep.Insert(t);
            }
        }

        public void Delete(int itemId, int moduleId)
        {
            var t = Get(itemId, moduleId);
            Delete(t);
        }

        public void Delete(Product t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Product>();
                rep.Delete(t);
            }
        }

        public IList<Product> GetAll()
        {
            IList<Product> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Product>();
                t = rep.Get().ToList();
            }
            return t;
        }

        public IList<Product> GetByModuleId(int moduleId)
        {
            IList<Product> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Product>();
                t = rep.Get(moduleId).ToList();
            }
            return t;
        }

        public Product Get(int itemId, int moduleId)
        {
            Product t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Product>();
                t = rep.GetById(itemId, moduleId);
            }
            return t;
        }

        public Product Get(int itemId)
        {
            Product t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Product>();
                t = rep.GetById(itemId);

            }
            return t;
        }

        public void Update(Product t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Product>();
                rep.Update(t);
            }
        }

        protected override System.Func<IProductDataManager> GetFactory()
        {
            return () => new ProductDataManager();
        }

    }

}
