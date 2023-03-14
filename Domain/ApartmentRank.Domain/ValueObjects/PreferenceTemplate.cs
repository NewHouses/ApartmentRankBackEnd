namespace ApartmentRank.Domain.ValueObjects
{
    public class PreferenceTemplate
    {
        public string name { get; set; }
        public IEnumerable<Preference> preferences { get; set; }
        public IEnumerable<PreferenceArea> preferenceAreas { get; set; }

        public PreferenceTemplate(string name, IEnumerable<Preference> preferences, IEnumerable<PreferenceArea> preferenceAreas) {
            this.name = name;
            this.preferences = preferences;
            this.preferenceAreas = preferenceAreas;
        }
    }
}
