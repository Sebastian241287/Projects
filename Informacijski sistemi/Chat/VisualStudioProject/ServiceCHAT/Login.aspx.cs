using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NoBD
{
    public partial class Login : System.Web.UI.Page
    {
        public static bool redir = false;
        public static HashSet<string> ausers = new HashSet<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UIme"] = null;
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {

            using (MD5 md5Hash = MD5.Create())
            {
                string password = GetMd5Hash(md5Hash, PPassword.Text);
                PPassword.Text = password;
            }

            DataView obstaja = (DataView)SqlDataSource6.Select(DataSourceSelectArguments.Empty);
            if (!(obstaja[0][0].ToString()).Equals("0"))
            {
                Session["UIme"] = PUsername.Text;
                ausers.Add(Session["UIme"].ToString());
                redir = true;
                Response.Redirect("Chat.aspx");
            }
            else
            {
                PError.Text = "Uporabniško ime in geslo se ne ujemata, vnesite znova.";
                PUsername.Text = "";
                PPassword.Text = "";
            }

        }

        protected void SignInBtn_Click(object sender, EventArgs e)
        {
            bool corrPassw = false;
            bool corrName = false;
            RError.Text = "";

            SqlDataSource4.InsertParameters["ime"].DefaultValue = RIme.Text;
            SqlDataSource4.InsertParameters["priimek"].DefaultValue = RPriimek.Text;
            SqlDataSource4.InsertParameters["username"].DefaultValue = RUsername.Text;
            SqlDataSource4.InsertParameters["admin"].DefaultValue = "0";
            DataView obstaja = (DataView)SqlDataSource5.Select(DataSourceSelectArguments.Empty);

            if (RUsername.Text.Equals(""))
                RError.Text = RError.Text + "Vstavite uporabniško ime ";
            else if (!(obstaja[0][0].ToString()).Equals("0"))
            {
                RError.Text = RError.Text + "Uporabnik s tem imenom ze obstaja, prosimo uporabite drugega. ";
            }
            else
            {
                corrName = true;
            }

            if (RGeslo1.Text.Equals(RGeslo2.Text) && Regex.IsMatch(RGeslo1.Text, @"(?=.*[A-Z].*[A-Z])(?=.*[0-9].*[0-9])(?=.*[?.*!:]+).{8,}"))
            {

                corrPassw = true;
                using (MD5 md5Hash = MD5.Create())
                {
                    string password = GetMd5Hash(md5Hash, RGeslo1.Text);
                    SqlDataSource4.InsertParameters["geslo"].DefaultValue = password;
                }
            }
            else
            {
                RError.Text = RError.Text + "Gesla se ne ujameta ali ali ni veljavno geslo, vnesite znova ";
                RGeslo1.Text = "";
                RGeslo2.Text = "";
            }
            if (corrName && corrPassw)
            {
                SqlDataSource4.Insert();
                RError.Text = "Vaš uporabniški račun je bil uspešno ustvarjen!";
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (PUsername.Text != "" && PPassword.Text != "")
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    string password = GetMd5Hash(md5Hash, PPassword.Text);
                    PPassword.Text = password;
                }
                DataView obstaja = (DataView)SqlDataSource6.Select(DataSourceSelectArguments.Empty);

                if (!(obstaja[0][0].ToString()).Equals("0"))
                {
                    DataView isAdmin = (DataView)SqlDataSource7.Select(DataSourceSelectArguments.Empty);

                    if (((isAdmin[0][0].ToString()).Equals("1")))
                    {
                        Session["UIme"] = PUsername.Text;
                        Response.Redirect("Administration.aspx");

                    }
                    else
                    {
                        PError.Text = "Nimate administratorske pravice";
                    }
                }
                else
                {
                    PError.Text = "Ne obstaja user.";
                }
            }
            else
            {
                PError.Text = "Dopolnite obadva polja.";
            }

        }
    }
}