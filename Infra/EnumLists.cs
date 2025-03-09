using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Localization.Infra;
using System.Resources;

namespace Infra
{
    public class EnumLists
    {
        public enum EOrder
        {
            Up = 0,
            Down = 1
        }

        /// <summary>
        /// Tipos de Dados de Configuração
        /// </summary>
        public enum EConfigurationType
        {
            String = 1,
            Number = 2,
            Language = 3,
            Switch = 4
        }

        /// <summary>
        /// Tipos de Log
        /// </summary>
        public enum ELogType
        {
            [Display(ResourceType = typeof(ResEnumLists), Name = "ELogType_Login")]
            Login = 1,
            [Display(ResourceType = typeof(ResEnumLists), Name = "ELogType_AddRecord")]
            AddRecord = 2,
            [Display(ResourceType = typeof(ResEnumLists), Name = "ELogType_UpdateRecord")]
            UpdateRecord = 3,
            [Display(ResourceType = typeof(ResEnumLists), Name = "ELogType_DeleteRecord")]
            DeleteRecord = 4,
            [Display(ResourceType = typeof(ResEnumLists), Name = "ELogType_Email")]
            Email = 5,
            [Display(ResourceType = typeof(ResEnumLists), Name = "ELogType_Error")]
            Error = 6,
            [Display(ResourceType = typeof(ResEnumLists), Name = "ELogType_Service")]
            Service = 7
        }

        /// <summary>
        /// Dias da semana
        /// </summary>
        public enum EDaysOfWeek
        {
            [Display(ResourceType = typeof(ResEnumLists), Name = "EDaysOfWeek_Sunday")]
            Sunday = DayOfWeek.Sunday,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EDaysOfWeek_Monday")]
            Monday = DayOfWeek.Monday,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EDaysOfWeek_Tuesday")]
            Tuestday = DayOfWeek.Tuesday,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EDaysOfWeek_Wednesday")]
            Wednesday = DayOfWeek.Wednesday,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EDaysOfWeek_Thursday")]
            Thursday = DayOfWeek.Thursday,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EDaysOfWeek_Friday")]
            Friday = DayOfWeek.Friday,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EDaysOfWeek_Saturday")]
            Saturday = DayOfWeek.Saturday
        }

        /// <summary>
        /// Retorna a lista de dias da semana da string passada
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public static string NamesOfWeekByString(string codes)
        {
            return Enum.GetValues(
                typeof(EDaysOfWeek)).Cast<object>().Aggregate(codes, (current, item) =>
               current.Replace(((int)item).ToString(),
               GetLocalizedDisplay<EDaysOfWeek>(item.ToString()).Substring(0, 3)));
        }

        /// <summary>
        /// Meses do ano
        /// </summary>
        public enum EMonths
        {
            [Display(ResourceType = typeof(ResEnumLists), Name = "EMonths_January")]
            January = 1,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EMonths_February")]
            February = 2,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EMonths_March")]
            March = 3,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EMonths_April")]
            April = 4,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EMonths_May")]
            May = 5,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EMonths_June")]
            June = 6,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EMonths_July")]
            July = 7,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EMonths_August")]
            August = 8,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EMonths_September")]
            September = 9,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EMonths_October")]
            October = 10,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EMonths_November")]
            November = 11,
            [Display(ResourceType = typeof(ResEnumLists), Name = "EMonths_December")]
            December = 12
        }


        /// <summary>
        /// Retorna a lista de meses da string passada
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public static string NamesOfMonthByString(string codes)
        {
            if (string.IsNullOrEmpty(codes))
                return codes;

            var arrayMonths = codes.Replace(" ", "").Split(',');
            var returnData = "";
            foreach (var month in arrayMonths)
            {
                returnData += (string.IsNullOrEmpty(returnData) ? "" : ", ");
                returnData += GetLocalizedDisplay<EMonths>(
                    ((EMonths)int.Parse(month))
                    .ToString()).Substring(0, 3);
            }
            return returnData;
        }

        /// <summary>
        /// Retorna a descrição do Enum, baseado no idioma
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="pPropertyName"></param>
        /// <returns></returns>
        public static string GetLocalizedDisplay<TModel>(string pPropertyName)
        {
            DisplayAttribute attribute;

            if (typeof(TModel).IsEnum)
            {
                MemberInfo member = typeof(TModel).GetMembers().SingleOrDefault(m => m.MemberType == MemberTypes.Field && m.Name == pPropertyName);
                attribute = (DisplayAttribute)member.GetCustomAttributes(typeof(DisplayAttribute), false)[0];
            }
            else
            {
                PropertyInfo property = typeof(TModel).GetProperty(pPropertyName);
                attribute = (DisplayAttribute)property.GetCustomAttributes(typeof(DisplayAttribute), true)[0];
            }

            if (attribute.ResourceType != null)
                return new ResourceManager(attribute.ResourceType).GetString(attribute.Name);
            else
                return attribute.Name;
        }
    }
}
