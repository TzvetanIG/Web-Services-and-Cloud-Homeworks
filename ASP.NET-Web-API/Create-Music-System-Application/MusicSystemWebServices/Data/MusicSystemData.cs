namespace MusicSystemWebServices.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using Repositories;
    using Models;

    public class MusicSystemData : IMusicSystemData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public MusicSystemData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public ArtistRepository Artists
        {
            get { return (ArtistRepository) this.GetRepository<Artist>(); }
        }

        public SongRepository Songs
        {
            get { return (SongRepository) this.GetRepository<Song>(); }
        }

        public IRepository<Album> Albums
        {
            get { return this.GetRepository<Album>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }



        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            var typeOfRepository = typeof(GenericRepository<T>);

            if (type.IsAssignableFrom(typeof(Artist)))
            {
                typeOfRepository = typeof(ArtistRepository);
            }

            if (type.IsAssignableFrom(typeof(Song)))
            {
                typeOfRepository = typeof(SongRepository);
            }

            if (!this.repositories.ContainsKey(type))
            {
               var repository = Activator.CreateInstance(typeOfRepository, this.context);
                this.repositories.Add(type, repository);
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}