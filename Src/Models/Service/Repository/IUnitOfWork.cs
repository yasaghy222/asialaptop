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
        IPCPGRepository PCPGroup { get; }
        IGenericRepository<Tbl_ProcReview> ProcReview { get; }
        #endregion

        #region factor
        IFactorRepository Factor { get; }
        IGenericRepository<Tbl_FactProc> FactProc { get; }
        #endregion

        #region customer
        ICustomerRepository Customer { get; }
        IGenericRepository<Tbl_CustAddress> CustAddress { get; }
        #endregion

        #region admin
        IGenericRepository<Tbl_Admin> Admin { get; }
        #endregion

        #region menu
        IGenericRepository<Tbl_Menu> Menu { get; }
        #endregion

        #region media
        IGenericRepository<Tbl_Media> Media { get;}
        #endregion

        #region newsletter
        IGenericRepository<Tbl_Newsletter> Newsletter { get; }
        #endregion

        #region page
        IGenericRepository<Tbl_Page> Page{ get;}
        #endregion

        int Save();
        Task<int> SaveAsync();
    }
}