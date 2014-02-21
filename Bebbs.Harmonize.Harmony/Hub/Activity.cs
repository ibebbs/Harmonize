namespace Bebbs.Harmonize.With.Harmony.Hub
{
    public interface IActivity
    {
        int Id { get; }
        string Type { get; }
        string ActivityTypeDisplayName { get; }
        string Label { get; }
        string Icon { get; }
        string SuggestedDisplay { get; }
    }

    internal class Activity : IActivity
    {
        int IActivity.Id
        {
            get { return id; }
        }

        string IActivity.Type
        {
            get { return type; }
        }

        string IActivity.ActivityTypeDisplayName
        {
            get { return activityTypeDisplayName; }
        }

        string IActivity.Label
        {
            get { return label; }
        }

        string IActivity.Icon
        {
            get { return icon; }
        }

        string IActivity.SuggestedDisplay
        {
            get { return suggestedDisplay; }
        }

        public int id { get; set; }
        public string type { get; set; }
        public string activityTypeDisplayName { get; set; }
        public string label { get; set; }
        public string icon { get; set; }
        public string suggestedDisplay { get; set; }
    }
}
