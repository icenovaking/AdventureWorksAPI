using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorksAPI.Models.Req
{
    public class ReqUpdDepartment
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
    }
}
