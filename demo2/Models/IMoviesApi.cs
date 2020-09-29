using System;
using System.Threading.Tasks;
using Refit;

namespace demo2.Models
{
    public interface IMoviesApi
    {
        [Get("/")]
        Task<Movie> FindMovies([AliasAs("apikey")]string apikey,[AliasAs("i")]string id,[AliasAs("t")] string title,MovieType? type, [AliasAs("y")]int? year,PlotType? plot);
    }

    public enum PlotType
    {
        Short,
        full
    }
}
