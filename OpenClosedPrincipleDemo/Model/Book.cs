﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenClosedPrincipleDemo.Model
{
    class Book : IBook
    {
        public string Author { get; set; }
        public double Price { get; set; }
        public string Title { get; set; }
    }
}
