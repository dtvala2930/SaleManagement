﻿using System;
using System.Data;
using System.Data.SqlClient;
using DTO_QLBanHang;
namespace DAL_QLBanHang
{
   public class DAL_KhachHang : DBConnect
    {
        //xem danh sách khách hàng
        public DataTable danhSachKH()
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DanhSachKhach";
                DataTable data = new DataTable();
                data.Load(cmd.ExecuteReader());
                return data;
            }
            finally
            {
                _conn.Close();
            }
        }


        //thêm khách hàng
        public bool insertKH(DTO_KhachHang khachHang)
        {

            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertDataKhach";
                cmd.Parameters.AddWithValue("DienThoai", khachHang.SoDienThoai);
                cmd.Parameters.AddWithValue("TenKhach", khachHang.TenKhach);
                cmd.Parameters.AddWithValue("DiaChi", khachHang.DiaChi);
                cmd.Parameters.AddWithValue("Phai", khachHang.Phai);
                cmd.Parameters.AddWithValue("Email", khachHang.EmailNV);
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

            }
            finally
            {
                _conn.Close();
            }
            return false;
        }


        // cập nhật khách hàng
        public bool updateKH(DTO_KhachHang khachHang)
        {
            try
            {
                
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateKhach";
                cmd.Parameters.AddWithValue("dienThoai", khachHang.SoDienThoai);
                cmd.Parameters.AddWithValue("tenKhach", khachHang.TenKhach);
                cmd.Parameters.AddWithValue("diaChi", khachHang.DiaChi);
                cmd.Parameters.AddWithValue("phai", khachHang.Phai);
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception)
            {

            }
            finally
            {
                _conn.Close();
            }

            return false;
        }


        // xóa khách hàng
        public bool deleteKH(string dienthoai)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteDataFromKhach";
                cmd.Parameters.AddWithValue("DienThoai", dienthoai);

                if (cmd.ExecuteNonQuery() >0)
                    return true;
                else
                    return false;

            }
            catch (Exception)
            {

            }
            finally
            {
                _conn.Close();
            }

            return false;
        }

        // tìm khách hàng
        public DataTable searchKH(string tenKhach)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SearchKhach";
                cmd.Parameters.AddWithValue("tenKhach", tenKhach);
                DataTable data = new DataTable();
                data.Load(cmd.ExecuteReader());
                return data;

            }

            finally
            {
                _conn.Close();
            }

        }

    }
}
