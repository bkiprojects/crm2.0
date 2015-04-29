﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace BKI_CRM2.Models
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using System.Data.Entity.Core.Objects;
using System.Linq;


public partial class CrmEntities : DbContext
{
    public CrmEntities()
        : base("name=CrmEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<AccountContactRole> AccountContactRole { get; set; }

    public virtual DbSet<Action> Action { get; set; }

    public virtual DbSet<Company> Company { get; set; }

    public virtual DbSet<Contact> Contact { get; set; }

    public virtual DbSet<ContactStateChange> ContactStateChange { get; set; }

    public virtual DbSet<ContactStateProcess> ContactStateProcess { get; set; }

    public virtual DbSet<ContractContactRole> ContractContactRole { get; set; }

    public virtual DbSet<LoaiTuDien> LoaiTuDien { get; set; }

    public virtual DbSet<Order> Order { get; set; }

    public virtual DbSet<Product> Product { get; set; }

    public virtual DbSet<Quote> Quote { get; set; }

    public virtual DbSet<Task> Task { get; set; }

    public virtual DbSet<ToBeConverted> ToBeConverted { get; set; }

    public virtual DbSet<TuDien> TuDien { get; set; }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<UserContactRole> UserContactRole { get; set; }

    public virtual DbSet<UserGroup> UserGroup { get; set; }

    public virtual DbSet<sysdiagram> sysdiagram { get; set; }

    public virtual DbSet<Account> Account { get; set; }

    public virtual DbSet<ContactState> ContactState { get; set; }

    public virtual DbSet<Contract> Contract { get; set; }


    public virtual int pr_Contact_Delete(Nullable<decimal> id)
    {

        var idParameter = id.HasValue ?
            new ObjectParameter("Id", id) :
            new ObjectParameter("Id", typeof(decimal));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pr_Contact_Delete", idParameter);
    }


    public virtual int pr_Contact_Insert(string ho, string ten, string diaChi, Nullable<bool> gioiTinh, string image, string facebook, string skype, Nullable<System.DateTime> ngaySinh, string sdt01, string sdt02, string maSoThue, string soTaiKhoan, string website, string email, Nullable<System.DateTime> hanKhachHang, Nullable<decimal> idLoaiKhachHang, Nullable<decimal> idTrangThaiHienTai, ObjectParameter id)
    {

        var hoParameter = ho != null ?
            new ObjectParameter("Ho", ho) :
            new ObjectParameter("Ho", typeof(string));


        var tenParameter = ten != null ?
            new ObjectParameter("Ten", ten) :
            new ObjectParameter("Ten", typeof(string));


        var diaChiParameter = diaChi != null ?
            new ObjectParameter("DiaChi", diaChi) :
            new ObjectParameter("DiaChi", typeof(string));


        var gioiTinhParameter = gioiTinh.HasValue ?
            new ObjectParameter("GioiTinh", gioiTinh) :
            new ObjectParameter("GioiTinh", typeof(bool));


        var imageParameter = image != null ?
            new ObjectParameter("Image", image) :
            new ObjectParameter("Image", typeof(string));


        var facebookParameter = facebook != null ?
            new ObjectParameter("Facebook", facebook) :
            new ObjectParameter("Facebook", typeof(string));


        var skypeParameter = skype != null ?
            new ObjectParameter("Skype", skype) :
            new ObjectParameter("Skype", typeof(string));


        var ngaySinhParameter = ngaySinh.HasValue ?
            new ObjectParameter("NgaySinh", ngaySinh) :
            new ObjectParameter("NgaySinh", typeof(System.DateTime));


        var sdt01Parameter = sdt01 != null ?
            new ObjectParameter("Sdt01", sdt01) :
            new ObjectParameter("Sdt01", typeof(string));


        var sdt02Parameter = sdt02 != null ?
            new ObjectParameter("Sdt02", sdt02) :
            new ObjectParameter("Sdt02", typeof(string));


        var maSoThueParameter = maSoThue != null ?
            new ObjectParameter("MaSoThue", maSoThue) :
            new ObjectParameter("MaSoThue", typeof(string));


        var soTaiKhoanParameter = soTaiKhoan != null ?
            new ObjectParameter("SoTaiKhoan", soTaiKhoan) :
            new ObjectParameter("SoTaiKhoan", typeof(string));


        var websiteParameter = website != null ?
            new ObjectParameter("Website", website) :
            new ObjectParameter("Website", typeof(string));


        var emailParameter = email != null ?
            new ObjectParameter("Email", email) :
            new ObjectParameter("Email", typeof(string));


        var hanKhachHangParameter = hanKhachHang.HasValue ?
            new ObjectParameter("HanKhachHang", hanKhachHang) :
            new ObjectParameter("HanKhachHang", typeof(System.DateTime));


        var idLoaiKhachHangParameter = idLoaiKhachHang.HasValue ?
            new ObjectParameter("IdLoaiKhachHang", idLoaiKhachHang) :
            new ObjectParameter("IdLoaiKhachHang", typeof(decimal));


        var idTrangThaiHienTaiParameter = idTrangThaiHienTai.HasValue ?
            new ObjectParameter("IdTrangThaiHienTai", idTrangThaiHienTai) :
            new ObjectParameter("IdTrangThaiHienTai", typeof(decimal));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pr_Contact_Insert", hoParameter, tenParameter, diaChiParameter, gioiTinhParameter, imageParameter, facebookParameter, skypeParameter, ngaySinhParameter, sdt01Parameter, sdt02Parameter, maSoThueParameter, soTaiKhoanParameter, websiteParameter, emailParameter, hanKhachHangParameter, idLoaiKhachHangParameter, idTrangThaiHienTaiParameter, id);
    }


    public virtual int pr_Contact_Update(Nullable<decimal> id, string ho, string ten, string diaChi, Nullable<bool> gioiTinh, string image, string facebook, string skype, Nullable<System.DateTime> ngaySinh, string sdt01, string sdt02, string maSoThue, string soTaiKhoan, string website, string email, Nullable<System.DateTime> hanKhachHang, Nullable<decimal> idLoaiKhachHang, Nullable<decimal> idTrangThaiHienTai)
    {

        var idParameter = id.HasValue ?
            new ObjectParameter("Id", id) :
            new ObjectParameter("Id", typeof(decimal));


        var hoParameter = ho != null ?
            new ObjectParameter("Ho", ho) :
            new ObjectParameter("Ho", typeof(string));


        var tenParameter = ten != null ?
            new ObjectParameter("Ten", ten) :
            new ObjectParameter("Ten", typeof(string));


        var diaChiParameter = diaChi != null ?
            new ObjectParameter("DiaChi", diaChi) :
            new ObjectParameter("DiaChi", typeof(string));


        var gioiTinhParameter = gioiTinh.HasValue ?
            new ObjectParameter("GioiTinh", gioiTinh) :
            new ObjectParameter("GioiTinh", typeof(bool));


        var imageParameter = image != null ?
            new ObjectParameter("Image", image) :
            new ObjectParameter("Image", typeof(string));


        var facebookParameter = facebook != null ?
            new ObjectParameter("Facebook", facebook) :
            new ObjectParameter("Facebook", typeof(string));


        var skypeParameter = skype != null ?
            new ObjectParameter("Skype", skype) :
            new ObjectParameter("Skype", typeof(string));


        var ngaySinhParameter = ngaySinh.HasValue ?
            new ObjectParameter("NgaySinh", ngaySinh) :
            new ObjectParameter("NgaySinh", typeof(System.DateTime));


        var sdt01Parameter = sdt01 != null ?
            new ObjectParameter("Sdt01", sdt01) :
            new ObjectParameter("Sdt01", typeof(string));


        var sdt02Parameter = sdt02 != null ?
            new ObjectParameter("Sdt02", sdt02) :
            new ObjectParameter("Sdt02", typeof(string));


        var maSoThueParameter = maSoThue != null ?
            new ObjectParameter("MaSoThue", maSoThue) :
            new ObjectParameter("MaSoThue", typeof(string));


        var soTaiKhoanParameter = soTaiKhoan != null ?
            new ObjectParameter("SoTaiKhoan", soTaiKhoan) :
            new ObjectParameter("SoTaiKhoan", typeof(string));


        var websiteParameter = website != null ?
            new ObjectParameter("Website", website) :
            new ObjectParameter("Website", typeof(string));


        var emailParameter = email != null ?
            new ObjectParameter("Email", email) :
            new ObjectParameter("Email", typeof(string));


        var hanKhachHangParameter = hanKhachHang.HasValue ?
            new ObjectParameter("HanKhachHang", hanKhachHang) :
            new ObjectParameter("HanKhachHang", typeof(System.DateTime));


        var idLoaiKhachHangParameter = idLoaiKhachHang.HasValue ?
            new ObjectParameter("IdLoaiKhachHang", idLoaiKhachHang) :
            new ObjectParameter("IdLoaiKhachHang", typeof(decimal));


        var idTrangThaiHienTaiParameter = idTrangThaiHienTai.HasValue ?
            new ObjectParameter("IdTrangThaiHienTai", idTrangThaiHienTai) :
            new ObjectParameter("IdTrangThaiHienTai", typeof(decimal));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pr_Contact_Update", idParameter, hoParameter, tenParameter, diaChiParameter, gioiTinhParameter, imageParameter, facebookParameter, skypeParameter, ngaySinhParameter, sdt01Parameter, sdt02Parameter, maSoThueParameter, soTaiKhoanParameter, websiteParameter, emailParameter, hanKhachHangParameter, idLoaiKhachHangParameter, idTrangThaiHienTaiParameter);
    }

}

}

