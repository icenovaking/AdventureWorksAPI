using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorksAPI.Models.Ret
{
    public class RetAddDepartment
    {
        public string RetCode { get; set; }  // 回傳結果-代碼 00-執行完成、01-API KEY 錯誤、ER-其他錯誤說明
        public string RetMsg { get; set; }
    }
}
