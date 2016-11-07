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

    public class ColorDataManager : ServiceLocator<IDataManager<Color>, ColorDataManager>, IDataManager<Color>
    {

        public Color GetNewItem()
        {
            Color p = null;
            using (var ctx = DataContext.Instance())
            {
                var repo = ctx.GetRepository<Color>();
                p = repo.Find("ORDER BY ID DESC").FirstOrDefault();
            }

            return p;
        }

        public void Create(Color t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Color>();
                rep.Insert(t);
            }
        }

        public void Delete(int itemId, int moduleId)
        {
            var t = Get(itemId, moduleId);
            Delete(t);
        }

        public void Delete(Color t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Color>();
                rep.Delete(t);
            }
        }

        public IList<Color> GetAll()
        {
            IList<Color> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Color>();
                t = rep.Get().ToList();
            }
            return t;
        }

        public IList<Color> GetByModuleId(int moduleId)
        {
            IList<Color> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Color>();
                t = rep.Get(moduleId).ToList();
            }
            return t;
        }

        public Color Get(int itemId, int moduleId)
        {
            Color t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Color>();
                t = rep.GetById(itemId, moduleId);
            }
            return t;
        }
        public Color Get(int itemId)
        {
            Color t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Color>();
                t = rep.GetById(itemId);
            }
            return t;
        }

        public void Update(Color t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Color>();
                rep.Update(t);
            }
        }

        protected override System.Func<IDataManager<Color>> GetFactory()
        {
            return () => new ColorDataManager();
        }

    }

}
