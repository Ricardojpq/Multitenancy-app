namespace Utils.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return Char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }

        public static string ToShortDate(this string value)
        {
            return DateTime.Parse(value.ToString()).ToShortDateString();
        }

        public static string getFormatAndNames(this string s, ref List<string> names)
        {
            string fi = "";
            bool open = false;
            int currentIdx = 0, lastIdx = 0;
            for (int i = 0; i < s.Length; i++)
            {
                char v = s[i];
                if (v == '{')
                {
                    if (open)
                    {
                        fi += s.Substring(lastIdx, i - lastIdx - 1);
                        lastIdx = i;
                        continue;
                    }
                    lastIdx = i;
                    open = true;
                }
                if (v == '}' && open)
                {
                    fi += "{" + currentIdx + "}";
                    names.Add(s.Substring(lastIdx + 1, i - lastIdx - 1));
                    open = false;
                    currentIdx++;
                    continue;
                }
                else if (open) continue;
                fi += v;
            }
            if (names.Count == 0)
            {
                names.Add(s);
                fi = "{0}";
            }
            return fi;
        }
    }
}
