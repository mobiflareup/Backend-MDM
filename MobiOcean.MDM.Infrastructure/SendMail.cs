using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Threading.Tasks;



namespace MobiOcean.MDM.Infrastructure
{
    public class SendMail
    {

        MailMessage msg;
        SmtpClient smt;      
            

        public void SendEmail(string EmailTo, string subject, string msgBody, int College_Id,string infoEmail, string cc = "", string bcc = "")
        {
            msg = new MailMessage();
            smt = new SmtpClient();
            try
            {

                msg.From = new MailAddress(infoEmail);
                msg.To.Add(EmailTo);
                #region------ find cc -----
                if (cc.Trim() != "")
                {
                    string[] ccUsr = cc.Split(',');
                    for (int idx = 0; idx < ccUsr.Length; idx++)
                    {
                        msg.CC.Add(ccUsr[idx].ToString());
                    }
                }
                #endregion

                #region------ find bcc -----
                if (bcc.Trim() != "")
                {
                    string[] bccUsr = bcc.Split(',');
                    for (int idx = 0; idx < bccUsr.Length; idx++)
                    {
                        msg.Bcc.Add(bccUsr[idx].ToString());
                    }
                }
                #endregion


                msg.Subject = subject;
                msg.Body = msgBody.Replace("\n", "<br>");
                msg.IsBodyHtml = true;
                smt.Host = "dedrelay.secureserver.net";//"relay-hosting.secureserver.net";//"us2.smtp.mailhostbox.com"; //           
                smt.Port = 25;
                smt.Credentials = new System.Net.NetworkCredential(infoEmail, "Bimlesh@123");
                smt.Send(msg);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                msg = null;
                smt = null;
            }
        }
        public void SendEmailWithAttachment(string EmailTo, string subject, string msgBody, int College_Id, byte[] invoiceFile ,string infoEmail, string cc = "", string bcc = "")
        {
            try
            {
                MailMessage mail = new MailMessage();
                smt = new SmtpClient();
                mail.From = new MailAddress(infoEmail);                
                mail.To.Add(EmailTo);
                #region------ find cc -----
                if (!string.IsNullOrEmpty(cc))
                {
                    string[] ccUsr = cc.Trim().Split(',');
                    for (int idx = 0; idx < ccUsr.Length; idx++)
                    {
                        mail.CC.Add(ccUsr[idx].ToString());
                    }
                }
                #endregion

                #region------ find bcc -----
                if (!string.IsNullOrEmpty(bcc))
                {
                    string[] bccUsr = bcc.Trim().Split(',');
                    for (int idx = 0; idx < bccUsr.Length; idx++)
                    {
                        mail.Bcc.Add(bccUsr[idx].ToString());
                    }
                }
                #endregion
                mail.Subject = subject;
                mail.Body = msgBody;
                mail.Body = msgBody.Replace("\n", "<br>");
                mail.IsBodyHtml = true;
                mail.Attachments.Add(new Attachment(new MemoryStream(invoiceFile), "Invoice.pdf"));               
                smt.Host = "dedrelay.secureserver.net";//"relay-hosting.secureserver.net";//"us2.smtp.mailhostbox.com"; //           
                smt.Port = 25;
                smt.Credentials = new System.Net.NetworkCredential(infoEmail, "Bimlesh@123");
                smt.Send(mail);
//SmtpClient client = new SmtpClient();
                //client.Port = 587;
                //client.Host = "smtp.gmail.com";
                //client.EnableSsl = true;
                //client.Timeout = 10000;
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
               // client.UseDefaultCredentials = false;
                //client.Credentials = new System.Net.NetworkCredential("gingerboxmobility01@gmail.com", "saral654321");
                //client.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
