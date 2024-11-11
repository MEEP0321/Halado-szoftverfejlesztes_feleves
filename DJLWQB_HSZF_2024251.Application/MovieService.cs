using DJLWQB_HSZF_2024251.Model;
using DJLWQB_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJLWQB_HSZF_2024251.Application
{
    public interface IMovieService
    {
        Movie GetMovie(string id);

        IEnumerable<Movie> GetMovies();

        void AddMovie(Movie m);

        void RemoveMovie(string id);

        void ModifyMovie(string id, Movie m);
    }
    public class MovieService : IMovieService
    {
        private readonly IMovieDataProvider movieDataProvider;
        public MovieService(IMovieDataProvider movieDataProvider)
        {
            this.movieDataProvider = movieDataProvider;
        }

        public void AddMovie(Movie m)
        {
            if (IsMovieExist(m, out string id))
            {
                movieDataProvider.GetMovie(id).Pcs += m.Pcs;
            }
            else
            {
                movieDataProvider.AddMovie(m);
            }
        }

        public Movie GetMovie(string id)
        {
            return movieDataProvider.GetMovie(id);
        }

        public IEnumerable<Movie> GetMovies()
        {
            return movieDataProvider.GetMovies();
        }

        public void ModifyMovie(string id, Movie m)
        {
            movieDataProvider.ModifyMovie(id, m);
        }

        public void RemoveMovie(string id)
        {
            movieDataProvider.RemoveMovie(id);
        }

        public bool IsMovieExist(Movie m, out string id)
        {

            foreach (Movie item in GetMovies())
            {
                if (item.Equals(m))
                {
                    id = item.Id;
                    return true;
                }
            }
            id = string.Empty;
            return false;
        }
    }
}
