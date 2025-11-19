
function ValidateDate(objMonth,objDay,objYear)
{
//created on: 11th May 2000 
//Programmer: Gaurav Joshi
//Purpose	: This funtion is used to validate a particular date. 
//Arguments : Month Day and Year objects respectively.

if ((objMonth.value=="01") || (objMonth.value==1) || (objMonth.value=="03") || (objMonth.value==3) || (objMonth.value=="05") || (objMonth.value==5) || (objMonth.value=="07") || (objMonth.value==7) || (objMonth.value=="08") || (objMonth.value==8) || (objMonth.value==10) || (objMonth.value==12))
{
	if (objDay.value > 31) 
		{
		return false;
		}
	else
		{	
		return true;
		}
}
else if ((objMonth.value=="04") || (objMonth.value==4) || (objMonth.value=="06") || (objMonth.value==6) || (objMonth.value=="09") || (objMonth.value==9) || (objMonth.value==11))
{
	if (objDay.value > 30) 
		{
			return false;
		}
	else
		{
			return true;
		}
}												
else if ((objMonth.value=="02") || (objMonth.value==2))
{
	if (((objYear.value % 4) == 0) && (objDay.value > 29))
		{	
			return false;
		}
	else if (((objYear.value % 4) != 0) && (objDay.value > 28))
		{	
			return false;
		}
	else
		{
			return true;
		}		
}							

}

function ValidateEMail(objName)
{
	//created on: 11th May 2000 
	//Programmer: Manisha Sethi	
	//Purpose	: This function is used to validate email. 
	//Arguments : Email object
		
	var sobjValue;
	var iobjLength;
	
	sobjValue=objName.value;
	iobjLength=sobjValue.length;
	iFposition=sobjValue.indexOf("@");
	iSposition=sobjValue.indexOf(".");
	iTmp=sobjValue.lastIndexOf(".");	
	iPosition=sobjValue.indexOf(",");
	iPos=sobjValue.indexOf(";");
	iSpacePos=sobjValue.indexOf(" ");
	
	if (iobjLength!=0)
	{
		if ((iFposition == -1))
		{

			alert("Please enter the E-Mail address in the proper format")
			eval(objName.focus());
			return false;
		}
		else if(sobjValue.charAt(0) == "@" )
		{
	
			alert("Please enter the E-Mail address in the proper format")
			eval(objName.focus());
			return false;				
		}
		else if(sobjValue.charAt(iobjLength) == "@" )
		{
		
			
			alert("Please enter the E-Mail address in the proper format");
			eval(objName.focus());
			return false;				
		}	
		else if((sobjValue.indexOf("@",(iFposition+1)))!=-1)
		{	
		   		
			alert("Please enter the E-Mail address in the proper format")
			eval(objName.focus());
			return false;
		}
		
		else if ((iPosition!=-1) || (iPos!=-1))
		{
		   	
			alert("Please enter the E-Mail address in the proper format")
			eval(objName.focus());
			return false;
		}
		else if (iSpacePos!=-1) 
		{
		    		
			alert("Kindly remove space characters from the E-Mail address")
			eval(objName.focus());
			return false;
		}
		else
		{
			return true;
		}		
	}		
}

function ValidateEMail1(sStr, objName)
{
	//created on: 11th May 2000 
	//Programmer: Manisha Sethi	
	//Purpose	: This function is used to validate email. 
	//Arguments : Email object
		
	var sobjValue;
	var iobjLength;
	
	sobjValue=sStr;
	iobjLength=sobjValue.length;
	iFposition=sobjValue.indexOf("@");
	iSposition=sobjValue.indexOf(".");
	iTmp=sobjValue.lastIndexOf(".");	
	
	if (iobjLength!=0)
	{
		if ((iFposition == -1)||(iSposition == -1))
		{
			alert("Please enter the E-Mail address in the proper format")
			eval(objName.focus());
			return false;
		}
		else if(sobjValue.charAt(0) == "@" || sobjValue.charAt(0)==".")
		{
			alert("Please enter the E-Mail address in the proper format")
			eval(objName.focus());
			return false;				
		}
		else if(sobjValue.charAt(iobjLength) == "@" || sobjValue.charAt(iobjLength)==".")
		{
			alert("Please enter the E-Mail address in the proper format");
			eval(objName.focus());
			return false;				
		}	
		else if((sobjValue.indexOf("@",(iFposition+1)))!=-1)
		{	
			alert("Please enter the E-Mail address in the proper format")
			eval(objName.focus());
			return false;
		}
		else if ((iobjLength-(iTmp+1)<2)||(iobjLength-(iTmp+1)>3))
		{
			alert("Please enter the E-Mail address in the proper format")
			eval(objName.focus());
			return false;
		}
		else
		{
			return true;
		}		
	}		
}





