namespace MetricTester
{
    public class SingleRandomThing
    {
        public Random RandomThing { get; private set; }
        private int maxValue;
        public SingleRandomThing(int maxValue)
        {
            RandomThing = new Random();
            this.maxValue = maxValue;
        }

        public int GetRandomThing()
        {
            return RandomThing.Next(maxValue);
        }
    }
}
