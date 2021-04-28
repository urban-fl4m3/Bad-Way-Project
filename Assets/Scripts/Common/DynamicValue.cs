using System;

namespace Common
{
    public class DynamicValue<TValue>
    {
        public event EventHandler<TValue> Changed;

        private TValue _value;

        private DynamicValue()
        {
            
        }
        
        public DynamicValue(TValue value)
        {
            _value = value;
        }

        public TValue Value
        {
            get => _value;
            set
            {
                if (_value == null)
                {
                    if (value != null)
                    {
                        _value = value;
                        OnChanged(value);
                    }
                } 
                else if (!_value.Equals(value))
                {
                    _value = value;
                    OnChanged(value);
                }
                
            }
        }
        
        private void OnChanged(TValue e)
        {
            Changed?.Invoke(this, e);
        }
    }
}