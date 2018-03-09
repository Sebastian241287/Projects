using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static NoBD.Login;

namespace NoBD
{
    public partial class Chat : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!redir)
                {
                    Server.Transfer("Login.aspx");
                }
                else
                {
                    redir = false;
                }

                CurrentUser.Text = Session["UIme"].ToString();
                pagerefresh();

            }
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            ausers.Remove(CurrentUser.Text);
            Server.Transfer("Login.aspx");
        }

        protected void Send_Click(object sender, EventArgs e)
        {
            SqlDataSource1.InsertParameters["username"].DefaultValue = CurrentUser.Text;
            SqlDataSource1.InsertParameters["besedilo"].DefaultValue = CurrentUser.Text + ": " + Message.Text;
            SqlDataSource1.InsertParameters["cas"].DefaultValue = DateTime.Now.ToString("HH:mm tt");
            SqlDataSource1.Insert();
            Message.Text = "";
            pagerefresh();
        }

        protected void Refresh_Click(object sender, EventArgs e)
        {
            pagerefresh();

        }
        void pagerefresh()
        {
            Users.Items.Clear();
            foreach (string s in ausers)
            {
                Users.Items.Add(s);
            }
            Messages.DataBind();
        }
    }
}