using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Company
    {
       public System.Guid VehicleGuid { get; set; }
       public System.Guid CompanyGuid { get; set; }  //Requires company has been registered in advance
       public string CompanyVReg { get; set; }
       public string CompanyVDes { get; set; }
       public string CompanyVType { get; set; }
       public Nullable<bool> IsDeleted { get; set; }
       public Nullable<System.Guid> DeletedByGuid { get; set; }
       public Nullable<System.DateTime> DeletedDate { get; set; }
       public Nullable<System.DateTime> ModifiedDate { get; set; }
       public Nullable<System.Guid> Modifiedby { get; set; }
     //  public virtual Company Companies { get; set; }
    }
}
