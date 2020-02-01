﻿using System;

namespace Adapter.SeabirdExample
{
    public class Aircraft : IAircraft
    {
        int height;
        bool airborne;

        public Aircraft()
        {
            height = 0;
            airborne = false;
        }

        public virtual void TakeOff()
        {
            Console.WriteLine("Aircraft engine takeoff");
            airborne = true;
            height = 200;
        }

        public virtual bool Airborne
        {
            get { return airborne; }
        }

        public int Height
        {
            get { return height; }
            protected set { height = value; }
        }
    }
}
