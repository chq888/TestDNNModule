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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using COM.Modules.TestDNNTestDNNModule.Components;
using COM.Modules.TestDNNTestDNNModule.Models;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Web.Mvc.Common;
using DotNetNuke.Web.Mvc.Routing;
using COM.Modules.TestDNNTestDNNModule.ViewModel;

namespace COM.Modules.TestDNNTestDNNModule.Controllers
{

    [DnnHandleError]
    public class ProductController : DnnController
    {

        private int _moduleId = 0;

        public int CurrentModuleId
        {
            get { return ModuleContext?.ModuleId ?? _moduleId; }
            set { _moduleId = value; }
        }

        public new ActionResult RedirectToAction(string actionName, string controllerName, object routeValues)
        {
            var routeVals = TypeHelper.ObjectToDictionary(routeValues);
            routeVals["controller"] = controllerName;
            routeVals["action"] = actionName;
            return Redirect(ModuleRoutingProvider.Instance().GenerateUrl(routeVals, ModuleContext));
        }

        [ModuleAction(ControlKey = "Edit", TitleKey = "AddItem")]
        public ActionResult Index()
        {
            //var items = ProductDataManager.Instance.Get(ModuleContext.ModuleId);
            return View(new List<Product>());
        }

        public ActionResult Edit(int itemId = -1)
        {
            ProductFormVM vm = new ProductFormVM();
            vm.Product = new Product() {Id = itemId, ModuleId = CurrentModuleId};
            return View(vm);
        }

    }

}