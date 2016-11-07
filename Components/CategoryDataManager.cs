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

using System.Linq;
using System.Collections.Generic;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using COM.Modules.TestDNNTestDNNModule.Models;

namespace COM.Modules.TestDNNTestDNNModule.Components
{
    public class CategoryDataManager : ServiceLocator<IDataManager<Category>, CategoryDataManager>, IDataManager<Category>
    {

        public Category GetNewItem()
        {
            Category p = null;
            using (var ctx = DataContext.Instance())
            {
                var repo = ctx.GetRepository<Category>();
                p = repo.Find("ORDER BY ID DESC").FirstOrDefault();
            }

            return p;
        }

        public void Create(Category t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Category>();
                rep.Insert(t);
            }
        }

        public void Delete(int itemId, int moduleId)
        {
            var t = Get(itemId, moduleId);
            Delete(t);
        }

        public void Delete(Category t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Category>();
                rep.Delete(t);
            }
        }

        public IList<Category> GetAll()
        {
            IList<Category> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Category>();
                t = rep.Get().ToList();
            }
            return t;
        }

        public IList<Category> GetByModuleId(int moduleId)
        {
            IList<Category> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Category>();
                t = rep.Get(moduleId).ToList();
            }
            return t;
        }

        public Category Get(int itemId, int moduleId)
        {
            Category t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Category>();
                t = rep.GetById(itemId, moduleId);
            }
            return t;
        }

        public Category Get(int itemId)
        {
            Category t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Category>();
                t = rep.GetById(itemId);
            }
            return t;
        }

        public void Update(Category t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Category>();
                rep.Update(t);
            }
        }

        protected override System.Func<IDataManager<Category>> GetFactory()
        {
            return () => new CategoryDataManager();
        }

    }

}
