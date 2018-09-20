using HtmlAgilityPack;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.UI;

/// <summary>
/// Summary description for Base
/// </summary>
public class Base : System.Web.UI.Page
{
    DataTable dt;
    ClientBAL clientBal;
    ConstantBAL consbal;
    public DateTime CurrentDateTime1 = DateTime.UtcNow.AddMinutes(Constant.addMinutes);


    public Base()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DateTime GetCurrentDateTimeByUserId()
    {

        try
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            consbal = new ConstantBAL();
            return consbal.GetCurrentDateTimeByUserId(UserId);

        }
        catch (Exception)
        {
            CurrentDateTime1 = DateTime.UtcNow.AddMinutes(Constant.addMinutes);
            return CurrentDateTime1;
        }

    }
    public string GenPass(int Size = 6, string input = "")
    {
        Random myrandom = new Random();
        if (input == "")
            input = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ9876543210";
        string pass = "";
        try
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < Size; i++)
            {
                ch = input[myrandom.Next(0, input.Length)];
                builder.Append(ch);
            }
            pass = builder.ToString();
        }
        catch (Exception)
        {
            int myNum = myrandom.Next(10000000, 100000000);
            pass = "btyutyuuyerewvb" + myNum + "yhdguyfgd";
        }
        return pass;
    }

    protected override void OnPreInit(EventArgs e)
    {

        if (Session["UserName"] == null || Session["ClientId"] == null || Session["UserId"] == null || Session["Role"] == null)
        {
            Response.Redirect(Constant.MobiURL);
        }
        else
        {
            int res = ChkIsFirstLoginFromClient(Session["ClientId"].ToString());
            if (res == 1)
            {
                Response.Redirect("~/FirstLoginChanges");               
            }
            else if (res == 2)
            {
                Response.Redirect("~/Subscribe");
            }
        }
        if (CheckMenu(HttpContext.Current.Request.Url.LocalPath.ToString().TrimStart('/')) != 1)
        {
            Response.Redirect("authentication");
        }


        base.OnPreInit(e);
    }
    private int CheckMenu(string URL)
    {
        URL = URL.Replace("MobiOcean/", "");
        URL = URL.Replace("web/", "");
        if (URL.IndexOf('?') > 0)
        {
            URL = URL.Substring(0, URL.IndexOf('?') + 1);
        }
        if (!URL.Contains(".aspx"))
        {
            URL += ".aspx";
        }
        URL = URL.Replace("MobiOcean.MDM/", "");
        clientBal = new ClientBAL();
        clientBal.RoleId = Convert.ToInt32(Session["Role"]);
        clientBal.ClientId = Convert.ToInt32(Session["ClientId"]);
        clientBal.URL = URL;
        return clientBal.IsMenuByClientId();
    }
    private int ChkIsFirstLoginFromClient(string ClientId)
    {
        clientBal = new ClientBAL();
        dt = new DataTable();
        clientBal.ClientId = int.Parse(ClientId);
        dt = clientBal.GetClientByClientId();
        if (dt.Rows.Count > 0)
        {
            try
            {
                if (dt.Rows[0]["IsFirstLogin"].ToString() == "1")
                {
                    return 1;//First time login by Client
                }
                else
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ExpiryDate"].ToString()) && Convert.ToDateTime(dt.Rows[0]["ExpiryDate"].ToString()) > DateTime.UtcNow.AddMinutes(Constant.addMinutes))
                    {
                        return 3;// Allowed
                    }
                    else
                    {
                        return 2;//Expired
                    }

                }
            }
            catch (Exception)
            {
                return 3;
            }
        }
        return 2;
    }
}

public class ReportBase : Base
{
    protected void printCommand(string printMessage)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0,top=0,width=1000,height=600,status=0');");
        sb.Append("printWin.document.write(\"");
        sb.Append(printMessage);
        sb.Append("\");");
        sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        sb.Append("printWin.close();};");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
    }
    protected void pdfCreationCommand(string printMessage)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<!DOCTYPE html>");
        sb.Append("<html lang='en'>");
        sb.Append("<head>");
        sb.Append("<meta charset='utf-8' />");
        sb.Append("<meta http-equiv='X-UA-Compatible' content='IE=edge' />");
        sb.Append("<meta name='viewport' content='width=device-width, initial-scale=1' />");
        sb.Append("<meta name='description' content=''><meta name='author' content='' />");
        //sb.Append("<style>  table th,td { border - right - width: 0px;border - bottom - width: 0px;border - left - width: 0px;border - top - width: 0px;}</style>");


        sb.Append("<title></title></head><body>");
        sb.Append(printMessage);
        sb.Append("</body></html>");
        var fixedMarkup = FixBrokenMarkup(sb.ToString());
        //Document document = new Document(PageSize.A4, 50, 50, 25, 25);
        Document document = new Document(PageSize.A2, 10f, 10f, 10f, 10f);
        using (MemoryStream memoryStream = new MemoryStream())
        {
            #region-- Add watermark--
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            Watermark writerEvent = new Watermark("MobiOcean");

            writer.PageEvent = writerEvent;
            #endregion

            document.Open();
            using (var stringReader = new StringReader(fixedMarkup))
            {
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, stringReader);
            }
            document.Close();
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=AppLogsDetails.pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();
        }
    }

    private string FixBrokenMarkup(string broken)
    {
        HtmlDocument h = new HtmlDocument()
        {
            OptionAutoCloseOnEnd = true,
            OptionFixNestedTags = true,
            OptionWriteEmptyNodes = true
        };
        h.LoadHtml(broken);

        // UPDATED to remove HtmlCommentNode
        var comments = h.DocumentNode.SelectNodes("//comment()");
        if (comments != null)
        {
            foreach (var node in comments) { node.Remove(); }
        }

        return h.DocumentNode.SelectNodes("child::*") != null
            //                            ^^^^^^^^^^
            // XPath above: string plain-text or contains markup/tags
            ? h.DocumentNode.WriteTo()
            : string.Format("<span>{0}</span>", broken);
    }
}
public class SessionableControllerHandler : HttpControllerHandler, IRequiresSessionState
{
    public SessionableControllerHandler(RouteData routeData)
        : base(routeData)
    { }
}
public class SessionStateRouteHandler : IRouteHandler
{
    IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
    {
        return new SessionableControllerHandler(requestContext.RouteData);
    }
}
