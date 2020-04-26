using RentalCarWeb.Models.DAO;
using RentalCarWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace RentalCarWeb.Models.Business
{
    public class CouponService
    {
        private static readonly CouponService instance = new CouponService();
        private CouponDAO couponDAO;
        static CouponService()
        {
        }
        private CouponService()
        {
            couponDAO = CouponDAO.Instance;
        }
        public static CouponService Instance
        {
            get
            {
                return instance;
            }
        }

        public List<Coupon> readAll()
        {
            List<Coupon> coupons = new List<Coupon>();
            try
            {
                coupons = couponDAO.readAll();
                return coupons;
            }
            catch (SqlException)
            {
                return coupons;
                throw;   
            }
        }
    }
}