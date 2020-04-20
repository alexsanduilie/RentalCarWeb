﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RentalCarWeb.Models.Database
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="academy_net")]
	public partial class CustomersRevervationsDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCustomer(Customer instance);
    partial void UpdateCustomer(Customer instance);
    partial void DeleteCustomer(Customer instance);
    partial void InsertReservation(Reservation instance);
    partial void UpdateReservation(Reservation instance);
    partial void DeleteReservation(Reservation instance);
    #endregion
		
		public CustomersRevervationsDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["academy_netConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CustomersRevervationsDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CustomersRevervationsDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CustomersRevervationsDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CustomersRevervationsDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Customer> Customers
		{
			get
			{
				return this.GetTable<Customer>();
			}
		}
		
		public System.Data.Linq.Table<Reservation> Reservations
		{
			get
			{
				return this.GetTable<Reservation>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Customers")]
	public partial class Customer : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _CostumerID;
		
		private string _Name;
		
		private System.DateTime _BirthDate;
		
		private string _Location;
		
		private System.Nullable<int> _ZIPCode;
		
		private EntitySet<Reservation> _Reservations;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCostumerIDChanging(int value);
    partial void OnCostumerIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnBirthDateChanging(System.DateTime value);
    partial void OnBirthDateChanged();
    partial void OnLocationChanging(string value);
    partial void OnLocationChanged();
    partial void OnZIPCodeChanging(System.Nullable<int> value);
    partial void OnZIPCodeChanged();
    #endregion
		
		public Customer()
		{
			this._Reservations = new EntitySet<Reservation>(new Action<Reservation>(this.attach_Reservations), new Action<Reservation>(this.detach_Reservations));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CostumerID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int CostumerID
		{
			get
			{
				return this._CostumerID;
			}
			set
			{
				if ((this._CostumerID != value))
				{
					this.OnCostumerIDChanging(value);
					this.SendPropertyChanging();
					this._CostumerID = value;
					this.SendPropertyChanged("CostumerID");
					this.OnCostumerIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BirthDate", DbType="Date NOT NULL")]
		public System.DateTime BirthDate
		{
			get
			{
				return this._BirthDate;
			}
			set
			{
				if ((this._BirthDate != value))
				{
					this.OnBirthDateChanging(value);
					this.SendPropertyChanging();
					this._BirthDate = value;
					this.SendPropertyChanged("BirthDate");
					this.OnBirthDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Location", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Location
		{
			get
			{
				return this._Location;
			}
			set
			{
				if ((this._Location != value))
				{
					this.OnLocationChanging(value);
					this.SendPropertyChanging();
					this._Location = value;
					this.SendPropertyChanged("Location");
					this.OnLocationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ZIPCode", DbType="Int")]
		public System.Nullable<int> ZIPCode
		{
			get
			{
				return this._ZIPCode;
			}
			set
			{
				if ((this._ZIPCode != value))
				{
					this.OnZIPCodeChanging(value);
					this.SendPropertyChanging();
					this._ZIPCode = value;
					this.SendPropertyChanged("ZIPCode");
					this.OnZIPCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Customer_Reservation", Storage="_Reservations", ThisKey="CostumerID", OtherKey="CostumerID")]
		public EntitySet<Reservation> Reservations
		{
			get
			{
				return this._Reservations;
			}
			set
			{
				this._Reservations.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Reservations(Reservation entity)
		{
			this.SendPropertyChanging();
			entity.Customer = this;
		}
		
		private void detach_Reservations(Reservation entity)
		{
			this.SendPropertyChanging();
			entity.Customer = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Reservations")]
	public partial class Reservation : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _CarID;
		
		private string _CarPlate;
		
		private int _CostumerID;
		
		private byte _ReservStatsID;
		
		private System.DateTime _StartDate;
		
		private System.DateTime _EndDate;
		
		private string _Location;
		
		private string _CouponCode;
		
		private EntityRef<Customer> _Customer;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCarIDChanging(int value);
    partial void OnCarIDChanged();
    partial void OnCarPlateChanging(string value);
    partial void OnCarPlateChanged();
    partial void OnCostumerIDChanging(int value);
    partial void OnCostumerIDChanged();
    partial void OnReservStatsIDChanging(byte value);
    partial void OnReservStatsIDChanged();
    partial void OnStartDateChanging(System.DateTime value);
    partial void OnStartDateChanged();
    partial void OnEndDateChanging(System.DateTime value);
    partial void OnEndDateChanged();
    partial void OnLocationChanging(string value);
    partial void OnLocationChanged();
    partial void OnCouponCodeChanging(string value);
    partial void OnCouponCodeChanged();
    #endregion
		
		public Reservation()
		{
			this._Customer = default(EntityRef<Customer>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CarID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int CarID
		{
			get
			{
				return this._CarID;
			}
			set
			{
				if ((this._CarID != value))
				{
					this.OnCarIDChanging(value);
					this.SendPropertyChanging();
					this._CarID = value;
					this.SendPropertyChanged("CarID");
					this.OnCarIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CarPlate", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
		public string CarPlate
		{
			get
			{
				return this._CarPlate;
			}
			set
			{
				if ((this._CarPlate != value))
				{
					this.OnCarPlateChanging(value);
					this.SendPropertyChanging();
					this._CarPlate = value;
					this.SendPropertyChanged("CarPlate");
					this.OnCarPlateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CostumerID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int CostumerID
		{
			get
			{
				return this._CostumerID;
			}
			set
			{
				if ((this._CostumerID != value))
				{
					if (this._Customer.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCostumerIDChanging(value);
					this.SendPropertyChanging();
					this._CostumerID = value;
					this.SendPropertyChanged("CostumerID");
					this.OnCostumerIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReservStatsID", DbType="TinyInt NOT NULL")]
		public byte ReservStatsID
		{
			get
			{
				return this._ReservStatsID;
			}
			set
			{
				if ((this._ReservStatsID != value))
				{
					this.OnReservStatsIDChanging(value);
					this.SendPropertyChanging();
					this._ReservStatsID = value;
					this.SendPropertyChanged("ReservStatsID");
					this.OnReservStatsIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StartDate", DbType="Date NOT NULL")]
		public System.DateTime StartDate
		{
			get
			{
				return this._StartDate;
			}
			set
			{
				if ((this._StartDate != value))
				{
					this.OnStartDateChanging(value);
					this.SendPropertyChanging();
					this._StartDate = value;
					this.SendPropertyChanged("StartDate");
					this.OnStartDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EndDate", DbType="Date NOT NULL")]
		public System.DateTime EndDate
		{
			get
			{
				return this._EndDate;
			}
			set
			{
				if ((this._EndDate != value))
				{
					this.OnEndDateChanging(value);
					this.SendPropertyChanging();
					this._EndDate = value;
					this.SendPropertyChanged("EndDate");
					this.OnEndDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Location", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Location
		{
			get
			{
				return this._Location;
			}
			set
			{
				if ((this._Location != value))
				{
					this.OnLocationChanging(value);
					this.SendPropertyChanging();
					this._Location = value;
					this.SendPropertyChanged("Location");
					this.OnLocationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CouponCode", DbType="VarChar(10)")]
		public string CouponCode
		{
			get
			{
				return this._CouponCode;
			}
			set
			{
				if ((this._CouponCode != value))
				{
					this.OnCouponCodeChanging(value);
					this.SendPropertyChanging();
					this._CouponCode = value;
					this.SendPropertyChanged("CouponCode");
					this.OnCouponCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Customer_Reservation", Storage="_Customer", ThisKey="CostumerID", OtherKey="CostumerID", IsForeignKey=true)]
		public Customer Customer
		{
			get
			{
				return this._Customer.Entity;
			}
			set
			{
				Customer previousValue = this._Customer.Entity;
				if (((previousValue != value) 
							|| (this._Customer.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Customer.Entity = null;
						previousValue.Reservations.Remove(this);
					}
					this._Customer.Entity = value;
					if ((value != null))
					{
						value.Reservations.Add(this);
						this._CostumerID = value.CostumerID;
					}
					else
					{
						this._CostumerID = default(int);
					}
					this.SendPropertyChanged("Customer");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
