namespace StubbUnity.Unity.Utils
{
    public class BitMask
    {
        public static int Set(int mask, int bit) => mask | 1 << bit;
        public static int UnSet(int mask, int bit) => mask & ~(1 << bit);
        public static bool IsSet(int mask, int bit) => (mask & (1 << bit)) != 0;
        
        private int _mask;

        public BitMask(int mask)
        {
            _mask = mask;
        }

        public void Set(int bit)
        {
            _mask = Set(_mask, bit);
        }

        public void UnSet(int bit)
        {
            _mask = UnSet(_mask, bit);
        }

        public bool IsSet(int bit)
        {
            return IsSet(_mask, bit);
        }
    }
}