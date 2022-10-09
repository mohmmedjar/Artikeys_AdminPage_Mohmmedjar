using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Artikeys_websiteonline_101022_mohmmedjar.Pages.customor
{
    public class CreateModel : PageModel
    {
        public CustomorInfo customorInfo = new CustomorInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            customorInfo.name = Request.Form["name"];
            customorInfo.email = Request.Form["email"];
            customorInfo.phone = Request.Form["phone"];
            customorInfo.address = Request.Form["address"];

            if (customorInfo.name.Length == 0 || customorInfo.email.Length == 0 ||
                customorInfo.phone.Length == 0 || customorInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the customor into the database

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "INSERT INTO customor " +
                        "(name, email, phone, address) VALUES" +
                        "(@name, @email, @phone, @address);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", customorInfo.name);
                        command.Parameters.AddWithValue("@email", customorInfo.email);
                        command.Parameters.AddWithValue("@phone", customorInfo.phone);
                        command.Parameters.AddWithValue("@address", customorInfo.address);

                        command.ExecuteNonQuery();

                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            customorInfo.name = ""; customorInfo.email = ""; customorInfo.phone = ""; customorInfo.address = "";
            successMessage = "New Customor Added Correctly";

            Response.Redirect("/customor/Index");
        }
    }
}
