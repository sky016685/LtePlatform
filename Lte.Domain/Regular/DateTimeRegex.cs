﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lte.Domain.Regular
{
    public static class DateTimeRegex
    {
        /// <summary>
        /// 匹配日期是否合法
        /// </summary>
        /// <param name="source">待匹配字符串</param>
        /// <returns>匹配结果true是日期反之不是日期</returns>
        public static bool CheckDateByString(this string source)
        {
            var rg = new Regex(@"^(\d{4}[\/\-](0?[1-9]|1[0-2])[\/\-]((0?[1-9])|((1|2)[0-9])|30|31))|((0?[1-9]|1[0-2])[\/\-]((0?[1-9])|((1|2)[0-9])|30|31)[\/\-]\d{4})$");
            return rg.IsMatch(source);
        }

        /// <summary>
        /// 从字符串中获取第一个日期
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>源字符串中的第一个日期</returns>
        public static string GetFirstDateByString(this string source)
        {
            return Regex.Match(source, @"(\d{4}[\/\-](0?[1-9]|1[0-2])[\/\-]((0?[1-9])|((1|2)[0-9])|30|31))|((0?[1-9]|1[0-2])[\/\-]((0?[1-9])|((1|2)[0-9])|30|31)[\/\-]\d{4})").Groups[0].Value;
        }

        /// <summary>
        /// 从字符串中获取所有的日期
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>源字符串中的所有日期</returns>
        public static List<string> GetAllDateByString(this string source)
        {
            var all = Regex.Matches(source, @"(\d{4}[\/\-](0?[1-9]|1[0-2])[\/\-]((0?[1-9])|((1|2)[0-9])|30|31))|((0?[1-9]|1[0-2])[\/\-]((0?[1-9])|((1|2)[0-9])|30|31)[\/\-]\d{4})");
            return (from Match item in all select item.Value).ToList();
        }

        public static string GetStrictDateByString(this string source)
        {
            return
                Regex.Match(source,
                    @"([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8])))")
                    .Groups[0].Value;
        }

        public static string GetPersistentDateTimeString(this string source)
        {
            return
                Regex.Match(source,
                    @"([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})(((0[13578]|1[02])(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)(0[1-9]|[12][0-9]|30))|(02(0[1-9]|[1][0-9]|2[0-8])))(([01][0-9])|(2[0-3]))([0-5][0-9])([0-5][0-9])")
                    .Groups[0].Value;
        }

        public static DateTime? GetDateTimeFromFileName(this string fileName)
        {
            var dateTimeString = fileName.GetPersistentDateTimeString();
            if (string.IsNullOrEmpty(dateTimeString)) return null;
            var year = dateTimeString.Substring(0, 4);
            var month = dateTimeString.Substring(4, 2);
            var day = dateTimeString.Substring(6, 2);
            var hour = dateTimeString.Substring(8, 2);
            var minute = dateTimeString.Substring(10, 2);
            var second = dateTimeString.Substring(12, 2);
            return new DateTime(year.ConvertToInt(2015), month.ConvertToInt(1), day.ConvertToInt(1),
                hour.ConvertToInt(12), minute.ConvertToInt(0), second.ConvertToInt(0));
        }
    }
}
