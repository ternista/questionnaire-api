namespace Questionnaires.Domain.Models
{
    public class Translation
    {
        public int TranslationId { get; internal set; }
        
        public string Locale { get; internal set; }

        public string Text { get; internal set; }

        internal Translation()
        {
        }

        public Translation(string locale, string text)
        {
            Locale = locale;
            Text = text;
        }
    }
}