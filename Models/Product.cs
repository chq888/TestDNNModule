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

using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COM.Modules.TestDNNTestDNNModule.Models
{

    [TableName("TestDNNModule_Products")]
    [PrimaryKey("Id", AutoIncrement = true)]
    [Scope("ModuleId")]
    public class Product : Entity
    {

        ///<summary>
        /// The ModuleId of where the object was created and gets displayed
        ///</summary>
        public int ModuleId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        //[IgnoreColumn]
        //public IList<Color> Colors { get; set; }

        //[IgnoreColumn]
        //public IList<Category> Categories { get; set; }

        public Product()
        {
            //Colors = new List<Color>();
            //Categories = new List<Category>();
        }

    }


    [TableName("TestDNNModule_ProductCategories")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class ProductCategory : Entity
    {

        public int ProductId { get; set; }

        public int CategoryId { get; set; }

    }


    [TableName("TestDNNModule_ProductColors")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class ProductColor : Entity
    {

        public int ProductId { get; set; }

        public int ColorId { get; set; }

    }

}