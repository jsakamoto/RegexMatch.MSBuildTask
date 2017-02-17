using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;

namespace MSBuildTask
{
    public class RegexMatch : Microsoft.Build.Utilities.Task
    {
        public string Pattern { get; set; } = "";

        public string Input { get; set; } = "";

        [Output]
        public bool IsMatch { get; private set; } = false;

        [Output]
        public string Match { get; private set; } = "";

        private string[] _Groups = new string[9];

        #region public string Mathc1 ~ Match9
        [Output]
        public string Match1 => _Groups[0];

        [Output]
        public string Match2 => _Groups[1];

        [Output]
        public string Match3 => _Groups[2];

        [Output]
        public string Match4 => _Groups[3];

        [Output]
        public string Match5 => _Groups[4];

        [Output]
        public string Match6 => _Groups[5];

        [Output]
        public string Match7 => _Groups[6];

        [Output]
        public string Match8 => _Groups[7];

        [Output]
        public string Match9 => _Groups[8];
        #endregion

        /// <summary>
        /// executes the task.
        /// </summary>
        /// <returns>true if the task successfully executed; otherwise, false.</returns>
        public override bool Execute()
        {
            for (var i = 0; i < _Groups.Length; i++) _Groups[i] = "";

            var match = Regex.Match(this.Input, this.Pattern);
            this.IsMatch = match.Success;
            if (match.Success)
            {
                this.Match = match.Value;
                foreach (var group in match.Groups.Cast<Group>().Take(9).Select((g, i) => new { Index = i, g.Value }))
                {
                    this._Groups[group.Index] = group.Value;
                }
            }

            //Log.LogMessage(MessageImportance.Normal, "Message.");
            return !Log.HasLoggedErrors;
        }
    }
}
