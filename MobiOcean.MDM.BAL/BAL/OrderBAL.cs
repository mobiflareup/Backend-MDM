using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.OrderDALTableAdapters;

/// <summary>
/// Summary description for OrderBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class OrderBAL
    {
        tblOrderMasterTableAdapter order;
        tblOrderDetailTableAdapter orderdtls;
        private string _orderno, _totalamt;
        private int _ordermasterid, _orderstatus, _ispaymentreceived, _orderdetailid, _quantity;

        public string orderno
        {
            get { return _orderno; }
            set { _orderno = value; }
        }
        public int ordermasterid
        {
            get { return _ordermasterid; }
            set { _ordermasterid = value; }
        }
        public int orderstatus
        {
            get { return _orderstatus; }
            set { _orderstatus = value; }
        }
        public int ispaymentreceived
        {
            get { return _ispaymentreceived; }
            set { _ispaymentreceived = value; }
        }
        public int quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
        public int orderdetailid
        {
            get { return _orderdetailid; }
            set { _orderdetailid = value; }
        }
        public string totalamt
        {
            get { return _totalamt; }
            set { _totalamt = value; }
        }
        public OrderBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetOrderMasterDetails()
        {
            order = new tblOrderMasterTableAdapter();
            return order.GetOrderMasterDetails(_orderno);
        }
        public DataTable GetOrderDetails()
        {
            orderdtls = new tblOrderDetailTableAdapter();
            return orderdtls.GetOrderDetails(_ordermasterid);
        }
        public int UpdateOrderMaster()
        {
            order = new tblOrderMasterTableAdapter();
            return order.UpdateOrderMaster(_orderstatus, _ispaymentreceived, ordermasterid);
        }
        public int DeleteOrderMaster()
        {
            order = new tblOrderMasterTableAdapter();
            return order.DeleteOrderMaster(_ordermasterid);
        }
        public int UpdateOrderDetails()
        {
            orderdtls = new tblOrderDetailTableAdapter();
            orderdtls.sp_UpdateorderDetail(_orderdetailid, _quantity, Convert.ToDouble(_totalamt));// UpdateOrderDetails(_quantity, Convert.ToDouble(_totalamt), _orderdetailid);
            return 1;
        }
        public int DeleteOrderDetails()
        {
            orderdtls = new tblOrderDetailTableAdapter();
            return orderdtls.DeleteOrderDetails(_orderdetailid);
        }
    }
}