function SearchQuery(objForm)
{
//created on: 12 June 2000
//Programmer: Gaurav Joshi
//Purpose	: This funtion is used to validate the search criteria entered is valid.
//Arguments : form object as the argument 
	if(objForm.TR_Search.value.indexOf('"') != -1) 
	{
		alert("Remove Double Quotes (" + '"' +")");
		eval("objForm.TR_Search.focus()");
	}
	if(objForm.TR_Search.value.indexOf("'") != -1) 
	{
		alert("Remove Single Quote (')");
		eval("objForm.TR_Search.focus()");
	}
	//*********************Changed by sandeep****************
	if (objForm.TR_Search.value.length>255)
	{	
		alert("The length of the text to be searched cannot exceed 255 characters")
		eval("objForm.TR_Search.focus()");
	}
	else
	{					
	if (objForm.TR_Search.value == ""){
		alert("Please enter the value for the required  field");
		eval("objForm.TR_Search.focus()");
		}
	else if((objForm.TR_Search.value.indexOf('"') == -1) && (objForm.TR_Search.value.indexOf("'") == -1))
	{
		objForm.action = "../../EMarketPlace/Sitesearch/SiteSearchResult.asp?strSearch="+objForm.TR_Search.value+"&Zone="+objForm.HID_CalledFrom.value; 
		objForm.submit()

	}
	//*******************ends here*********************** 
	}
}



