﻿namespace Adapter.SeabirdExample
{
    public interface IAircraft
    {
        bool Airborne { get; }
        void TakeOff();
        int Height { get; }
    }
}
