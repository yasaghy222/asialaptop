﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using Src.Controllers;
using Src.Models.Data;
using Src.Models.ViewData.Table;
using Src.Models.Utitlity;
using System.ComponentModel.DataAnnotations;
using Src.Models.ViewData.Base;
using System.Linq;
using Mapster;
using System.Collections.Generic;

namespace Src
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new NinjectController());

            #region authorize filler
            #endregion

            #region mapster
            #region product
            TypeAdapterConfig<Tbl_Product, Product.ViewProc>.NewConfig()
                   .Map(dest => dest.BrandName, src => src.Tbl_ProcBrand.Title)
                   .Map(dest => dest.CatName, src => src.Tbl_ProcCat.GetCatList())
                   .Map(dest => dest.Price, src => src.Price.SetCama())
                   .Map(dest => dest.OffPrice, src => src.OffPrice.SetCama())
                   .Map(dest => dest.Type, src => (EnumExtensions
                                                   .GetEnumValue<Product.ProcType>(src.Type))
                                                   .GetAttribute<DisplayAttribute>().Name);

            TypeAdapterConfig<Tbl_Product, Product.ViewTbl_Proc>.NewConfig()
                   .Map(dest => dest.FullDesc, src => HttpUtility.HtmlDecode(src.FullDesc));

            TypeAdapterConfig<Tbl_Product, Product.FullSearchResult>.NewConfig()
                   .Map(dest => dest.Price, src => src.Price.SetCama())
                   .Map(dest => dest.OffPrice, src => src.OffPrice.SetCama())
                   .Map(dest => dest.Type, src => (EnumExtensions
                                                   .GetEnumValue<Product.ProcType>(src.Type))
                                                   .GetAttribute<DisplayAttribute>().Name);
            #endregion

            #region procBrand
            TypeAdapterConfig<Tbl_ProcBrand, Product.ViewTbl_ProcBrand>.NewConfig()
                   .Map(dest => dest.AssignCount, src => src.Tbl_Product.Count);
            #endregion

            #region product category
            TypeAdapterConfig<Tbl_ProcCat, Product.ViewTbl_ProcCat>.NewConfig()
                   .Map(dest => dest.AssignCount, src => src.Tbl_Product.Count());

            TypeAdapterConfig<Tbl_ProcCat, Common.FullTree>.NewConfig()
                   .Map(dest => dest.AssignCount, src => src.Tbl_Product.Count())
                   .Map(dest => dest.HasChild, src =>
                                               GenericFunction<Tbl_ProcCat>
                                               .HasChild(src, item => item.PID == src.ID));
            #endregion

            #region product property
            TypeAdapterConfig<Tbl_PCPGroup, Common.FullTree>.NewConfig()
                   .Map(dest => dest.AssignCount, src => src.Tbl_ProcProp.Count())
                   .Map(dest => dest.HasChild, src =>
                                               GenericFunction<Tbl_PCPGroup>
                                               .HasChild(src, item => item.PID == src.ID));

            TypeAdapterConfig<Tbl_PCPGroup, Product.CatProp>.NewConfig()
                   .Map(dest => dest.ValueList, src => src.Tbl_ProcProp.Where(item => item.PCPGID == src.ID).Select(item => item.Value).ToList());
            #endregion

            #region factor
            TypeAdapterConfig<Tbl_Factor, Factor.ViewOrderDetail>.NewConfig()
                   .Map(dest => dest.CustName, src => src.Tbl_Customer.Name)
                   .Map(dest => dest.CustAddress, src => src.Tbl_CustAddress)
                   .Map(dest => dest.OrderProc, src => src.Tbl_FactProc)
                   .Map(dest => dest.PaymentStatus, src => src.PaymentStatus ?
                                                           Factor.PaymentStatus.Paid : Factor.PaymentStatus.UnPaid)
                   .Map(dest => dest.TotalPrice, src => src.TotalPrice.SetCama())
                   .Map(dest => dest.Status, src => (EnumExtensions
                                                     .GetEnumValue<Factor.FactStatus>(src.Status))
                                                     .GetAttribute<DisplayAttribute>().Name)
                   .Map(dest => dest.SubmitDate, src => src.SubmitDate.ToPersianDate("default"));
            #endregion
            #endregion
        }
    }
}