﻿using System;

namespace Common.Commands
{
    public interface ICommand
    {
        event EventHandler OnComplete;
        event EventHandler OnFail;
        
        void Execute();
    }
}