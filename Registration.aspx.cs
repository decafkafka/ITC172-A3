using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        //instantiate classes
        ShowTrackerEntities ste = new ShowTrackerEntities();
        KeyCode k = new KeyCode();
        int seed = k.GetKeyCode(); //get the seed
        PasswordHash phash = new PasswordHash();

        try //try the code for errors
        {
            //get the hashed password
            byte[] pass = phash.HashIt(txtConfirm.Text, seed.ToString());

            //assign the values to the fields of the Fan Class
            Fan fanInfo = new Fan();
            fanInfo.FanName = txtName.Text;
            fanInfo.FanEmail = txtEmail.Text;
            fanInfo.FanDateEntered = DateTime.Now;
            ste.Fans.Add(fanInfo);

            FanLogin fanLoginInfo = new FanLogin();
            fanLoginInfo.Fan = fanInfo;
            fanLoginInfo.FanLoginUserName = txtUserName.Text;
            fanLoginInfo.FanLoginRandom = seed;
            fanLoginInfo.FanLoginPasswordPlain = txtConfirm.Text;
            fanLoginInfo.FanLoginHashed = pass;
            ste.FanLogins.Add(fanLoginInfo);

            ste.SaveChanges();
            lblErrorSuccess.Text = "You were successfully registered";
        }
        catch (Exception ex)
        {
            lblErrorSuccess.Text = ex.Message;
        }
    }
}