function validate(oForm)
{
	

	var iCounter=0
	var sFldval,sFldname,sFldType
	var iLength
	var intLoop
	var intStatus=0
	iLength=oForm.elements.length


	while(iCounter<iLength)
	{
	
		sFldval=oForm.elements[iCounter].value;
		sFldname=oForm.elements[iCounter].id;
		
		if (sFldval != null){
		sFldlen=sFldval.length
		sFldType = sFldname.substring(0,3);
		sFldType = sFldType.toUpperCase();
		
		if (sFldval == "-88888")
		{
		alert("Kindly choose a value for the required field");
		eval("sFldval=oForm."+ sFldname+".focus()");
		return false;
		}
		
		if (sFldType !="CMD" && sFldType!="HID" && sFldType != "SEL")
		{
					
			if(sFldname.charAt(1) == "R" && sFldval.length==0)
				{
					alert("Please enter value for the required text field")
					eval("sFldval=oForm."+ sFldname+".focus()");
					return false;				
				}
				
			if((sFldname.charAt(1) == "R" || sFldname.charAt(1) == "N") && sFldval.length > 0)
			{
				for (intLoop=0;intLoop!=sFldval.length;++intLoop)
					{
						if(sFldval.charAt(intLoop)!=" ")
						{
							intStatus=0;
							break;
						}
						else if(sFldval.charAt(intLoop)==" ")
								intStatus=1;
					}
				if(intStatus==1)
					{
						alert("Space characters entered are not valid") 
						eval("sFldval=oForm."+sFldname+".focus()");
						return false;						
					}
			}				
			
			//***********************************************
			// Changes start by sandeep on 23/6/2003
			// Description: Email validation eg. ER_Email
			//              For Allowing multiple Email validation,
			//              provide field name like- ER_M_Email, where
			//              'M_' stands for 'Multiple' Email
			//***********************************************

			if(sFldname.charAt(0)=="E" && isNaN(sFldval))
			{
				var objName
				var sobjValue=new Array;
				var iobjLength;
				var tValue;
				var tCount;
				var strmsg;
				
				objName = oForm.elements[iCounter]

				if (sFldname.substring(3,5)=="M_")
				{
					sobjValue=objName.value.split(",");
					strmsg = "One of the E-Mail address provided is not in the proper format";
				}
				else
				{
					sobjValue[0]=objName.value;
					strmsg = "Please enter the E-Mail address in the proper format";
				}

				for (i=0; i<sobjValue.length; i++)
				{
					iobjLength=sobjValue[i].length;
					iFposition=sobjValue[i].indexOf("@");
					iSposition=sobjValue[i].indexOf(".");
					iTmp=sobjValue[i].lastIndexOf(".");	
					iPosition=sobjValue[i].indexOf(",");
					iPos=sobjValue[i].indexOf(";");
					iSpacePos=sobjValue[i].indexOf(" ");
					
					if (iobjLength!=0)
					{
						if ((iFposition == -1))
						{
							alert(strmsg)
							eval(objName.focus());
							return false;
						}
						else if(sobjValue[i].charAt(0) == "@" )
						{
							alert(strmsg)
							eval(objName.focus());
							return false;				
						}
						else if(sobjValue[i].charAt(iobjLength) == "@" )
						{
							alert(strmsg);
							eval(objName.focus());
							return false;				
						}	
						else if((sobjValue[i].indexOf("@",(iFposition+1)))!=-1)
						{	
							alert(strmsg);
							eval(objName.focus());
							return false;
						}
						else if ((iPosition!=-1) || (iPos!=-1))
						{
							alert(strmsg);
							eval(objName.focus());
							return false;
						}
						else if (iSpacePos!=-1) 
						{
							alert("Kindly remove space characters from the E-Mail address")
							eval(objName.focus());
							return false;
						}
					} // if
				} // for
			} // main if	
			//***********************************************			
			// Changes end by sandeep
			//***********************************************
				
			if(sFldname.charAt(0)=="M" && isNaN(sFldval))
				{
					alert("Enter valid numeric value") 
					eval("sFldval=oForm."+sFldname+".focus()");
					return false;
				}
			if(sFldname.charAt(0)=="M" && !isNaN(sFldval))
				{
					if(parseFloat(sFldval) <0 || parseFloat(sFldval) > parseFloat(922337203685477.5808))
					{
						alert("Enter  value in range 0 - 922337203685477.5808") 
						eval("sFldval=oForm."+sFldname+".focus()");
						return false;
					}
				}			
					
			if(sFldname.charAt(0) == "I" && isNaN(sFldval))
				{
					alert("Enter valid numeric value integer") 
					eval("sFldval=oForm."+sFldname+".focus()");
					return false;
				}
			if(sFldname.charAt(0) == "I" &&  !isNaN(sFldval))
				{			
					if(parseInt(sFldval) <0 || parseFloat(sFldval) > parseFloat(2147483647))
						{
							alert("Enter  value in range 0 - 2147483647") 
							eval("sFldval=oForm."+sFldname+".focus()");
							return false;
						}
				}
			if(sFldname.charAt(0) == "F" && isNaN(sFldval))
				{
					alert("Enter valid numeric value float") 
					eval("sFldval=oForm."+sFldname+".focus()");
					return false;
				}
			if(sFldname.charAt(0) == "F" &&  !isNaN(sFldval))
				{			
					if(parseFloat(sFldval) <0 )
						{
							alert("Enter valid value" ) 
							eval("sFldval=oForm."+sFldname+".focus()");
							return false;
						}
				}
			if(sFldname.charAt(0) == "Y" && isNaN(sFldval))
				{
					alert("Enter valid value ") 
					eval("sFldval=oForm."+sFldname+".focus()");
					return false;
				}
			if(sFldname.charAt(0) == "Y" &&  !isNaN(sFldval))
				{			
					if(parseInt(sFldval) <=0 || parseInt(sFldval) > parseInt(255))
						{
							alert("Enter valid  value in range 1 - 255") 
							eval("sFldval=oForm."+sFldname+".focus()");
							return false;
						}
				}
			if(sFldname.charAt(0) == "S" && isNaN(sFldval))
				{
					alert("Enter valid numeric value ") 
					eval("sFldval=oForm."+sFldname+".focus()");
					return false;
				}
			
			if(sFldname.charAt(0) == "S" && !isNaN(sFldval))
				{			
					if(parseInt(sFldval) <= 0  || parseInt(sFldval) > 32767)
						{
							alert("Enter valid value in range 1 - 32767") 
							eval("sFldval=oForm."+sFldname+".focus()");
							return false;
						}
				}
			if(sFldname.charAt(0) == "D" && (sFldval.length > 0)&& (sFldval!=" ")) 	
				{
					var str=sFldval.replace(/-/g,"/");
					if (isNaN(Date.parse(str)) )
					{
						alert("Please Enter a Valid Date");
						eval("sFldval=oForm."+sFldname+".focus()");
						return false;
					}
					else
					{
						if(parseInt(str.length) <6 )
							{
								alert("Enter a Valid Date ");
								eval("sFldval=oForm."+sFldname+".focus()");
								return false;
							}
						else
							if(parseInt(str.Length)>10)
							{
								alert("Enter Valid Date");
								eval("sFldval=oForm."+sFldname+".focus()");
								return false;
							}
					} 
		//************************Change By Rajeev ***************************

					if (DateCheck(oForm, sFldname) == false){
						//eval("sFldval=oForm."+sFldname+".focus()");
						return false;
						}
		//*************************End Change *********************************		
				}
				
		//*********************************************************************				
		//************************Changed by Gaurav on 22/04/2000 to validate time*******		
		//************************Changed by Minal on 01/04/2004 to validate time better*******
		
			if(sFldname.charAt(0) == "K" && (sFldval.length > 0)&& (sFldval!=" ")) 	
				{
					var str=sFldval;
					var str1,str2,strcolon;
					strcolon = str.substring(2,3);
					str1 = str.substring(0,2);
					str2 = str.substring(3,5);
					str = str1 + str2;
					
						if (strcolon!=":")
							{
								alert("Please Enter a Valid Time like (01:30,23:00)");
								eval("sFldval=oForm."+sFldname+".focus()");
								return false;
							}
						if((parseInt(str.length) <4) || (parseInt(str.length) >4))
							{
								alert("Please Enter a Valid Time like (01:30,23:00)");
								eval("sFldval=oForm."+sFldname+".focus()");
								return false;
							}
						if (isNaN(str))
							{
								alert("Please Enter a Valid Time like (01:30,23:00)");
								eval("sFldval=oForm."+sFldname+".focus()");
								return false;
							}
							if (parseInt(str.length)==4)
							{
								if (parseInt(str1)>23)
								{
									alert("Please Enter a Valid Time like (01:30,23:00)");
									eval("sFldval=oForm."+sFldname+".focus()");
									return false;
								}
								if (parseInt(str2)>60)
								{
									alert("Please Enter a Valid Time like (01:30,23:00)");
									eval("sFldval=oForm."+sFldname+".focus()");
									return false;
								}
							}
	
				}
			else if ((sFldname.charAt(0) == "K") && (sFldval.charAt(0)==" "))
				{
					alert("Please Enter a Valid Time like (01:30,23:00)");
					eval("sFldval=oForm."+sFldname+".focus()");
					return false;
				}		
		//*************************End Change *********************************
		//*********************************************************************				
				
				
				
			if(sFldname.charAt(0) == "T")
				{
					sFldval = sFldval.replace(/'/gi,"`")
					oForm.elements[iCounter].value = sFldval 
					sFldval = sFldval.replace(/"/gi,"`")
					oForm.elements[iCounter].value = sFldval 
					if(sFldval.indexOf('"') != -1) 
					{
						alert("Remove Double Quotes (" + '"' +")");
						eval("sFldval=oForm."+sFldname+".focus()");
						return false;
					}
					if(sFldval.indexOf("'") != -1) 
					{
						alert("Remove Single Quote (')");
						eval("sFldval=oForm."+sFldname+".focus()");
						return false;
					}
					/*if(sFldval.indexOf("\\") != -1) 
					{
						alert("Remove back slash (" + '\\' + ")");
						eval("sFldval=oForm."+sFldname+".focus()");
						return false;
					}*/
					
					//********************************change by sandeep****************************
														
					
					//********************************change by sandeep****************************TR_C_05000_Name
					
					if ((oForm.elements[iCounter].type)== "textarea")
					{
						if (oForm.elements[iCounter].id.substring(3,5)=="C_")
						{
							if (oForm.elements[iCounter].value.length > parseFloat(oForm.elements[iCounter].id.substring(5,10)))
							{
								alert("This field cannot exceed " + parseFloat(oForm.elements[iCounter].id.substring(5,10)) + " characters")
								eval("sFldval=oForm."+sFldname+".focus()");
								return false;
							}
						}
						else
						{
							if (oForm.elements[iCounter].value.length > 250)
							{
							alert("This field cannot exceed 250 characters")
							eval("sFldval=oForm."+sFldname+".focus()");
							return false;
							}
						}
						if (oForm.elements[iCounter].value.indexOf('"') != -1)
						{
						alert("Remove Double Quotes (" + '"' +")");
						eval("sFldval=oForm."+sFldname+".focus()");
						return false;
						}
						if (oForm.elements[iCounter].value.indexOf("'") != -1)
						{
						alert("Remove Single Quote (')");
						eval("sFldval=oForm."+sFldname+".focus()");
						return false;
						}
						/*if(sFldval.indexOf("\\") != -1) 
						{
						alert("Remove back slash (" + '\\' + ")");
						eval("sFldval=oForm."+sFldname+".focus()");
						return false;
						}*/
						
						sFldval=oForm.elements[iCounter].value;
						sFldname=oForm.elements[iCounter].id;
						if (sFldval != null)
						{
						sFldlen=sFldval.length
						sFldType = sFldname.substring(0,3);
						sFldType = sFldType.toUpperCase();
						if (sFldType !="CMD" && sFldType!="HID" && sFldType != "SEL")
						{
						if(sFldname.charAt(1) == "R" && sFldval.length==0)
							{
								alert("Please enter value for the required field")
								eval("sFldval=oForm."+ sFldname+".focus()");
								return false;				
							}
						}
						}									
					}
					
					//****************************************
					
					//********************************ends here************************************				
				} 	
			}
		}			
		
		// CODE BY: SANDEEP
		// DESCRIPTION: CHECK FOR REQUIRED SELECT ITEM IN MULTIPLE SELECTION LISTBOX
		// DATE: 1 DEC 2004
		// FOR REQUIRED    : SELR_
		// FOR NOT REQUIRED: SEL_
		if (oForm.elements[iCounter].id.substring(0,5)=="SELR_")
		{
			f=0;
			for (i=0;i<oForm.elements[iCounter].length;i++)
			{
				if (oForm.elements[iCounter].item(i).selected)
					f=1;
			}
			if (f==0)
			{
				alert("Please select item from the list");
				eval("sFldval=oForm."+sFldname+".focus()");
				return false;
			}
		}
		// CODE END: SANDEEP
		
		iCounter=iCounter+1;
		
	}
	
	
//Code 	 bye mayank
/*	for( i=0;i<document.frmForm.elements.length;i++)
	{
	
	if (document.frmForm.elements[i].type == "select-one")
	{ 
		if (window.document.frmForm.elements[i].options[window.document.frmForm.elements[i].selectedIndex].value == "--0--" )
		{
			alert("Please select the option");
			window.document.frmForm.elements[i].focus();
			return false;
		}
		
	}
}	*/

/*for( i=0;i<document.frmForm.elements.length;i++)
{
	
	if (document.frmForm.elements[i].type == "text")
	{
		if (window.document.frmForm.elements[i].value.length>250) 
		{
			alert("Length of the string should be less than or equal to 250");
			window.document.frmForm.elements[i].focus();
			return false;
		}
		
	}
	
}*/	

	
		return true;
}

	


function fldValValidator(oForm)
{
if (validate() == true){
  var checkOK = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzÉäåöúü¿¡¬√ƒ≈∆«»… ÀÃÕŒœ–—“”‘’÷ÿŸ⁄€‹›ﬁﬂ‡·‚„‰ÂÊÁËÈÍÎÏÌÓÔÒÚÛÙıˆ¯˘˙˚¸˝˛0123456789_";
  var checkStr = oForm.TR_newFld1.value;
  var allValid = true;
  for (i = 0;  i < checkStr.length;  i++)
  {
    ch = checkStr.charAt(i);
    for (j = 0;  j < checkOK.length;  j++)
      if (ch == checkOK.charAt(j))
        break;
    if (j == checkOK.length)
    {
      allValid = false;
      break;
    }
  }
  if (!allValid)
  {
    alert("Please enter only letter, digit and \"_\" characters in the field.");
    oForm.TR_newFld1.focus();
    return (false);
  }
  return (true);
  }
  else
  {
  return false;
  }
  
}


function ChkBoxvalues  (sMode)
{
	var iCounter, iField, iTotalForms, iTotalFields, oCtl, sName ;

	iTotalForms = document.forms.length ;
	for (iCounter = 0; iCounter < iTotalForms ; iCounter++)
	{
		iTotalFields = document.forms[iCounter].elements.length ;
		for (iField = 0; iField < iTotalFields; iField++)
		{
			oCtl = document.forms[iCounter].elements[iField] ;
			if (oCtl.type.toLowerCase() == "checkbox")
			{
				sName = "BR_" + oCtl.id .substring(4) ;
				if (sMode == "S")
				{
					if (oCtl.checked == true)
						document.forms[iCounter].elements[sName].value = "1" ;
					else
						document.forms[iCounter].elements[sName].value = "0" ;
				}
				else
				{
					if (document.forms[iCounter].elements[sName].value == "1")
						oCtl.checked = true ;
					else
						oCtl.checked = false ;
				}
			}
		}
	}
	return ;
}
//Date Check Function *******************Start  add by Rajeev on 09/16/1999*********************
// x Field name

function DateCheck(oForm, x){
	
	var sDate;
	var iDateLength
	var iDay
	var iMonth
	var iYear
	var iFirst
	var iSecond
	var bLeapYear
	var sMessage
	sMessage = "Enter valid Date in mm/dd/yyyy format."
	sDate = oForm.elements[x].value
	iDateLength=sDate.length
	
	if (iDateLength < 6) 
		{
		alert (sMessage)
		return false;
		}
	else 
		{
		iFirst=sDate.indexOf("/")
		if (iFirst=="1")
			{
			iMonth="0" + sDate.substring(0,1)
			sDate = sDate.substring(2,iDateLength)
		   	}
		else if (iFirst=="2")
			{
			iMonth=sDate.substring(0,2)	
			sDate = sDate.substring(3,iDateLength)
			}
		else
			{
			alert (sMessage)
			return false;
			}
			
		iSecond=sDate.indexOf("/")
		iDateLength=sDate.length

		if (iSecond=="1")
			{
			iDay="0" + sDate.substring(0,1)
			iYear = sDate.substring(2,iDateLength)
		   	}
		else if (iSecond=="2")
			{
			iDay=sDate.substring(0,2)	
			iYear = sDate.substring(3,iDateLength)
			}
		else
			{
			alert (sMessage)
			return false;
			}
		if(iYear.length =="2") 
			{
			if (iYear < 80)	
				{
				iYear="20"+iYear
				}
			else
				{
				iYear="19"+iYear
				}

			}
		else if (iYear.length=="4")
			{
			iYear= iYear
			}
		else
			{
			alert (sMessage)
			return false;
			}  	
		}
//check for full date
// Leap Year Check
//iYear = parseInt(iYear)
//iMonth = parseInt(iMonth)
//iDay = parseInt(iDay)

if (iYear>1800)
	{	
	if (iYear % 4 ==0) 
		{
		  bLeapYear = true
		}
	else
		{
		 bLeapYear = false
		}			
	}
else
	{
	alert (sMessage)
	return false;
	}

//Month and day Check
if ((iMonth < 13) && (iDay < 32))
	{
	if (((iMonth=='04') || (iMonth=='06') || (iMonth=='09') || (iMonth==11)) && (iDay >30))
		{
		alert (sMessage)
		return false;
		}
	else if ((iMonth=='02') && (bLeapYear==true) && (iDay>29)) 
		{
		alert (sMessage)
		return false;
		}
	else if ((iMonth=='02') && (bLeapYear==false) && (iDay>28))
		{
		alert (sMessage)
		return false;
		}
	}
else

	{
	alert (sMessage)
	return false;
	}
	
// Check End
sDate=iMonth+'/'+iDay+'/'+iYear
oForm.elements[x].value=sDate

return true;
}



function CompareDates(oForm,objDateTo,objDateFrom)
{
// to compare the date entered by the user with the current date. The date shouldn't be less 
// then current date
	var iYearFrom;
	var iMonthFrom;
	var iDateFrom;
	var iYearTo;
	var iMonthTo;
	var iDateTo;
	var sDelimitor
	var strDateFrom;
	var strDateTo;
	
	strDateFrom = objDateFrom.value;
	strDateTo = objDateTo.value;
	
	sDelimitor='/';
	iMonthFrom = strDateFrom.substring(0, 2); //extracts the month
	iDateFrom = strDateFrom.substring(3, 5); //extracts the date
	iYearFrom =  strDateFrom.substring(6, 10);  //extracts the Year
	iMonthTo = strDateTo.substring(0, 2); //extracts the month
	iDateTo = strDateTo.substring(3, 5) //extracts the date
	iYearTo =  strDateTo.substring(6, 10);  //extracts the Year


	if (parseFloat(iYearFrom) > parseFloat(iYearTo))
	{
		
		return 1;	
	}
	else if ((parseFloat(iMonthFrom) > parseFloat(iMonthTo)) && (parseFloat(iYearFrom) >= parseFloat(iYearTo)))
	{
		
		return 1;	
	}
	else if ((parseFloat(iDateFrom) > parseFloat(iDateTo)) && (parseFloat(iMonthFrom) >=parseFloat(iMonthTo)) && (parseFloat(iYearFrom) >= parseFloat(iYearTo)))	
	{
		
		return 1;	
	}
	else
	{
		
		return 0;
	}		
	
	
}


function CompareDate(objDateTo,objDateFrom)
{
// to compare the date entered by the user with the current date. The date shouldn't be less 
// then current date
	var iYearFrom;
	var iMonthFrom;
	var iDateFrom;
	var iYearTo;
	var iMonthTo;
	var iDateTo;
	var sDelimitor
	var strDateFrom;
	var strDateTo;

	strDateFrom = objDateFrom
	strDateTo = objDateTo
	
	sDelimitor='/';
	iMonthFrom = strDateFrom.substring(0, 2); //extracts the month
	iDateFrom = strDateFrom.substring(3, 5); //extracts the date
	iYearFrom =  strDateFrom.substring(6, 10);  //extracts the Year
	iMonthTo = strDateTo.substring(0, 2); //extracts the month
	iDateTo = strDateTo.substring(3, 5) //extracts the date
	iYearTo =  strDateTo.substring(6, 10);  //extracts the Year


	if (parseFloat(iYearFrom) > parseFloat(iYearTo))
	{
		return 1;	
	}
	else if ((parseFloat(iMonthFrom) > parseFloat(iMonthTo)) && (parseFloat(iYearFrom) >= parseFloat(iYearTo)))
	{
		return 1;	
	}
	else if ((parseFloat(iDateFrom) > parseFloat(iDateTo)) && (parseFloat(iMonthFrom) >=parseFloat(iMonthTo)) && (parseFloat(iYearFrom) >= parseFloat(iYearTo)))	
	{
		return 1;	
	}
	else
	{
		
		return 0;
	}		
	
	
}

// CODE BY: SANDEEP
// DESCRIPTION: SEARCH FOR NO. OF CHECKED CHECKBOXS IN THE GRID
// INPUT: FORM OBJECT, CHECKBOX NAME
// OUTPUT: NO. OF CHECKED CHECKBOXES IN FORM

function GetChecked(oForm,str)
{
	var len;
	var len1;
	var ct;
	ct = 0;
	len1 = str.length;
	for (i=0; i<oForm.elements.length;i++)
	{
		len = oForm.elements[i].id.length;
		if (oForm.elements[i].id.substring(len-len1,len) == str)
			if (oForm.elements[i].checked)
				ct++;
	}
	if (ct==0)
		alert("Please select atleast one item from the list.");
	return ct;
}

function GetQuotation(str)
{
	var f;
	f = false;
	for (i=0; i<str.length; i++)
	{
		if(str.charAt(i) == "'" )
			f = true;
	}
	return f;
}

function FindControl(oForm, c)
{
	f=false;
	for (i=0; i<oForm.elements.length;i++)
	{
		if (oForm.elements[i].id == c)
		{
			f=true;
			break;
		}
	}
	return f;
}

// Change by Sandeep on 24-Dec-2004
// Description: Gives 2 digits after decimal point
function formatAsMoney(mnt) {
	mnt -= 0;
	mnt = (Math.round(mnt*100))/100;
	return (mnt == Math.floor(mnt)) ? mnt + '.00' 
	            : ( (mnt*10 == Math.floor(mnt*10)) ? 
	                    mnt + '0' : mnt);
}
