using System;
using System.Diagnostics;
using LiveSplit.ComponentUtil;

namespace LiveSplit.Empathy
{
    class Hooks : IDisposable
    {
        Component _parent;
        Process _empathy;
        MemoryWatcherList _memory = new MemoryWatcherList();
        MemoryWatcher<Int32> _wmvStatus;
        IntPtr _hook, _open, _browse;
        Int32 _cutscene;

        public Hooks(Component parent)
        {
            _parent = parent;
        }

        public void AddHooks(Process empathy)
        {
            _empathy = empathy;
            _memory.Clear();
            _wmvStatus = null;

            if (_empathy != null)
            {
                byte[] data = LiveSplit.Empathy.Properties.Resources.HookData;
                _hook = _empathy.AllocateMemory(data.Length);
                _empathy.WriteBytes(_hook, data);

                IntPtr baseaddr = _empathy.MainModuleWow64Safe().BaseAddress;
                WriteAddress(_empathy, _hook, baseaddr + 0x27C6D0);

                _open = _empathy.WriteDetour(baseaddr + 0x15CDE90, 0x14, _hook + 0xA0);
                WriteAddress(_empathy, _hook + 0x110, _open);

                _browse = _empathy.WriteDetour(baseaddr + 0x859B00, 0x11, _hook + 0x120);
                WriteAddress(_empathy, _hook + 0x198, _browse);

                MemoryWatcher<Int32> loading = new MemoryWatcher<Int32>(new DeepPointer(0x2616900, 0x84));
                loading.OnChanged += On_Loading;
                _memory.Add(loading);

                _wmvStatus = new MemoryWatcher<Int32>(new DeepPointer(_hook + 0x8, 0x30, 0x44));
                _wmvStatus.OnChanged += On_Cutscene;

                MemoryWatcher<Int32> map = new MemoryWatcher<Int32>(_hook + 0x14);
                map.OnChanged += On_MapChange;
                _memory.Add(map);
            }
        }

        public void Update()
        {
            try
            {
                if (_empathy != null && _empathy.HasExited)
                {
                    _memory.Clear();
                    _parent.On_Loading(false);
                    _empathy = null;
                }

                if (_empathy == null)
                {
                    Process[] processes = Process.GetProcessesByName("Empathy-Win64-Shipping");
                    _empathy = processes.Length == 0 ? null : processes[0];
                    AddHooks(_empathy);
                }

                if (_empathy != null)
                {
                    _memory.UpdateAll(_empathy);
                    _cutscene = _empathy.ReadValue<Int32>(_hook + 0x10);
                    if (_cutscene != 0)
                    {
                        _wmvStatus.Update(_empathy);
                    }
                }
            }
            catch
            {
                // Sometimes reads or writes fail due to race conditions.
                // Treat these exceptions as an exit event.
                _memory.Clear();
                _parent.On_Loading(false);
                _empathy = null;
            }
        }

        void On_Loading(Int32 old, Int32 current)
        {
            _parent.On_Loading(current == 0);
        }

        void On_Cutscene(Int32 old, Int32 current)
        {
            if (current == 3)
            {
                if (_cutscene == 1)
                {
                    _parent.On_Start();
                }
                else if (_cutscene == 2)
                {
                    _parent.On_End();
                }

                _empathy.WriteBytes(_hook + 0x10, new byte[4]);
                _wmvStatus.Reset();
            }
        }

        void On_MapChange(Int32 old, Int32 current)
        {
            if (old != 0 && current != 0 && current > old)
            {
                _parent.On_Split();
            }
        }

        public void Dispose()
        {
            try
            {
                if (_empathy != null && !_empathy.HasExited)
                {
                    IntPtr baseaddr = _empathy.MainModuleWow64Safe().BaseAddress;

                    byte[] openbuf = _empathy.ReadBytes(_open, 0x14);
                    _empathy.WriteBytes(baseaddr + 0x15CDE90, openbuf);
                    _empathy.FreeMemory(_open);

                    byte[] browsebuf = _empathy.ReadBytes(_browse, 0x11);
                    _empathy.WriteBytes(baseaddr + 0x859B00, browsebuf);
                    _empathy.FreeMemory(_browse);

                    _empathy.FreeMemory(_hook);
                }
            }
            finally
            {
                _empathy = null;
            }
        }

        private static void WriteAddress(Process empathy, IntPtr addr, IntPtr value)
        {
            empathy.WriteBytes(addr, BitConverter.GetBytes(value.ToInt64()));
        }
    }
}
