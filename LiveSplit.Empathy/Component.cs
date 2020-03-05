using System;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;

namespace LiveSplit.Empathy
{
    class Component : LogicComponent
    {
        public override string ComponentName => "Empathy Autosplitter";

        TimerModel _timer;
        LiveSplitState _state;
        bool _starting = false;

        Hooks _hooks;
        Settings _settings = new Settings();

        public Component(LiveSplitState state)
        {
            _state = state;
            _timer = new TimerModel { CurrentState = state };
            _hooks = new Hooks(this);
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            _hooks.Update();
        }

        public void On_Loading(bool loading)
        {
            if (!_starting)
            {
                _state.IsGameTimePaused = loading;
            }
        }

        public void On_Start()
        {
            if (_state.CurrentPhase != TimerPhase.NotRunning && _settings.AutoReset)
            {
                _timer.Reset();
            }
            if (_state.CurrentPhase == TimerPhase.NotRunning && _settings.AutoStart)
            {
                _timer.Start();
            }
            _state.IsGameTimePaused = true;
            _starting = true;
        }

        public void On_GameStart()
        {
            _starting = false;
            _state.IsGameTimePaused = false;
        }

        public void On_End()
        {
            if (_state.CurrentPhase == TimerPhase.Running && _settings.AutoEnd)
            {
                while (_state.CurrentSplitIndex < _state.Run.Count - 1)
                {
                    _timer.SkipSplit();
                }
                _timer.Split();
            }
        }

        public void On_MapSplit()
        {
            if (_state.CurrentPhase == TimerPhase.Running && _settings.AutoMiddle)
            {
                _timer.Split();
            }
        }

        public void On_MemorySplit()
        {
            if (_state.CurrentPhase == TimerPhase.Running && _settings.AutoSplit)
            {
                _timer.Split();
            }
        }

        public override XmlNode GetSettings(XmlDocument document) => _settings.GetSettings(document);

        public override Control GetSettingsControl(LayoutMode mode) => _settings;

        public override void SetSettings(XmlNode settings) => _settings.SetSettings(settings);

        public override void Dispose()
        {
            if (_hooks != null)
            {
                _hooks.Dispose();
                _hooks = null;
            }
            if (_settings != null)
            {
                _settings.Dispose();
                _settings = null;
            }
        }
    }
}
