using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
/// Summary description for IBaseContarct
/// </summary>
public interface IBaseinterface
{
    ArrayList ArrNamesI { get; set; }
    ArrayList ArrTypesI { get; set; }
    ArrayList ArrValuesI { get; set; }

    string InsertSPI { get; set; }
    string UpdateSPI { get; set; }
    string DeleteSPI { get; set; }
    string EditSPI { get; set; }
    string SelectAllSPI { get; set; }

    void ClearAll();
    void Setbase();
    void Setbase_Id();

}