using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Common_Core
{
    /// <summary>
    /// 文件处理帮助文件
    /// </summary>
    public static class JKFile
    {
        /// <summary>
        /// 通过文件名读取文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        public static string GetCountentFromFile(string fileName)
        {
            if (fileName == null)
            {
                throw new Exception("文件路径不允许为空");
            }
            return File.ReadAllText(fileName, GetEncoding(fileName));
        }

        /// <summary>
        /// 根据文件名得到文件编码
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns></returns>
        private static Encoding GetEncoding(string file)
        { 
            /*
             * 读取三个字节
             * 如果前两个字节的数据为ff fe 则为Unicode字符
             * 如果三个字节为 ef bb df 则为UTF8格式
             * 如果前两字节是 fe ff 则为Unicode Big Endian格式
             */
            // 读取第一个字节
            int firstChar;
            Encoding encoding;
            using(FileStream fileTemp = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                firstChar = fileTemp.ReadByte();
            }
            switch (firstChar)
            {
                case 0xff: encoding = Encoding.Unicode; break;
                case 0xef: encoding = Encoding.UTF8; break;
                case 0xfe: encoding = Encoding.BigEndianUnicode; break;
                default: encoding = Encoding.Default; break;
            }
            return encoding;
        }
    }
}
