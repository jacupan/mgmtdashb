﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GatePassApplication.Classes
{
    public class Common
    {
        public enum WebUserInformation
        {
            DomainName = 0,
            Username = 1
        }

        public static string GetWebCurrentUser(WebUserInformation webUserInformation)
        {
            string result = "";
            try
            {
                //string[] userInformation = HttpContext.Current.User.Identity.Name.ToString().Split(@"\".ToCharArray());
                string[] userInformation = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name.ToString().Split(@"\".ToCharArray());
                result = userInformation[(int)webUserInformation].ToString();
            }
            catch
            {
                result = "N/A";
            }
            //CommonFunctions.Iif(webUserInformation != WebUserInformation.DomainName, userInformation[0], userInformation[1]);

            // Return the result
            return result.ToUpper();
            //return userSTR;
        }
        public static string ChangeControlProperty(string ctrl, Boolean bol)
        {
           string str = "";
            
            if (bol == false && ctrl=="ButtonReturn")
            {
                str = "";
            }
            else if(bol==true && ctrl=="ButtonReturn")
            {
                str = "disabled='disabled'";
            }
            else if (bol == false && ctrl == "ButtonUnreturn")
            {
                str = "disabled='disabled'";
            }
            else
            {
                str = "";
            }
            return str;
        }
    }
}