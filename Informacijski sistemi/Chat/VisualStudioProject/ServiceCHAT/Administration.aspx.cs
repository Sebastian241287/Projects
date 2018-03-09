using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NoBD
{
    public partial class Administration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UIme"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
            refresh();
            /*ListBox1.Items.Clear();
            DataView totalMessages = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);

            for (int i = 0; i <= totalMessages.Count - 1; i++)
            {
                ListBox1.Items.Add(totalMessages[i][0].ToString() + ": " + totalMessages[i][1]);
            }        
            */
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            if (MUsername.Text != "")
            {
                DataView obstaja = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

                if (!((obstaja[0][0].ToString()).Equals(null)))
                {
                    SqlDataSource4.DeleteParameters["username"].DefaultValue = MUsername.Text;

                    SqlDataSource4.Delete();

                    SqlDataSource3.DeleteParameters["original_username"].DefaultValue = MUsername.Text;

                    SqlDataSource3.Delete();

                    Label1.Text = "Uporabnik uspešno zbrisan.";
                    /*ListBox1.Items.Clear();
                    DataView totalMessages = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);

                    for (int i = 0; i <= totalMessages.Count - 1; i++)
                    {
                        ListBox1.Items.Add(totalMessages[i][0].ToString() + ": " + totalMessages[i][1]);
                    }
                    */
                    refresh();
                }
                else
                {
                    Label1.Text = "Uporabnik ne obstaja.";
                }

            }
            else
            {
                Label1.Text = "Prosim dopolnite polje.";
            }
        }

        protected void admin_Click(object sender, EventArgs e)
        {
            if (MUsername.Text != "")
            {
                DataView obstaja = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

                //ListBox1.Items.Add(obstaja[0][0].ToString()+" "+ obstaja[0][1].ToString()+" "+ obstaja[0][2].ToString()+" "+ obstaja[0][3].ToString());

                if (!((obstaja[0][0].ToString()).Equals(null)))
                {
                    SqlDataSource5.UpdateParameters["username"].DefaultValue = MUsername.Text;
                    SqlDataSource5.UpdateParameters["admin"].DefaultValue = "1";
                    SqlDataSource5.Update();
                    Label1.Text = "Uporabnik " + MUsername.Text + " ima administratorske pravice.";
                }
                else
                {
                    Label1.Text = "Uporabnik ne obstaja.";
                }
            }
            else
            {
                Label1.Text = "Prosim dopolnite polje.";
            }

        }

        protected void odjava_Click(object sender, EventArgs e)
        {
            Session["UIme"] = null;
            Response.Redirect("Login.aspx");
        }

        public void refresh()
        {
            ListBox1.Items.Clear();
            DataView totalMessages = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);

            for (int i = 0; i <= totalMessages.Count - 1; i++)
            {
                ListBox1.Items.Add(totalMessages[i][0].ToString() + ": " + totalMessages[i][1]);
            }
        }
    }
}