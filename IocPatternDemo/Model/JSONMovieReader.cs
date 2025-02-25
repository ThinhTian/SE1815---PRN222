using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IocPatternDemo.Model
{
    internal class JSONMovieReader : IMoviesReader
    {
        static string file = @"Data/MoviesDB.json";
        static List<Movies> movies = new List<Movies>();
        public List<Movies> ReadMovies() {
            var moviesText = File.ReadAllText(file);
            return JsonSerializer.Deserialize<List<Movies>>(moviesText);
        }
    }
}
