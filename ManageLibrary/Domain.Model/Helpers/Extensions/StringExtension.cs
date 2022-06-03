using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Model.Helpers.Extensions
{
    public static class StringExtension
    {
        public static string CleanFormat(this string input, params string[] excludes)
        {
            string[] replaceThis = { " ", "+", "/&&", "||", "|", "!", "(", ")", "[", "]", "{", "}", "^", "~", "*", "?", ":", "\\"
                    , "%", "#", "\\", "/", "#", "\t", "\r", "\n","$","'","-","=",".",",","_"};
            if (excludes != null && excludes.Any())
            {
                replaceThis = replaceThis.Except(excludes).ToArray();
            }
            if (input == null)
                return string.Empty;
            StringBuilder sb = new StringBuilder(input);
            replaceThis.ToList().ForEach(s => sb.Replace(s, ""));
            return sb.ToString();
        }
        public static string RemoveCharactor(this string input, params string[] removes)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            StringBuilder sb = new StringBuilder(input);
            removes.ToList().ForEach(s => sb.Replace(s, ""));
            return sb.ToString();
        }
        public static string ContactValues(this string delimiter, params object[] args)
        {
            if (args == null) return string.Empty;
            var str = new List<string>();
            foreach (var o in args)
            {
                str.Add(o == null ? "" : o.ToString());
            }
            return string.Join(delimiter, str.ToArray());
        }
        public static string FirstCharToUpper(this string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
      
        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.RemoveAccent().ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            str = string.Join("-", str.Split('-', StringSplitOptions.RemoveEmptyEntries));
            str = str.Substring(0, str.Length <= 50 ? str.Length : 50).Trim();
            return str;
        }
        public static string GetSentenceFromWords(this string text, string search)
        {
            var index = text.IndexOf(search, StringComparison.InvariantCultureIgnoreCase);
            if (index == -1) return string.Empty;
            var beginOfSearch = text.LastIndexOf(" ", index);
            return text.Substring(Math.Max(beginOfSearch, 0), Math.Min(text.Length - Math.Max(beginOfSearch, 0), 40));
        }
        public static bool IsMatchSuggest(this string text, string search)
        {
            var index = text.IndexOf(search, StringComparison.InvariantCultureIgnoreCase);
            if (index < 0) return false;
            else return true;
        }
        public static bool IsValidEmail(this string inputEmail)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(inputEmail);
            if (match.Success)
                return true;
            else
                return false;
        }
        public static bool IsValidSlug(this string input)
        {
            Regex regex = new Regex(@"^[a-z0-9-]+$");
            Match match = regex.Match(input);
            if (match.Success)
                return true;
            else
                return false;
        }
        public static bool IsValidURL(this string input)
        {
            Regex regex = new Regex(@"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(w|W){3}.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?");
            Match match = regex.Match(input);
            if (match.Success)
                return true;
            else
                return false;
        }
        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return Encoding.ASCII.GetString(bytes);
        }
        public static string RemoveDuplicateWord(this string input)
        {
            return string.Join(" ", input.Split(' ').Distinct());
        }
        public static string RemoveStyleAttributeFromHTMLTags(this string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;
            //Remove style
            Regex r = new Regex(@"<([^>]*)(\sstyle="".+?""(\s|))(.*?)>");
            return r.Replace(html, "");
        }
        public static string RemoveQueryParameter(this string input)
        {
            return input?.Split('?')[0] ?? string.Empty;
        }
        public static string RemoveCountryCode(this string number)
        {
            if(number.Length>=10)
                return number.Substring(number.Length-10);
            return number;
        }
        public static string ReplaceLastAt(this string input, char oldChar, char newChar)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            char[] chars = input.ToCharArray();
            int index = input.LastIndexOf(oldChar);
            if (index < 0 || index > input.Length)
                return input;
            chars[index] = newChar;
            return new string(chars);
        }
        public static string ReplaceSpecialCharactorBySpace(this string input)
        {
            string[] replaceThis = {"+", "/&&", "||", "|", "!", "(", ")", "[", "]", "{", "}", "^", "~", "*", "?", ":", "\\"
                    , "%", "#", "\\", "/", "#", "\t", "\r", "\n","$","'","-","=",".", "’",",","_", "–"};
            if (input == null)
                return string.Empty;
            StringBuilder sb = new StringBuilder(input);
            replaceThis.ToList().ForEach(s => sb.Replace(s, " "));
            return sb.ToString().TrimAndSingleSpace();
        }
        public static string SplitCamelCase(this string str)
        {
            System.Text.StringBuilder output = new System.Text.StringBuilder(str.Substring(0, 1));

            for (int i = 1; i < str.Length; i++)
            {
                if (Char.IsUpper(str[i]) && (!char.IsUpper(str[i - 1]) || (i + 1 < str.Length && char.IsLower(str[i + 1]))))
                {
                    output.Append(" " + str[i]);
                }
                else
                {
                    output.Append(str[i]);
                }
            }
            return output.ToString();
        }
        public static string StandardizedFullTextSearch(this string input)
        {
            if (input == null)
                return string.Empty;
            var listStop = new List<string>() { "to", "the", "a", "in", "i", "m", "s", "no", "co", "re", "t" };
            var result = ("\"" + input.ReplaceSpecialCharactorBySpace().Replace(" ", @"*"" AND """) + "*\"").ToLower();
            foreach (var item in listStop)
            {
                result = result.Replace($"\"{item}*\"", $"\"{item}\"");
            }
            return result;
        }
        public static string StandardizedAzureFileName(this string input)
        {
            var fileName = Path.GetFileNameWithoutExtension(input);
            var ext = Path.GetExtension(input);
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()) + "#%";
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            var result = r.Replace(fileName, "").ToLower();         
            return result.Substring(0, Math.Min(result.Length, 1015)) + ext;
        }
        public static string StandardizeRexPartern(this string input)
        {
            string partern = @"[\\\^\$\.\|\?\*\+\-\[\]\{\}\(\)]";
            return Regex.Replace(input, partern, "\\$0", RegexOptions.IgnoreCase);
        }
        public static string StandardizedFileName(this string input)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(input, "");
        }
        public static string StandardizedCleanPhoneNumber(this string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            var rex = new Regex(@"(\+\d{1,3}|\d{1,3}\()?[- (]{0,2}(\d{1,4})[- )]{0,2}(\d{0,4})[- )](\d{0,4})");
            return rex.Replace(input, "$2$3$4");
        }
        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }
        public static string StripTagsRegexCompiled(string source)
        {
            Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);
            return _htmlRegex.Replace(source, string.Empty);
        }
        public static byte[] ToByteArray(this Stream stream)
        {
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length;)
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
            return buffer;
        }
    
        public static string StripTagsCharArray(this string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
        public static string HtmlToPlainText(this string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            const string blockTags = @"<+(\W*)+\/+(\W*)+(address|article|aside|blockquote|canvas|dd|div|dl|dt|fieldset|figcaption|figure|footer|form|h1|h2|h3|h4|h5|h6|header|hr|li|main|nav|noscript|ol|p|pre|section|table|tfoot|ul|video|ADDRESS|ARTICLE|ASIDE|BLOCKQUOTE|CANVAS|DD|DIV|DL|DT|FIELDSET|FIGCAPTION|FIGURE|FOOTER|FORM|H1|H2|H3|H4|H5|H6|HEADER|HR|LI|MAIN|NAV|NOSCRIPT|OL|P|PRE|SECTION|TABLE|TFOOT|UL|VIDEO)+(\W*)+>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);
            var tagBlockSpaceRegex = new Regex(blockTags, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            text = tagBlockSpaceRegex.Replace(text, " ");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
        public static string TrimAllSpace(this string input)
        {
            if (input == null)
                return string.Empty;
            return string.Join("", input.Split(null as char[], StringSplitOptions.RemoveEmptyEntries));
        }
        public static string TrimAndSingleSpace(this string input, bool isRemoveSpostrophe = false, bool isRemoveDoubleQuote = true)
        {
            if (input == null)
                return string.Empty;
            if (isRemoveSpostrophe)
                input = input.Replace("'", "");
            if (isRemoveDoubleQuote)
                input.Replace("\"", "");
            return string.Join(" ", input.Split(null as char[], StringSplitOptions.RemoveEmptyEntries));
        }
        public static string CutString(this string input, int length)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return input.Substring(0, Math.Min(input.Length, length));
        }
        public static string CombineUri(this string root, params string[] uriParts)
        {
            char[] trims = new char[] { '\\', '/' };
            string uri = (root ?? string.Empty).TrimEnd(trims);
            if (uriParts != null && uriParts.Length > 0)
            {
                for (int i = 1; i < uriParts.Length; i++)
                {
                    uri = string.Format("{0}/{1}", uri.TrimEnd(trims), (uriParts[i] ?? string.Empty).TrimStart(trims));
                }
            }
            return uri;
        }
      
        public static string ToLinkString(this string str)
        {
            try
            {
                return Regex.Replace(str, "\\b(href=&quot;|href=\"|href='|)(http)([:0-9a-zA-Z./#=&?_-])*", delegate (Match match)
                {
                    return match.Value.IndexOf("href")==0 ? match.Value : $" <a class='secondary1--text' href='{match.Value}' target='_blank'><strong>{match.Value}</strong></a>";
                });

            }
            catch
            {
                return str;
            }
        }
        public static string SafeToLower(this string str)
        {
            if (str == null)
            {
                str = string.Empty;
            }
            return str.ToLower();
        }
        public static string HashPassword(this string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
        public static bool VerifyHashedPassword(this string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }
        public static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }
        public static string LoremIpsum(int minWords, int maxWords, int minSentences = 1, int maxSentences = 1, int numLines = 1)
        {
            var words = new[] { "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat" };

            var rand = new Random();
            int numSentences = rand.Next(maxSentences - minSentences)
                + minSentences;
            int numWords = rand.Next(maxWords - minWords) + minWords;

            var sb = new StringBuilder();
            for (int p = 0; p < numLines; p++)
            {
                for (int s = 0; s < numSentences; s++)
                {
                    for (int w = 0; w < numWords; w++)
                    {
                        if (w > 0) { sb.Append(" "); }
                        string word = words[rand.Next(words.Length)];
                        if (w == 0) { word = word.Substring(0, 1).Trim().ToUpper() + word.Substring(1); }
                        sb.Append(word);
                    }
                    sb.Append(". ");
                }
                if (p < numLines - 1) sb.AppendLine();
            }
            return sb.ToString();
        }
        public static string CreateMD5Hash(string input)
        {
            // Step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public static string VideoType(string url)
        {
            var ext = Path.GetExtension(url).ToLower();
            switch(ext)
            {
                case ".mp4":
                    return "video/mp4";
                case ".webm":
                    return "video/webm";
                case ".mov":
                    return "video/mp4";
                default:
                    return "application/vnd.ms-sstr+xml";
            }
        }
        public static string AppendFileName(this string fileName, string appendPart)
        {
            var path = Path.GetFileNameWithoutExtension(fileName);
            var ext = Path.GetExtension(fileName);
            return Path.Combine(Path.GetDirectoryName(fileName), $"{path}{appendPart}{ext}");
        }
        public static string AppendFileName(this string fileName, string appendPart, string newExt)
        {
            var path = Path.GetFileNameWithoutExtension(fileName);
            return Path.Combine(Path.GetDirectoryName(fileName), $"{path}{appendPart}{newExt}");
        }
        public static string GetNumeric(this string input)
        {
            if (input == null)
                return string.Empty;
            return string.Join("", input.ToCharArray().Where(Char.IsDigit));
        }
      
       
        public static List<T> ToListEnum<T>(this string value, string separator)
        {
            if (value.Trim() == "") return null;
            var list = value.TrimAllSpace().Split(separator).Select(c => Int32.Parse(c)).ToList();
            return list.ConvertAll(delegate (int id)
            {
                return (T)Enum.Parse(typeof(T), id.ToString());
            });

        }

        public static int? ToNullableInt(this string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;

            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }
        public static string NullIfEmpty(this string s)
        {
            return s?.Trim() == "" ? null : s;
        }
    }
}
