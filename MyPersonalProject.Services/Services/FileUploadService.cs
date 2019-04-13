using MyPersonalProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalProject.Services.Services
{
    public class FileUploadService : IFileUploadService
    {
        private IDataProvider _dataProvider;

        public FileUploadService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Delete(int id)
        {
            _dataProvider.ExecuteNonQuery(
                "FileUpload_Delete",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", id);
                }
            );
        }
    }
}
