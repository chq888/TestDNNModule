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
using COM.Modules.TestDNNTestDNNModule.Models;

namespace COM.Modules.TestDNNTestDNNModule.ViewModel
{

    public class ProductFormVM
    {

        ///<summary>
        /// The ModuleId of where the object was created and gets displayed
        ///</summary>
        public Product Product { get; set; }

        public IList<Category> Categories { get; set; }

        public IList<Category> SelectedCategories { get; set; }

        public IList<Color> Colors { get; set; }

        public ProductFormVM()
        {
            Colors = new List<Color>();
            Categories = new List<Category>();
            SelectedCategories= new List<Category>();
        }

    }

}