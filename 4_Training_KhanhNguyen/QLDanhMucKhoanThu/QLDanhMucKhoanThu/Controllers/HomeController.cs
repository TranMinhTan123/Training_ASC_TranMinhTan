﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using QLDanhMucKhoanThu.Models;

namespace QLDanhMucKhoanThu.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Trần Minh Tân
        /// </summary>
        private ThucTap_DoAnEntities1 db = new ThucTap_DoAnEntities1();
        #region Đăng nhập
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult XuLyDangNhap(FormCollection form)
        {
            if (form["mssv"] != "" && form["matkhau"] != "")
            {
                string maSV = form["mssv"].ToString();
                string matKhau = form["matkhau"].ToString();
                var sinhVien = db.sp_GetSinhVien(maSV, matKhau).FirstOrDefault();
                if (sinhVien != null)
                {
                    //lưu sinh viên
                    Session["sinhvien"] = sinhVien;
                    return View("LoadDSKhoanThu");
                    //return Content("Đăng nhập thành công");
                }
                else
                {
                    ViewBag.Loi = "Mã số sinh viên/mật khẩu không chính xác!";
                }
            }
            else
            {
                ViewBag.Loi = "Mã số sinh viên/mật khẩu không được để trống";
            }
            return View("LoadDSKhoanThu");
        }
        #endregion
        #region Kiểm tra nhập Mã môn học
        public string ktMa(string ma, decimal? dongia)
        {    
            string kq = "";
            // Kiểm tra độ dài mã
            if (ma.Length != 10 && ma != "")
            {
                kq = "Mã phải 10 ký tự";
                return kq;
            }
            else 
            {
                // kiểm tra trùng mã
                var result = db.sp_KhoanThu().Where(x => x.MaMH.Equals(ma)).ToList();
                if (result.Count != 0)
                {
                    kq = "Trùng Mã";
                    return kq;
                }
                else
                {
                    //kiểm tra khoảng trắng khi nhập mã
                    for (int i=0;i<ma.Length;i++)
                    {
                        if (ma.Substring(i, 1) == " ")
                        {
                            kq = "Mã không hợp lệ";
                        }
                    }
                    //Đơn giá phải lớn hơn hoặc bằng 100
                    if (dongia < 100 || dongia == null)
                    {
                        kq = "Số tiền tối thiểu phải 100$";
                    }
                }
            }
            return kq;
        }
        #endregion

        #region Load danh sách khoản thu môn học
        public ActionResult LoadDSKhoanThu()
        {
            return View();
        }

        public ActionResult TBL_KhoanThuMonHoc_Read([DataSourceRequest]DataSourceRequest request)
        {
            var khoanthu = db.sp_KhoanThu().ToList();
            DataSourceResult result = khoanthu.ToDataSourceResult(request, tBL_KhoanThuMonHoc => new
            {
                Id = tBL_KhoanThuMonHoc.Id,
                MaMH = tBL_KhoanThuMonHoc.MaMH,
                TenMH = tBL_KhoanThuMonHoc.TenMH,
                SoTC = tBL_KhoanThuMonHoc.SoTC,
                SoTiet = tBL_KhoanThuMonHoc.SoTiet,
                DonGia = tBL_KhoanThuMonHoc.DonGia
            });

            return Json(result);
        }
        #endregion
        #region Thêm khoản thu 
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TBL_KhoanThuMonHoc_Create([DataSourceRequest]DataSourceRequest request, sp_KhoanThu_Result sp_KhoanThu_Result) 
        {

            if(ktMa(sp_KhoanThu_Result.MaMH, sp_KhoanThu_Result.DonGia) !="")
            {
                return this.Json(new DataSourceResult
                {
                    Errors = ktMa(sp_KhoanThu_Result.MaMH, sp_KhoanThu_Result.DonGia)
                });
            }
            else
            {
                if (ModelState.IsValid)
                {

                    db.sp_Insert_KhoanThu(sp_KhoanThu_Result.MaMH, sp_KhoanThu_Result.TenMH, sp_KhoanThu_Result.SoTC, sp_KhoanThu_Result.SoTiet, sp_KhoanThu_Result.DonGia);
                    db.SaveChanges();
                    return Json(new[] { sp_KhoanThu_Result }.ToDataSourceResult(request, ModelState));
                }
            }
            return View("LoadDSKhoanThu");
        }
        #endregion
        #region cập nhật khoản thu
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TBL_KhoanThuMonHoc_Update([DataSourceRequest]DataSourceRequest request, sp_KhoanThu_Result sp_KhoanThu_Result)
        {
            if (ModelState.IsValid)
            {
                db.sp_Update_KhoanThu(sp_KhoanThu_Result.Id, sp_KhoanThu_Result.MaMH, sp_KhoanThu_Result.TenMH, sp_KhoanThu_Result.SoTC, sp_KhoanThu_Result.SoTiet, sp_KhoanThu_Result.DonGia);
                //db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { sp_KhoanThu_Result }.ToDataSourceResult(request, ModelState));
        }
        #endregion
        #region Xóa khoản thu
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TBL_KhoanThuMonHoc_Destroy([DataSourceRequest]DataSourceRequest request, sp_KhoanThu_Result sp_KhoanThu_Result)
        {
            if (ModelState.IsValid)
            {
                db.sp_Delete_KhoanThu(sp_KhoanThu_Result.Id);
                db.SaveChanges();
            }
            return Json(new[] { sp_KhoanThu_Result }.ToDataSourceResult(request, ModelState));
        }
        #endregion
    }
}
