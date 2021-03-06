﻿using Src.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Src.Models.Service.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ALDBEntities _context;
        public UnitOfWork(ALDBEntities context) => _context = context;

        #region product
        private IProductRepository product;
        public IProductRepository Product { get => product = product ?? new ProductRepository(_context); }

        private IGenericRepository<Tbl_ProcCat> procCat;
        public IGenericRepository<Tbl_ProcCat> ProcCat { get => procCat = procCat ?? new GenericRepository<Tbl_ProcCat>(_context); }

        private IGenericRepository<Tbl_ProcBrand> procBrand;
        public IGenericRepository<Tbl_ProcBrand> ProcBrand { get => procBrand = procBrand ?? new GenericRepository<Tbl_ProcBrand>(_context); }

        private IGenericRepository<Tbl_ProcImg> procImg;
        public IGenericRepository<Tbl_ProcImg> ProcImg { get => procImg = procImg ?? new GenericRepository<Tbl_ProcImg>(_context); }

        private IGenericRepository<Tbl_ProcProp> procProp;
        public IGenericRepository<Tbl_ProcProp> ProcProp { get => procProp = procProp ?? new GenericRepository<Tbl_ProcProp>(_context); }

        private IPCPGRepository pcpGroup;
        public IPCPGRepository PCPGroup { get => pcpGroup = pcpGroup ?? new PCPGRepository(_context); }

        private IGenericRepository<Tbl_ProcReview> procReview;
        public IGenericRepository<Tbl_ProcReview> ProcReview { get => procReview = procReview ?? new GenericRepository<Tbl_ProcReview>(_context); }
        #endregion

        #region factor
        private IFactorRepository factor;
        public IFactorRepository Factor { get => factor = factor ?? new FactorRepository(_context); }

        private IGenericRepository<Tbl_FactProc> factProc;
        public IGenericRepository<Tbl_FactProc> FactProc { get => factProc = factProc ?? new GenericRepository<Tbl_FactProc>(_context); }
        #endregion

        #region customer
        private ICustomerRepository customer;
        public ICustomerRepository Customer { get => customer = customer ?? new CustomerRepository(_context); }

        private IGenericRepository<Tbl_CustAddress> custAddress;
        public IGenericRepository<Tbl_CustAddress> CustAddress { get => custAddress = custAddress ?? new GenericRepository<Tbl_CustAddress>(_context); }
        #endregion

        #region admin
        private IGenericRepository<Tbl_Admin> admin;
        public IGenericRepository<Tbl_Admin> Admin { get => admin = admin ?? new GenericRepository<Tbl_Admin>(_context); }
        #endregion

        #region menu
        private IGenericRepository<Tbl_Menu> menu;
        public IGenericRepository<Tbl_Menu> Menu { get => menu = menu ?? new GenericRepository<Tbl_Menu>(_context); }
        #endregion

        #region media
        private IGenericRepository<Tbl_Media> media;
        public IGenericRepository<Tbl_Media> Media { get => media = media ?? new GenericRepository<Tbl_Media>(_context); }
        #endregion

        #region newsletter
        private IGenericRepository<Tbl_Newsletter> newsletter;
        public IGenericRepository<Tbl_Newsletter> Newsletter { get => newsletter = newsletter ?? new GenericRepository<Tbl_Newsletter>(_context); }
        #endregion

        #region page
        private IGenericRepository<Tbl_Page> page;
        public IGenericRepository<Tbl_Page> Page { get => page = page ?? new GenericRepository<Tbl_Page>(_context); }
        #endregion


        public int Save()
        {
            return _context.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}