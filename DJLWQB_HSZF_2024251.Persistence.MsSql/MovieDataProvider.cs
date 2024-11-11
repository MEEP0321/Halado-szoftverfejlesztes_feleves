using DJLWQB_HSZF_2024251.Model;
using DJLWQB_HSZF_2024251.Persistence.MsSql.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DJLWQB_HSZF_2024251.Persistence.MsSql
{
    public interface IMovieDataProvider
    {
        Movie GetMovie(string id);

        IEnumerable<Movie> GetMovies();

        void AddMovie(Movie m);

        void RemoveMovie(string id);

        void ModifyMovie(string id, Movie m); 
    }


    public class MovieDataProvider: IMovieDataProvider
    {
        private readonly VideoTKDBContext context;
        public MovieDataProvider(VideoTKDBContext context)
        {
                this.context = context;
        }

        public void AddMovie(Movie m)
        {
            context.Movies.Add(m);
            context.SaveChanges();
        }

        public Movie GetMovie(string id)
        {
            return context.Movies.First(x => x.Id == id);
        }

        public IEnumerable<Movie> GetMovies()
        {
            return context.Movies;
        }

        public void ModifyMovie(string id, Movie m)
        {
            Type type = typeof(Movie);

            foreach (var prop in type.GetProperties()) {
                if (prop.Name != "Id")
                {
                    prop.SetValue(GetMovie(id), prop.GetValue(m));
                }
            }

            context.SaveChanges();
        }

        public void RemoveMovie(string id)
        {
            context.Movies.Remove(GetMovie(id));
            context.SaveChanges();
        }
    }
}
