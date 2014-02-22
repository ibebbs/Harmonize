using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bebbs.Harmonize.With.Text
{
    public class MultiRegexBuilder
    {
        private readonly bool _includeLineDelimiters;
        private readonly Dictionary<string, string> _groups;

        public MultiRegexBuilder(bool includeLineDelimiters)
        {
            _includeLineDelimiters = includeLineDelimiters;
            _groups = new Dictionary<string, string>();
        }

        public MultiRegexBuilder(bool includeLineDelimiters, IEnumerable<KeyValuePair<string, string>> source) : this(includeLineDelimiters)
        {
            AddRange(source);
        }

        public void AddRange(IEnumerable<KeyValuePair<string, string>> source)
        {
            (source ?? Enumerable.Empty<KeyValuePair<string, string>>()).ForEach(kvp => Add(kvp.Key, kvp.Value));
        }

        public void Add(string name, string pattern)
        {
            _groups.Add(name, pattern);
        }

        public Regex ToRegex()
        {
            string pattern = string.Join("|", _groups.Select(kvp => string.Format("(?<{0}>{1})", kvp.Key, kvp.Value)).ToArray());

            if (_includeLineDelimiters)
            {
                pattern = string.Format("^{0}$", pattern);
            }

            return new Regex(pattern, RegexOptions.Compiled);
        }
    }
}
