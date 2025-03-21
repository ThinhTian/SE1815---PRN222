﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenClosedPrincipleDemo.Model;
using System.Collections.Generic;
using System.IO;

namespace OpenClosedPrincipleDemo.Utilities
{
    
    internal class Utilities
    {
        static string ReadFile(string filename)
        {
            return File.ReadAllText(filename);
        }
        internal static List<Book> ReadData()
        {
            var cadJSON = ReadFile("Data/BookStore_01.json");
            return JsonConvert.DeserializeObject<List<Book>>(cadJSON);
        } 

         internal static List<Book> ReadDataExtra()
        {
            List<Book> books = ReadData();
            var cadJSON = ReadFile("Data/BookStore_02.json");
            books.AddRange(JsonConvert.DeserializeObject<List<Book>>(cadJSON));
            return books;
        }
    }
}
