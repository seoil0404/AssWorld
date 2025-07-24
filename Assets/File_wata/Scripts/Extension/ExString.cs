using System.Text;

namespace Wata.Extension {
    public static class ExString {

        public static string VariableNameToString(this string target) {
            var labelContext = target
                .StartsWith("m_") ? target[2..] : target;
            labelContext = labelContext
                .StartsWith("_") ? target[1..] : target
                .Replace("_", " ");
            
            labelContext = char.ToUpper(labelContext[0]) + labelContext[1..];
            var builder = new StringBuilder();
            
            foreach (var c in labelContext) {
                if (char.IsUpper(c))
                    builder.Append(' ');
                builder.Append(c);
            }

            return builder.ToString();
        }
    }
}