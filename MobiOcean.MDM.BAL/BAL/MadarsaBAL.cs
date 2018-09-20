using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.MadarsaDALTableAdapters;

/// <summary>
/// Summary description for MadarsaBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class MadarsaBAL
    {
        MadarsadetailTableAdapter madarsadtl;
        public List<MadarsaDetailList> MadarsaDetailsList { get; set; }
        public List<ParaTeacherList> ParaTeacherList { get; set; }
        public int MadarsaId { get; set; }
        public string MadarsaRegistrationNumber { get; set; }



        public string MadarsaName { get; set; }
        public string MadarsaEmail { get; set; }
        public string District { get; set; }
        public string DeviceId { get; set; }
        public double DeviceLatitude { get; set; }
        public double DeviceLongitude { get; set; }
        public string IMINo { get; set; }
        public string ParaTeacherName { get; set; }
        public string ParaTeacherEmail { get; set; }
        public string ParaTeacherAadhar { get; set; }
        public string ParaTeacherMobile { get; set; }
        public int ParaTeacherId { get; set; }
        public int UserId { get; set; }
        public string DateTime { get; set; }
        public int IsSendToServer { get; set; }
        public int IsLogin { get; set; }
        public string ImagePath { get; set; }

        public MadarsaBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetMadarsaAttendanceDetails()
        {
            madarsadtl = new MadarsadetailTableAdapter();
            return madarsadtl.GetMadarsaAttendanceDetail();
        }
        public int IU_MadarsaDetail()
        {
            madarsadtl = new MadarsadetailTableAdapter();
            return Convert.ToInt32(madarsadtl.IU_MadarsaDetail(MadarsaId, MadarsaName, MadarsaEmail, MadarsaRegistrationNumber, District, DeviceId, DeviceLatitude.ToString(), DeviceLongitude.ToString(), IMINo).ToString());
        }
        public int RegisterDevice()
        {
            madarsadtl = new MadarsadetailTableAdapter();
            return Convert.ToInt32(madarsadtl.RegisterDevice(DeviceId, MadarsaName, MadarsaRegistrationNumber, DeviceLatitude.ToString(), DeviceLongitude.ToString()));
        }
        public DataTable GetParaTeacherList()
        {
            madarsadtl = new MadarsadetailTableAdapter();
            return madarsadtl.GetParaTeacherDetail(MadarsaId);
        }
        public int IU_ParaTeacherList()
        {
            madarsadtl = new MadarsadetailTableAdapter();
            return Convert.ToInt32(madarsadtl.IU_ParaTeacherList(ParaTeacherId, ParaTeacherName, ParaTeacherEmail, ParaTeacherMobile, ParaTeacherAadhar, MadarsaId).ToString());
        }
        public int MarkAttendance()
        {
            madarsadtl = new MadarsadetailTableAdapter();
            return Convert.ToInt32(madarsadtl.InsertAttendance(ParaTeacherId, Convert.ToDateTime(DateTime), DeviceLatitude, DeviceLongitude).ToString());
        }
        public int UploadImage()
        {
            madarsadtl = new MadarsadetailTableAdapter();
            return Convert.ToInt32(madarsadtl.UploadImage(ParaTeacherId, ImagePath).ToString());
        }
    }
    public class MadarsaDetailList
    {
        public int MadarsaId { get; set; }
        public string MadarsaRegistrationNumber { get; set; }
        public string MadarsaName { get; set; }
        public string MadarsaEmail { get; set; }
        public string District { get; set; }
        public string DeviceId { get; set; }
        public double DeviceLatitude { get; set; }
        public double DeviceLongitude { get; set; }
        public string IMINo { get; set; }
    }
    public class ParaTeacherList
    {
        public string ParaTeacherName { get; set; }
        public string ParaTeacherEmail { get; set; }
        public string ParaTeacherAadhar { get; set; }
        public string ParaTeacherMobile { get; set; }
        public int ParaTeacherId { get; set; }
        public int MadarsaId { get; set; }
    }
    public class ResponseClass
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<ParaTeacherList> ParaTeacherList { get; set; }
    }
}