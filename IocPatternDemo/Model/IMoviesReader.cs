using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocPatternDemo.Model
{
    public interface IMoviesReader
    {
        List<Movies> ReadMovies();
    }
}
