﻿using System;

namespace BotCore.Data.Attributes;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class StateAttribute : Attribute
{
    private readonly string _state;

    public StateAttribute(string state)
    {
        _state = state;
    }

    public string GetState()
    {
        return _state;
    }
}