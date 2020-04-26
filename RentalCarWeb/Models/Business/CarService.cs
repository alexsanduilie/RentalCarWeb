using RentalCarWeb.Models.DAO;
using RentalCarWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace RentalCarWeb.Models.Business
{
    public class CarService
    {
        private static readonly CarService instance = new CarService();
        private CarDAO carDAO;
        static CarService()
        {
        }
        private CarService()
        {
            carDAO = CarDAO.Instance;
        }
        public static CarService Instance
        {
            get
            {
                return instance;
            }
        }

        public int confirmID(string column, string paramValue)
        {
                return carDAO.confirmID(column, paramValue);
        }

        public int confirmOverallLocation(string column, string paramValue)
        {
                return carDAO.confirmOverallLocation(column, paramValue);
        }

        public DataTable readAllInDataTable()
        {
            try
            {
                return carDAO.readAllInDataTable();
            }
            catch (SqlException)
            {
                return null;
                throw;
            }
        }

        public List<Car> readAll()
        {
            List<Car> cars = new List<Car>();
            try
            {
                cars = carDAO.readAll();
                return cars;
            }
            catch (SqlException)
            {
                return cars;
                throw;
            }
        }
    }
}