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
        MemoryWatcher<UInt32> _wmvStatus;
        IntPtr _hook, _open, _browse, _start, _memcom;
        UInt32 _cutscene;

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
                WriteAddress(_empathy, _hook, baseaddr + 0x17536B2);

                _open = _empathy.WriteDetour(baseaddr + 0x15CDE90, 0xE, _hook + 0x110);
                WriteAddress(_empathy, _hook + 0x18A, _open);

                _browse = _empathy.WriteDetour(baseaddr + 0x859B00, 0xD, _hook + 0x1A0);
                WriteAddress(_empathy, _hook + 0x205, _browse);

                _start = _empathy.WriteDetour(baseaddr + 0x1EE8E0, 0x11, _hook + 0x210);
                WriteAddress(_empathy, _hook + 0x218, _start);

                _memcom = _empathy.WriteDetour(baseaddr + 0x1E7790, 0xF, _hook + 0x230);
                WriteAddress(_empathy, _hook + 0x238, _memcom);

                MemoryWatcher<UInt32> loading = new MemoryWatcher<UInt32>(new DeepPointer(0x2616900, 0x84));
                loading.OnChanged += On_Loading;
                _memory.Add(loading);

                _wmvStatus = new MemoryWatcher<UInt32>(new DeepPointer(_hook + 0x8, 0x30, 0x44));
                _wmvStatus.OnChanged += On_Cutscene;

                MemoryWatcher<UInt32> map = new MemoryWatcher<UInt32>(_hook + 0x14);
                map.OnChanged += On_MapChange;
                _memory.Add(map);

                MemoryWatcher<UInt32> start = new MemoryWatcher<UInt32>(_hook + 0x18);
                start.OnChanged += On_StartChange;
                _memory.Add(start);

                MemoryWatcher<UInt32> memcom = new MemoryWatcher<UInt32>(_hook + 0x1C);
                memcom.OnChanged += On_MemcomChange;
                _memory.Add(memcom);
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
                    _cutscene = _empathy.ReadValue<UInt32>(_hook + 0x10);
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

        void On_Loading(UInt32 old, UInt32 current)
        {
            _parent.On_Loading(current == 0);
        }

        void On_Cutscene(UInt32 old, UInt32 current)
        {
            if (current == 3)
            {
                if (_cutscene == 1)
                {
                    _parent.On_GameStart();
                }
                else if (_cutscene == 2)
                {
                    _parent.On_End();
                }

                _empathy.WriteBytes(_hook + 0x10, new byte[4]);
                _wmvStatus.Reset();
            }
        }

        void On_MapChange(UInt32 old, UInt32 current)
        {
            if (old != 0 && current != 0 && current > old)
            {
                _parent.On_MapSplit();
            }
        }

        void On_StartChange(UInt32 old, UInt32 current)
        {
            _parent.On_Start();
        }

        void On_MemcomChange(UInt32 old, UInt32 current)
        {
            for (UInt32 i=0; i < (current - old); ++i)
            {
                _parent.On_MemorySplit();
            }
        }

        public void Dispose()
        {
            try
            {
                if (_empathy != null && !_empathy.HasExited)
                {
                    IntPtr baseaddr = _empathy.MainModuleWow64Safe().BaseAddress;

                    byte[] openbuf = _empathy.ReadBytes(_open, 0xE);
                    _empathy.WriteBytes(baseaddr + 0x15CDE90, openbuf);
                    _empathy.FreeMemory(_open);

                    byte[] browsebuf = _empathy.ReadBytes(_browse, 0xD);
                    _empathy.WriteBytes(baseaddr + 0x859B00, browsebuf);
                    _empathy.FreeMemory(_browse);

                    byte[] startbuf = _empathy.ReadBytes(_start, 0x11);
                    _empathy.WriteBytes(baseaddr + 0x1EE8E0, startbuf);
                    _empathy.FreeMemory(_start);

                    byte[] memcombuf = _empathy.ReadBytes(_memcom, 0xF);
                    _empathy.WriteBytes(baseaddr + 0x1E7790, memcombuf);
                    _empathy.FreeMemory(_memcom);

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
