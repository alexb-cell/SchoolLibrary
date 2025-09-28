using DesignPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesignPattern.ViewModels
{
    public class CatalogViewModel
    {
        public List<Book> Books { get; set; }
        public List<TypeBook> TypesBook { get; set; }
    }
}