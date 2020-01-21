using LiveSplit.Model;
using LiveSplit.UI.Components;
using System.Reflection;

namespace LiveSplit.Empathy
{
    public class ComponentFactory : IComponentFactory
    {
        public string ComponentName => "Empathy Autosplitter";
        public string Description => "Autosplitter and Load Remover for Empathy: Path of Whispers";
        public ComponentCategory Category => ComponentCategory.Control;
        public IComponent Create(LiveSplitState state) => new Component(state);
        public string UpdateName => this.ComponentName;
        public string UpdateURL => "https://raw.githubusercontent.com/LRFLEW/LiveSplit.Empathy/";
        public string XMLURL => this.UpdateURL + "master/Components/Updates.xml";
        public System.Version Version => Assembly.GetExecutingAssembly().GetName().Version;
    }
}
