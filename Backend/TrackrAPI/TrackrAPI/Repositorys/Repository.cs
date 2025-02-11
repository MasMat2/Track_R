﻿using Microsoft.EntityFrameworkCore;
using TrackrAPI.Helpers;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys
{
    public abstract class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        public TrackrContext context;

        public Repository(TrackrContext context)
        {
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public T Agregar(T objeto)
        {
            typeof(T).GetProperties().ToList().ForEach(propertyInfo =>
            {
                if ((propertyInfo.PropertyType.IsClass && !propertyInfo.PropertyType.FullName.StartsWith("System.")) ||
                     propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    propertyInfo.SetValue(objeto, null);
                }
            });

            context.Set<T>().Add(objeto);
            context.SaveChanges();

            context.Entry(objeto).State = EntityState.Detached;
            context.SaveChanges();
            return objeto;
        }

        public void Editar(T objeto)
        {
            typeof(T).GetProperties().ToList().ForEach(propertyInfo =>
            {
                if ((propertyInfo.PropertyType.IsClass && !propertyInfo.PropertyType.FullName.StartsWith("System.")) ||
                     propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    propertyInfo.SetValue(objeto, null);
                }
            });

            context.Set<T>().Attach(objeto);
            context.Entry(objeto).State = EntityState.Modified;
            context.SaveChanges();

            context.Entry(objeto).State = EntityState.Detached;
            context.SaveChanges();
        }

        public void Eliminar(T objeto)
        {
            typeof(T).GetProperties().ToList().ForEach(propertyInfo =>
            {
                if ((propertyInfo.PropertyType.IsClass && !propertyInfo.PropertyType.FullName.StartsWith("System.")) ||
                     propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    propertyInfo.SetValue(objeto, null);
                }
            });

            context.Remove(objeto);
            context.SaveChanges();
        }

        public void Eliminar(IEnumerable<T> objetos)
        {
            foreach (T objeto in objetos)
            {
                Eliminar(objeto);
            }
        }

        public void Truncate()
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            var tableName = entityType.GetTableName();
            var schema = entityType.GetSchema();

            var fullTableName = string.IsNullOrEmpty(schema) ? tableName : $"{schema}.{tableName}";


            context.Database.ExecuteSqlRaw($"TRUNCATE TABLE {fullTableName}");
            
        }

        public void Editar(IEnumerable<T> objetos)
        {
            foreach (T objeto in objetos)
            {
                typeof(T).GetProperties().ToList().ForEach(propertyInfo =>
                {
                    if ((propertyInfo.PropertyType.IsClass && !propertyInfo.PropertyType.FullName.StartsWith("System.")) ||
                         propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                    {
                        propertyInfo.SetValue(objeto, null);
                    }
                });

                context.Set<T>().Attach(objeto);
                context.Entry(objeto).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void Agregar(IEnumerable<T> objetos)
        {
            try
            {
                foreach (T objeto in objetos)
                {
                    typeof(T).GetProperties().ToList().ForEach(propertyInfo =>
                    {
                        if ((propertyInfo.PropertyType.IsClass && !propertyInfo.PropertyType.FullName.StartsWith("System.")) ||
                             propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                        {
                            propertyInfo.SetValue(objeto, null);
                        }
                    });

                    context.Set<T>().Add(objeto);

                }

                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new CdisException(ex.Message);
            }

        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
            GC.SuppressFinalize(this);
        }

    }
}
