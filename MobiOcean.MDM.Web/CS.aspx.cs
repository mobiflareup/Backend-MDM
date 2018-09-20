using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class CS : Base
    {
        protected List<Email> Emails
        {
            get { return (List<Email>)ViewState["Emails"]; }
            set { ViewState["Emails"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Read_Emails();
            }
        }

        private void Read_Emails()
        {
            Pop3Client pop3Client;
            if (Session["Pop3Client"] == null)
            {
                pop3Client = new Pop3Client();
                pop3Client.Connect("us2.pop.mailhostbox.com", 110, false);
                pop3Client.Authenticate("support@mobiocean.com", "MobiOcean@123", AuthenticationMethod.UsernameAndPassword);
                Session["Pop3Client"] = pop3Client;
            }
            else
            {
                pop3Client = (Pop3Client)Session["Pop3Client"];
            }
            int count = pop3Client.GetMessageCount();
            this.Emails = new List<Email>();
            int counter = 0;
            for (int i = count; i >= 1; i--)
            {
                Message message = pop3Client.GetMessage(i);
                Email email = new Email()
                {
                    MessageNumber = i,
                    Subject = message.Headers.Subject,
                    DateSent = message.Headers.DateSent,
                    From = string.Format(message.Headers.From.Address),
                    Name = string.Format(message.Headers.From.DisplayName)
                };
                MessagePart body = message.FindFirstHtmlVersion();
                if (body != null)
                {
                    email.Body = body.GetBodyAsText();
                }
                else
                {
                    body = message.FindFirstPlainTextVersion();
                    if (body != null)
                    {
                        email.Body = body.GetBodyAsText();
                    }
                }
                List<MessagePart> attachments = message.FindAllAttachments();

                foreach (MessagePart attachment in attachments)
                {
                    email.Attachments.Add(new Attachment
                    {
                        FileName = attachment.FileName,
                        ContentType = attachment.ContentType.MediaType,
                        Content = attachment.Body
                    });
                }
                this.Emails.Add(email);
                counter++;
                if (counter > 2)
                {
                    break;
                }
            }
            gvEmails.DataSource = this.Emails;
            gvEmails.DataBind();
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Repeater rptAttachments = (e.Row.FindControl("rptAttachments") as Repeater);
                List<Attachment> attachments = this.Emails.Where(email => email.MessageNumber == Convert.ToInt32(gvEmails.DataKeys[e.Row.RowIndex].Value)).FirstOrDefault().Attachments;
                rptAttachments.DataSource = attachments;
                rptAttachments.DataBind();
            }
        }
        protected void Download(object sender, EventArgs e)
        {
            LinkButton lnkAttachment = (sender as LinkButton);
            GridViewRow row = (lnkAttachment.Parent.Parent.NamingContainer as GridViewRow);
            List<Attachment> attachments = this.Emails.Where(email => email.MessageNumber == Convert.ToInt32(gvEmails.DataKeys[row.RowIndex].Value)).FirstOrDefault().Attachments;
            Attachment attachment = attachments.Where(a => a.FileName == lnkAttachment.Text).FirstOrDefault();
            Response.AddHeader("content-disposition", "attachment;filename=" + attachment.FileName);
            Response.ContentType = attachment.ContentType;
            Response.BinaryWrite(attachment.Content);
            Response.End();
        }
    }
    [Serializable]
    public class Email
    {
        public Email()
        {
            this.Attachments = new List<Attachment>();
        }
        public int MessageNumber { get; set; }
        public string From { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateSent { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
    [Serializable]
    public class Attachment
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
