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

using DotNetNuke.Web.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using COM.Modules.TestDNNTestDNNModule.Components;
using COM.Modules.TestDNNTestDNNModule.Models;

namespace COM.Modules.TestDNNTestDNNModule
{

    public class RouterMapper : IServiceRouteMapper
    {

        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute(
                moduleFolderName: "Services",
                routeName: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                namespaces: new[] { "COM.Modules.TestDNNTestDNNModule.Controllers" }
            );

            //mapRouteManager.MapHttpRoute("MyModuleName.WebAPI", "default", "{controller}/{action}", new[] { "MyModuleName.WebAPI" });

            //TODO find out where start-up app to init data, it's ok now
            var cates = CategoryDataManager.Instance.GetAll();
            if (cates == null || !cates.Any())
            {
                CategoryDataManager.Instance.Create(new Category() { Name = "Cate A" });
                CategoryDataManager.Instance.Create(new Category() { Name = "Cate B" });
                CategoryDataManager.Instance.Create(new Category() { Name = "Cate C" });
                CategoryDataManager.Instance.Create(new Category() { Name = "Cate D" });
            }
            
        }

    }

}