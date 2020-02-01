namespace Adapter.SeabirdExample
{
    public class Seabird : Seacraft, IAircraft
    {
        int height = 0;

        public void TakeOff()
        {
            while (!Airborne)
                IncreaseRevs();
        }

        public override void IncreaseRevs()
        {
            base.IncreaseRevs();
            if (Speed > 40)
                height += 100;
        }

        public int Height
        {
            get { return height; }
        }

        public bool Airborne
        {
            get { return height > 50; }
        }
    }
}
