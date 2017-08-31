using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileUpLoadMVC.Controllers
{
    public class FileUploadController : Controller
    {
        private string con =
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\jeevan\my\projects\FileUpLoadMVC\FileUpLoadMVC\App_Data\Database1.mdf;Integrated Security=True";
        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void UploadFiles()
        {
            if (!(Request.Files?.Count > 0)) return;
            var filesCount = Request.Files.Count;
            try
            {
                for (int i = 0; i < filesCount; i++)
                {
                    var file = Request.Files[i];
                    var fileName = Path.GetFileName(file?.FileName);
                    if (fileName != null)
                    {
                      
                        var fileBytes = new byte[file.InputStream.Length];
                        file.InputStream.Read(fileBytes, 0, fileBytes.Length);
                        file.InputStream.Close();

                        var cmdStr = "insert into table1(col1, filename) values(@val,@filename)";
                        using (var connection = new SqlConnection(con))
                        {
                            connection.Open();
                            var cmd = new SqlCommand(cmdStr, connection);
                            cmd.Parameters.AddWithValue("@val", fileBytes);
                            cmd.Parameters.AddWithValue("@filename", fileName);
                            cmd.ExecuteNonQuery();

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}