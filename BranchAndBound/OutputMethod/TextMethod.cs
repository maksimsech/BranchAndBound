using System;
using System.Text;
using BranchAndBound.Method;

namespace BranchAndBound.OutputMethod
{
    public static class TextMethodExtencion
    {
        public static string ToResultString(this MethodValue value)
        {
            var result = new StringBuilder();
            
            var state = value.CurrentState switch
            {
                MethodValue.State.Initial => "Начало",
                MethodValue.State.Changed => "Изменено",
                MethodValue.State.Trimmed => "Исключено",
                MethodValue.State.Finish => "Конец",
                MethodValue.State.FinalPath => "Путь",
                _ => throw new ArgumentException("State not found")
            };

            result.AppendLine(state);

            if(value.Selected.HasValue)
                result.AppendLine($"({value.Selected?.row}, {value.Selected?.column})");

            if(string.IsNullOrEmpty(value.Matrix) == false)
                result.AppendLine(value.Matrix);

            return result.ToString();
        }
    }
}