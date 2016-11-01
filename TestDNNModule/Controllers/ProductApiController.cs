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

using DotNetNuke.Services.Exceptions;
using DotNetNuke.Web.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using COM.Modules.TestDNNTestDNNModule.Components;
using COM.Modules.TestDNNTestDNNModule.Models;
using COM.Modules.TestDNNTestDNNModule.ViewModel;

namespace COM.Modules.TestDNNTestDNNModule.Controllers
{

    /// <summary>
    /// /testdnn/desktopmodules/services/API/ProductApi/GET
    /// </summary>
    public class ProductApiController : ApiControllerBase
    {

        public HttpResponseMessage Search(ProductSearchVM vm)
        {
            var list = ProductDataManager.Instance.SearchProduct(vm);
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        public HttpResponseMessage Get()
        {
            var items = ProductDataManager.Instance.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage Get(int id)
        {
            var vm = new ProductFormVM();
            if (id > 0)
            {
                vm = ProductDataManager.Instance.GetProduct(id);
            }

            return Request.CreateResponse(HttpStatusCode.OK, vm);
        }

        [DnnAuthorize]
        public HttpResponseMessage Post(ProductFormVM vm)
        {
            if (vm.Product.Id > 0)
            {
                ProductDataManager.Instance.Update(vm.Product);
                foreach (var cate in vm.SelectedCategories)
                {
                    if (cate.Id <= 0 || cate.IsDirty == null)
                    {
                        // add new
                        ProductDataManager.Instance.CreateProductCategory(new ProductCategory()
                        {
                            ProductId = vm.Product.Id,
                            CategoryId = cate.Id
                        });
                    }
                    else if (cate.IsDirty != null && cate.IsDirty.Value)
                    {
                        // remove uncheck
                        ProductDataManager.Instance.DeleteProductCategory(new ProductCategory()
                        {
                            ProductId = vm.Product.Id,
                            CategoryId = cate.Id
                        });
                    }
                }

                foreach (var col in vm.Colors)
                {
                    if (col.Id <= 0)
                    {
                        ColorDataManager.Instance.Create(col);
                        var newColor = ColorDataManager.Instance.GetNewItem();
                        ProductDataManager.Instance.CreateProductColor(new ProductColor()
                        {
                            ProductId = vm.Product.Id,
                            ColorId = newColor.Id
                        });
                    }
                    else
                    {
                        ColorDataManager.Instance.Update(col);
                    }
                }
            }
            else
            {
                ProductDataManager.Instance.Create(vm.Product);
                var newProduct = ProductDataManager.Instance.GetNewItem();
                foreach (var cate in vm.SelectedCategories)
                {
                    //CategoryDataManager.Instance.Create(cate);
                    //var newCategory = CategoryDataManager.Instance.GetNewItem();
                    ProductDataManager.Instance.CreateProductCategory(new ProductCategory()
                    {
                        ProductId = newProduct.Id,
                        CategoryId = cate.Id
                    });
                }

                foreach (var col in vm.Colors)
                {
                    ColorDataManager.Instance.Create(col);
                    var newColor = ColorDataManager.Instance.GetNewItem();
                    ProductDataManager.Instance.CreateProductColor(new ProductColor()
                    {
                        ProductId = newProduct.Id,
                        ColorId = newColor.Id
                    });
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [DnnAuthorize]
        public HttpResponseMessage Put(ProductFormVM vm)
        {
            // same Post
            if (vm.Product.Id > 0)
            {
                ProductDataManager.Instance.Update(vm.Product);
                foreach (var cate in vm.SelectedCategories)
                {
                    if (cate.Id <= 0 || cate.IsDirty == null)
                    {
                        // add new
                        ProductDataManager.Instance.CreateProductCategory(new ProductCategory()
                        {
                            ProductId = vm.Product.Id,
                            CategoryId = cate.Id
                        });
                    }
                    else if (cate.IsDirty != null && cate.IsDirty.Value)
                    {
                        // remove uncheck
                        ProductDataManager.Instance.DeleteProductCategory(new ProductCategory()
                        {
                            ProductId = vm.Product.Id,
                            CategoryId = cate.Id
                        });
                    }
                }

                foreach (var col in vm.Colors)
                {
                    if (col.Id <= 0)
                    {
                        ColorDataManager.Instance.Create(col);
                        var newColor = ColorDataManager.Instance.GetNewItem();
                        ProductDataManager.Instance.CreateProductColor(new ProductColor()
                        {
                            ProductId = vm.Product.Id,
                            ColorId = newColor.Id
                        });
                    }
                    else
                    {
                        ColorDataManager.Instance.Update(col);
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [DnnAuthorize]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            ProductDataManager.Instance.DeleteProduct(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }

}