using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Artikeys_websiteonline_101022_mohmmedjar.Pages.customor
{
    public class IndexModel : PageModel
    {
        public List<CustomorInfo> listcustomor = new List<CustomorInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM customor";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomorInfo customorInfo = new CustomorInfo();
                                customorInfo.id = "" + reader.GetInt32(0);
                                customorInfo.name = "" + reader.GetString(1);
                                customorInfo.email = "" + reader.GetString(2);
                                customorInfo.phone = "" + reader.GetString(3);
                                customorInfo.address = "" + reader.GetString(4);
                                customorInfo.created_at = reader.GetSqlDateTime(5).ToString();


                                listcustomor.Add(customorInfo);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

    }

    public class CustomorInfo 
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
            
    }
}
