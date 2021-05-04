using System;

namespace Modules.TickModule
{
    public class VirtualTickController<TTick> where TTick : ITick
    {
        private TTick[] _ticks;

        private int _capacity = 4;
        private int _length;

        public VirtualTickController()
        {
            _length = 0;
            _ticks = new TTick[4];
        }
        
        public void Tick()
        {
            for (var i = 0; i < _length; i++)
            {
                _ticks[i]?.Tick();
            }
        }

        public void Add(TTick tick)
        {
            _ticks[_length] = tick;
            
            _length++;

            if (_length >= _capacity)
            {
                IncreaseTicksCapacity();
            }
        }

        public void Remove(TTick tick)
        {
            var removeAt = Array.IndexOf(_ticks, tick);

            _ticks[removeAt] = default;

            for (var i = removeAt; i< _length - 1; i++)
            {
                _ticks[i] = _ticks[i + 1];
            }
        }

        public void Clear()
        {
            _ticks = new TTick[0];
            _length = 0;
            _capacity = 4;
        }

        private void IncreaseTicksCapacity()
        {
            _capacity *= 2;
            var largerTicks = new TTick[_capacity];

            for (var i = 0; i < _length - 1; i++)
            {
                largerTicks[i] = _ticks[i];
            }

            _ticks = largerTicks;
        }
    }
}