using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common_Core;

namespace XGY_Service
{
    public class AccountService
    {
        /// <summary>
        /// 将密码转换为MD5格式
        /// </summary>
        /// <param name="pwd"></param>
        public static string  ConvertPwd(string pwd)
        {
            try
            {
                string pwdMd5 = CommonHelper.GetMD5String(pwd);
                return pwdMd5;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }                
        }
    }
}
