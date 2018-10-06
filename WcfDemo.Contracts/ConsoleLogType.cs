using System;
using System.ComponentModel;

namespace WcfDemo.Contracts
{
    public enum ConsoleDisplayType
    {
        [Description("INIT")]
        Initialization = 0,

        [Description("INFO")]
        Information = 1,

        [Description("VALI")]
        ValidationError = 2,

        [Description("INTE")]
        InternalError = 3,

        [Description("SUCC")]
        Success = 4,

        [Description("EXIT")]
        Exit = 5,

        [Description("MISS")]
        Missing = 6,

        [Description("PREP")]
        Preparing = 7,

        [Description("OPTI")]
        Option = 8,

        [Description("INST")]
        Instruction = 9
    }

    public class ConsoleDisplayHelper
    {
        public static string GetEnumDescription(ConsoleDisplayType consoleDisplayType)
        {
            var fileInfo = consoleDisplayType.GetType().GetField(consoleDisplayType.ToString());
            var attributes = (DescriptionAttribute[])fileInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            
            return attributes != null && attributes.Length > 0
                ? attributes[0].Description
                : consoleDisplayType.ToString();
        }

        public static ConsoleColor ReturnConsoleTextColor(ConsoleDisplayType consoleDisplayType)
        {
            switch (consoleDisplayType)
            {
                case ConsoleDisplayType.Success:
                    return ConsoleColor.Green;

                case ConsoleDisplayType.Exit:
                    return ConsoleColor.DarkGray;

                case ConsoleDisplayType.Information:
                    return ConsoleColor.Gray;

                case ConsoleDisplayType.Initialization:
                    return ConsoleColor.Gray;

                case ConsoleDisplayType.Instruction:
                    return ConsoleColor.White;

                case ConsoleDisplayType.InternalError:
                    return ConsoleColor.Red;

                case ConsoleDisplayType.Missing:
                    return ConsoleColor.Magenta;

                case ConsoleDisplayType.Option:
                    return ConsoleColor.White;

                case ConsoleDisplayType.Preparing:
                    return ConsoleColor.Gray;

                case ConsoleDisplayType.ValidationError:
                    return ConsoleColor.Red;

                default:
                    return ConsoleColor.Gray;

            }
        }
    }
}
