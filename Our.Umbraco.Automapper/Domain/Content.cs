using Our.Umbraco.Automapper.Framework.Attributes;

namespace Our.Umbraco.Automapper.Domain
{
    public class Content
    {
        public string Url { get; set; }

        public int Id { get; set; }

        private bool hideFromNavigation;

        [UmbracoProperty(PropertyAlias = "umbracoNaviHide")]
        public bool HideFromNavigation
        {
            get { return hideFromNavigation; }
            set { hideFromNavigation = value; }
        }
    }
}