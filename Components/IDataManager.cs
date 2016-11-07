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

using DotNetNuke.Collections;
using DotNetNuke.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COM.Modules.TestDNNTestDNNModule.Models;

namespace COM.Modules.TestDNNTestDNNModule.Components
{

    public interface IDataManager<TEntity> where TEntity : Entity
    {

        void Create(TEntity t);
        void Delete(int itemId, int moduleId);
        void Delete(TEntity t);
        IList<TEntity> GetAll();
        IList<TEntity> GetByModuleId(int moduleId);
        TEntity Get(int itemId);
        TEntity Get(int itemId, int moduleId);
        TEntity GetNewItem();
        void Update(TEntity t);

    }

}