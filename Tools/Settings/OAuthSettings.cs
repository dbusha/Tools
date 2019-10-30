namespace Tools.Settings
{
    public class OAuthSettings : SqliteSettings
    {
        public OAuthSettings() : base("OAuth") { }
        public OAuthSettings(string path) : base(path, "OAuth") { } 
        

        public string BaseUrl
        {
            get => (string)this[nameof(BaseUrl)];
            set => this[nameof(BaseUrl)] = value;
        }
        
        
        public string ConsumerKey
        {
            get => (string)this[nameof(ConsumerKey)];
            set => this[nameof(ConsumerKey)] = value;
        }
        

        public string ConsumerSecret 
        {
            get => (string)this[nameof(ConsumerSecret)];
            set => this[nameof(ConsumerSecret)] = value;
        }
        
        
        public string AccessToken 
        {
            get => (string)this[nameof(AccessToken)];
            set => this[nameof(AccessToken)] = value;
        }
        
        
        public string AccessTokenSecret
        {
            get => (string)this[nameof(AccessTokenSecret)];
            set => this[nameof(AccessTokenSecret)] = value;
        } 
        
        
        public string Session
        {
            get => (string)this[nameof(Session)];
            set => this[nameof(Session)] = value;
        } 
        
    }
}