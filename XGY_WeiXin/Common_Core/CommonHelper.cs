using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Common_Core
{
    public class CommonHelper
    {
        //计算字符串的md5值
        public static string GetMD5String(string msg)
        {
            StringBuilder sb = new StringBuilder();
            //1.创建一个md5对象
            using (MD5 md5 = MD5.Create())
            {
                //1.1把字符串转换为byte[]
                //对于字符串中包含中文，如果在进行md5计算前，使用不同的编码返回字节数组，那么可能最后计算出的md5值会不相同，所以要使用相同的md5编码
                byte[] buffers = System.Text.Encoding.UTF8.GetBytes(msg);
                //2.进行md5计算,md5计算完毕后，返回的也是一个byte[]
                byte[] bytes = md5.ComputeHash(buffers);
                md5.Clear();//释放资源，清除内存
                //3.把bytes中的每个字节转换为一个16进制表示的字符串，并返回
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
            }
            return sb.ToString();
        }

    }
}
