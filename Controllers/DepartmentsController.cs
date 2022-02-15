using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdventureWorksAPI.Models;
using AdventureWorksAPI.Models.Req;
using AdventureWorksAPI.Models.Ret;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace AdventureWorksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly AdventureWorks2019Context _context;
        private readonly ILogger<DepartmentsController> _logger;

        public DepartmentsController(AdventureWorks2019Context context, ILogger<DepartmentsController> logger)
        {
            _context = context;
            _logger = logger;
        }
        // POST: api/CusComplain
        [HttpPost("AddDepartment",Name = "AddDepartment")]
        public async Task<RetAddDepartment> AddDepartmentAsync([FromBody]ReqAddDepartment Req)
        {
            RetAddDepartment Retobj = new RetAddDepartment();

            bool CheckState = false;

            // 檢查傳入的警示訊息
            if (string.IsNullOrEmpty(Req.Name) || string.IsNullOrEmpty(Req.GroupName))
            {
                CheckState = true;
                Retobj.RetCode = "ER";
                Retobj.RetMsg = "訊息不得為空";
            }

            // 參數無誤
            if (!CheckState)
            {
                try
                {
                    var _Name = new SqlParameter("@Name", Req.Name);
                    var _GroupName = new SqlParameter("@GroupName", Req.GroupName);

                    var Ret_Result = await _context.AddDepartmentRet
                    .FromSqlInterpolated($"EXECUTE dbo.AddNewDepartment {_Name},{_GroupName} ").ToListAsync();


                    if (Ret_Result.FirstOrDefault() != null)
                    {

                        Retobj.RetCode = Ret_Result.FirstOrDefault().RetCode;
                        Retobj.RetMsg = Ret_Result.FirstOrDefault().RetMsg;
                    }
                    else
                    {
                        Retobj.RetCode = "99";
                        Retobj.RetMsg = "資料庫執行發生錯誤";

                    }

                }
                catch (Exception ex)
                {
                    Retobj.RetCode = "EX";
                    Retobj.RetMsg = "Exception Error: " + ex.Message;
                    _logger.LogError("Function: AddNoticeAsync, Exception Error: " + ex.ToString() + " -- Input: " + JsonConvert.SerializeObject(Req));

                }

            }



            return Retobj;
        
        }






    }
}
