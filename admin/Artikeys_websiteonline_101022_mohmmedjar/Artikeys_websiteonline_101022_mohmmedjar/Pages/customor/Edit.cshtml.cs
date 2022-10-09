using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Artikeys_websiteonline_101022_mohmmedjar.Pages.customor
{
    public class EditModel : PageModel
    {
        public CustomorInfo customorInfo = new CustomorInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Customor WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customorInfo.id = "" + reader.GetInt32(0);
                                customorInfo.name = "" + reader.GetString(1);
                                customorInfo.email = "" + reader.GetString(2);
                                customorInfo.phone = "" + reader.GetString(3);
                                customorInfo.address = "" + reader.GetString(4);
                                customorInfo.created_at = reader.GetSqlDateTime(5).ToString();

                            }
                        }
                    }

                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            customorInfo.id = Request.Form["id"];
            customorInfo.name = Request.Form["name"];
            customorInfo.email = Request.Form["email"];
            customorInfo.phone = Request.Form["phone"];
            customorInfo.address = Request.Form["address"];


            if (customorInfo.id.Length == 0 || customorInfo.name.Length == 0 || customorInfo.email.Length == 0 ||
                customorInfo.phone.Length == 0 || customorInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Customor " +
                        "SET nmae=@name, email=@email, phone=@phone, address=@address"+
                    "WHERE id=@id";

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
            catch(Exception ex)
            {
                errorMessage=ex.Message;
                return;
            }

            Response.Redirect("/customor/Index");
        }
    }
}
