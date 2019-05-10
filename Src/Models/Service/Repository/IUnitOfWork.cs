﻿using Src.Models.Data;
using System;
using System.Threading.Tasks;

namespace Src.Models.Service.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        #region product
        IProductRepository Product { get;}
        IGenericRepository<Tbl_ProcCat> ProcCat { get;}
        IGenericRepository<Tbl_ProcBrand> ProcBrand { get; }
        IGenericRepository<Tbl_ProcImg> ProcImg { get; }
        IGenericRepository<Tbl_ProcProp> ProcProp { get; }
        IGenericRepository<Tbl_PCPGroup> PCPGroup { get; }
        #endregion

        #region factor
        IFactorRepository Factor { get; }
        #endregion

        int Save();
        Task<int> SaveAsync();
    }
}