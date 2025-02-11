﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TrackrAPI.Repositorys
{
    public interface IRepository<T>
    {
        T Agregar(T objeto);
        void Agregar(IEnumerable<T> objetos);
        void Editar(T objeto);
        void Editar(IEnumerable<T> objetos);
        void Eliminar(T objeto);
        void Eliminar(IEnumerable<T> objetos);
        void Truncate();
    }
}
