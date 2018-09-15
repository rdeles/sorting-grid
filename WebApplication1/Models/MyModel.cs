using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class MyModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get ; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public string CategoryName { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Country { get; set; }
    }

    // Adapted from https://forums.asp.net/t/1853996.aspx?WebGrid+Sort+Arrows

    public static class SortArrow
    {
        public static MvcHtmlString SortDirection(ref WebGrid grid, String sortCol) 
        {
            String html = "";

            if (grid.SortColumn == sortCol && grid.SortDirection == System.Web.Helpers.SortDirection.Ascending)
            {
                html = "▼";
            }
            else if (grid.SortColumn == sortCol && grid.SortDirection == System.Web.Helpers.SortDirection.Descending)
            {
                html = "▲";
            }
            else
            {
                html = "";
            }

            return MvcHtmlString.Create(html);
        }
    }
}