using MobiOcean.MDM.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiOcean.MDM.BAL.Query
{
    public class websearch
    {
        Search sc;
        public DataTable GetHeader()
        {
            string Query = @"select * from website_menu where parent_id=1 and status='Publish' order by sort_order desc";
            sc = new Search();
            return sc.GetRecordsMySql(Query);
        }
        public DataTable GetSubHeader(string Id)
        {
            string Query = @"select * from website_menu where parent_id=" + Id + " and status='Publish' order by sort_order desc";
            sc = new Search();
            return sc.GetRecordsMySql(Query);
        }
        public DataTable GetChildHeader(string Id)
        {
            string Query = @"select * from website_menu where parent_id=" + Id + " and status='Publish' order by sort_order desc";
            sc = new Search();
            return sc.GetRecordsMySql(Query);
        }
        public DataTable GetFooter()
        {
            string Query = @"select * from product_demo";
            sc = new Search();
            return sc.GetRecordsMySql(Query);
        }
        public DataTable fetchDisplay(string statement)
        {
            string Query = @"select * from " + statement;
            sc = new Search();
            return sc.GetRecordsMySql(Query);
        }
        public string getValuefromTable(string fieldname, string statement, string key, string key_val)
        {
            string Query = @"select " + fieldname + " from " + statement + " where " + key + "='" + key_val + "'";
            sc = new Search();
            return sc.GetRecordsMySql(Query).Rows[0][fieldname].ToString();
        }
        public string option2(string id, string Url)
        {
            string html = "";
            DataTable dt6, dt7 = new DataTable();
            dt6 = fetchDisplay("website_menu where parent_id='" + id + "' and status='Publish' order by sort_order desc");
            if (dt6.Rows.Count > 0)
            {
                foreach (DataRow item in dt6.Rows)
                {
                    dt7 = fetchDisplay("website_menu where parent_id='" + item["id"] + "' and status='Publish' order by sort_order desc");
                    if (dt7.Rows.Count > 0)
                    {

                        html += @"<li class='dropdown'><a aria-haspopup='true' class='dropdown-toggle scroll disabled arialbold' data-toggle='dropdown' href='#'>" + item["name"].ToString().First().ToString().ToUpper() + item["name"].ToString().Substring(1) + @"<span class='caret'></span></a>
<ul class='dropdown-menu solution'>";
                        foreach (DataRow item1 in dt7.Rows)
                        {
                            html += @"<li><img src = 'Mobiocean/Content/images/app-managemant-hover.png' class='imagehiwi' /> <a class='arialbold' href=" + item1["page_url"] + @">" + item1["name"] + @"</a></li>";
                            // <li><a class='arialbold' href=" + item1["page_url"] + @"><" + item1["name"] + "</a></li>";
                        }
                        html += option1(item["id"].ToString(), Url);
                        html += "</ul></li>";
                    }
                    else
                    {
                        html += " <li><img src = 'Mobiocean/Content/images/app-managemant-hover.png' class='imagehiwi' /> <a class='arialbold' href='" + item["page_url"] + "'>" + item["name"] + @"</a></li>";
                        //"<li><a class='arialbold' href='" + item["page_url"] + "'>" + item["name"] + "</a></li>";
                    }
                }
            }
            return html;
        }
        public string option1(string id, string Url)
        {
            string html = "";
            DataTable dt6, dt7 = new DataTable();
            dt6 = fetchDisplay("website_menu where parent_id='" + id + "' and status='Publish' order by sort_order desc");
            if (dt6.Rows.Count > 0)
            {
                foreach (DataRow item in dt6.Rows)
                {
                    dt7 = fetchDisplay("website_menu where parent_id='" + item["id"] + "' and status='Publish' order by sort_order desc");
                    if (dt7.Rows.Count > 0)
                    {

                        html += @"<li class='dropdown'><a aria-haspopup='true' class='dropdown-toggle scroll disabled arialbold' data-toggle='dropdown' href='#'>" + item["name"].ToString().First().ToString().ToUpper() + item["name"].ToString().Substring(1) + @"<span class='caret'></span></a>
<ul class='dropdown-menu solution'>";
                        foreach (DataRow item1 in dt7.Rows)
                        {
                            html += @"<li><a class='arialbold' href=" + item1["page_url"] + @">" + item1["name"] + @"</a></li>";
                        }
                        html += option1(item["id"].ToString(), Url);
                        html += "</ul></li>";

                    }
                    else
                    {
                        html += "<li><a class='arialbold' href='" + Url + "'>" + item["name"] + @"</a></li>";

                    }
                }
            }
            return html;
        }
    }
}
