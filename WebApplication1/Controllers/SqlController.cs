using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Linq.Dynamic;

namespace WebApplication1.Controllers
{
    public class SqlController : Controller
    {
        // GET: Sql
        public ActionResult Index()
        {
            return View();
        }

        // GET: WebGrid?page=1&rowsPerPage=3&sort=OrderID&sortDir=ASC
        public ActionResult WebGrid(int page = 1, int rowsPerPage = 10, string sortCol = "ProductID", string sortDir = "ASC")
        {
            List<MyModel> res;
            int count;
            string sql;
            string dir;
            string[] columns = { "PRODUCTID", "PRODUCTNAME", "UNITPRICE", "UNITSINSTOCK", "UNITSONORDER", "CATEGORYNAME", "COMPANYNAME", "CONTACTNAME", "COUNTRY" };
            string[] sort = {"ASC", "DESC"};

            if (!(columns.Contains(sortCol.ToUpper())))
            {
                sortCol = "ProductID";
            }

            if (!(sort.Contains(sortDir.ToUpper())))
            {
                sortDir = "ASC";
            }

            ViewBag.sortCol = sortCol;

            if (sortDir == "ASC")
            {
                dir = "Ascending";
            } else
            {
                dir = "Descending";
            }

            using (var nwd = new NorthwindEntities())
            {
                if (sortCol == "CategoryName") {
                    sortCol = "Category.CategoryName";
                }

                if (sortCol == "CompanyName")
                {
                    sortCol = "Supplier.CompanyName";
                }

                if (sortCol == "ContactName")
                {
                    sortCol = "Supplier.ContactName";
                }

                if (sortCol == "Country")
                {
                    sortCol = "Supplier.Country";
                }

                var _res = nwd.Products
                    .OrderBy(sortCol + " " + sortDir + ", ProductID " + sortDir)
                    .Skip((page - 1) * rowsPerPage)
                    .Take(rowsPerPage)
                    .Select(o => new MyModel
                    {
                        ProductID = o.ProductID,
                        ProductName = o.ProductName,
                        UnitPrice = o.UnitPrice,
                        UnitsInStock = o.UnitsInStock,
                        UnitsOnOrder = o.UnitsOnOrder,
                        CategoryName = o.Category.CategoryName,
                        CompanyName = o.Supplier.CompanyName,
                        ContactName = o.Supplier.ContactName,
                        Country = o.Supplier.Country,
                    });

                sql = _res.ToString();
                res = _res.ToList();
                count = nwd.Products.Count();
            }

            ViewBag.sql = sql;
            ViewBag.dir = dir;
            ViewBag.rowsPerPage = rowsPerPage;
            ViewBag.count = count;
            return View(res);
        }
    }
}