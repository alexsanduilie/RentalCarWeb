using RentalCarWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace RentalCarWeb.Models.DAO
{
    public class CouponDAO
    {
        private static readonly CouponDAO instance = new CouponDAO();

        static CouponDAO()
        {
        }

        private CouponDAO()
        {
        }

        public static CouponDAO Instance
        {
            get
            {
                return instance;
            }
        }

        private static string table_Name = "Coupons";

        public List<Coupon> readAll()
        {
            string readSQL = "SELECT * FROM " + table_Name;
            List<Coupon> coupons = new List<Coupon>();

            using (SqlCommand cmd = new SqlCommand(readSQL, MvcApplication.conn))
            {
                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        coupons.Add(new Coupon(dr["CouponCode"].ToString(), dr["Description"].ToString(), Double.Parse(dr["Discount"].ToString())));
                    }

                    dr.Close();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    return coupons;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL error: " + ex.Message);
                    return coupons;
                }
            }

        }
    }
}