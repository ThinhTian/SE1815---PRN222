using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IocPatternDemo.Model
{
    internal class XMLMovieReader : IMoviesReader
    {
        static string url = @"Data\";
        static XDocument films = XDocument.Load(url + "MoviesDB.xml");
        static List<Movies> movies = new List<Movies>();
        public List<Movies> ReadMovies()
        {
            var movieCollection =
            (from f in films.Descendants("Movie")
             select new Movies
             {
                 ID = f.Element("ID").Value,
                 Title = f.Element("Title").Value,
                 OscarNominations = f.Element("OscarNominations").Value,
                 OscarWins = f.Element("OscarWins").Value
             }).ToList();
            return movieCollection;
        }
    }
}
